using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderApi.Data.Database;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain.Entities;
using OrderApi.Infrastructure.Prometheus;
using OrderApi.Messaging.Receive.Options.v1;
using OrderApi.Messaging.Receive.Receiver.v1;
using OrderApi.Models.v1;
using OrderApi.Service.v1.Command;
using OrderApi.Service.v1.Query;
using OrderApi.Service.v1.Services;
using OrderApi.Validators.v1;
using Prometheus;

namespace OrderApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();
            services.AddOptions();

            var serviceClientSettingsConfig = Configuration.GetSection("RabbitMq");
            var serviceClientSettings = serviceClientSettingsConfig.Get<RabbitMqConfiguration>();
            services.Configure<RabbitMqConfiguration>(serviceClientSettingsConfig);

            bool.TryParse(Configuration["BaseServiceSettings:UseInMemoryDatabase"], out var useInMemory);
            bool.TryParse(Configuration["UseAadAuthentication"], out var useAadAuthentication);

            if (!useInMemory)
            {
                if (useAadAuthentication)
                {
                    services.AddDbContext<OrderContext>(options =>
                    {
                        SqlAuthenticationProvider.SetProvider(
                            SqlAuthenticationMethod.ActiveDirectoryDeviceCodeFlow,
                            new CustomAzureSqlAuthProvider(Configuration["TenantId"]));
                        var sqlConnection = new SqlConnection(Configuration.GetConnectionString("OrderDatabase"));
                        options.UseSqlServer(sqlConnection);
                    });
                }
                else
                {
                    services.AddDbContext<OrderContext>(options =>
                    {
                        options.UseSqlServer(Configuration.GetConnectionString("OrderDatabase"));
                    });
                }
            }
            else
            {
                services.AddDbContext<OrderContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            }

            services.AddAutoMapper(typeof(Startup));

            services.AddMvc().AddFluentValidation();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Order Api",
                    Description = "A simple API to create or pay orders",
                    Contact = new OpenApiContact
                    {
                        Name = "Wolfgang Ofner",
                        Email = "Wolfgang@programmingwithwolfgang.com",
                        Url = new Uri("https://www.programmingwithwolfgang.com/")
                    }
                });
            });

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as ActionExecutingContext;

                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(ICustomerNameUpdateService).Assembly);

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddTransient<IValidator<OrderModel>, OrderModelValidator>();

            services.AddTransient<IRequestHandler<GetPaidOrderQuery, List<Order>>, GetPaidOrderQueryHandler>();
            services.AddTransient<IRequestHandler<GetOrderByIdQuery, Order>, GetOrderByIdQueryHandler>();
            services.AddTransient<IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>, GetOrderByCustomerGuidQueryHandler>();
            services.AddTransient<IRequestHandler<CreateOrderCommand, Order>, CreateOrderCommandHandler>();
            services.AddTransient<IRequestHandler<PayOrderCommand, Order>, PayOrderCommandHandler>();
            services.AddTransient<IRequestHandler<UpdateOrderCommand>, UpdateOrderCommandHandler>();
            services.AddTransient<ICustomerNameUpdateService, CustomerNameUpdateService>();

            services.AddSingleton<MetricCollector>();

            if (serviceClientSettings.Enabled)
            {
                services.AddHostedService<CustomerFullNameUpdateReceiver>();
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseRouting();

            app.UseMetricServer();
            app.UseMiddleware<ResponseMetricMiddleware>();
            app.UseHttpMetrics();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
