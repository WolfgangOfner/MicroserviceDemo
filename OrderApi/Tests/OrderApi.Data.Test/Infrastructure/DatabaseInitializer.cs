using System;
using System.Linq;
using OrderApi.Data.Database;
using OrderApi.Domain;

namespace OrderApi.Data.Test.Infrastructure
{
    public class DatabaseInitializer
    {
        public static void Initialize(OrderContext context)
        {
            if (context.Order.Any())
            {
                return;
            }

            Seed(context);
        }

        private static void Seed(OrderContext context)
        {
            var orders = new[]
            {
                new Order
                {
                    Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                    OrderState = 1,
                    CustomerGuid = Guid.Parse("d3e3137e-ccc9-488c-9e89-50ba354738c2"),
                    CustomerFullName = "Wolfgang Ofner"
                },
                new Order
                {
                    Id = Guid.Parse("bffcf83a-0224-4a7c-a278-5aae00a02c1e"),
                    OrderState = 1,
                    CustomerGuid = Guid.Parse("4a2f1e35-f527-4136-8b12-138a57e1ba08"),
                    CustomerFullName = "Darth Vader"
                },
                new Order
                {
                    Id = Guid.Parse("58e5cd7d-856b-4224-bdff-bd8f85bf5a6d"),
                    OrderState = 2,
                    CustomerGuid = Guid.Parse("334feb16-d7bb-4ca9-ab56-f4fadeb88d21"),
                    CustomerFullName = "Son Goku"
                }
            };

            context.Order.AddRange(orders);
            context.SaveChanges();
        }
    }
}