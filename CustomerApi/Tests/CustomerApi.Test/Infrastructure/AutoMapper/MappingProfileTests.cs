using System;
using AutoMapper;
using CustomerApi.Data.Entities;
using CustomerApi.Infrastructure.AutoMapper;
using CustomerApi.Models.v1;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Test.Infrastructure.AutoMapper
{
    public class MappingProfileTests
    {
        private readonly CreateCustomerModel _createCustomerModel;
        private readonly UpdateCustomerModel _updateCustomerModel;
        private readonly IMapper _mapper;

        public MappingProfileTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = mockMapper.CreateMapper();

            _createCustomerModel = new CreateCustomerModel
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Birthday = new DateTime(1989, 11, 23),
                Age = 30
            };
            _updateCustomerModel = new UpdateCustomerModel
            {
                Id = Guid.Parse("9f35b48d-cb87-4783-bfdb-21e36012930a"),
                FirstName = "FirstName",
                LastName = "LastName",
                Birthday = new DateTime(1989, 11, 23),
                Age = 30
            };
        }

        [Fact]
        public void Map_Customer_CreateCustomerModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Customer, CreateCustomerModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Customer_UpdateCustomerModel_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Customer, UpdateCustomerModel>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_Customer_Customer_ShouldHaveValidConfig()
        {
            var configuration = new MapperConfiguration(cfg =>
                cfg.CreateMap<Customer, Customer>());

            configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void Map_CreateCustomerModel_Customer()
        {
            var customer = _mapper.Map<Customer>(_createCustomerModel);

            customer.FirstName.Should().Be(_createCustomerModel.FirstName);
            customer.LastName.Should().Be(_createCustomerModel.LastName);
            customer.Birthday.Should().Be(_createCustomerModel.Birthday);
            customer.Age.Should().Be(_createCustomerModel.Age);
        }

        [Fact]
        public void Map_UpdateCustomerModel_Customer()
        {
            var customer = _mapper.Map<Customer>(_updateCustomerModel);

            customer.Id.Should().Be(_updateCustomerModel.Id);
            customer.FirstName.Should().Be(_createCustomerModel.FirstName);
            customer.LastName.Should().Be(_createCustomerModel.LastName);
            customer.Birthday.Should().Be(_createCustomerModel.Birthday);
            customer.Age.Should().Be(_createCustomerModel.Age);
        }
    }
}