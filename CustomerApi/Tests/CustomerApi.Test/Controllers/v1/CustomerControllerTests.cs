using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using AutoMapper;
using CustomerApi.Controllers.v1;
using CustomerApi.Domain.Entities;
using CustomerApi.Models.v1;
using CustomerApi.Service.v1.Command;
using CustomerApi.Service.v1.Query;
using FakeItEasy;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace CustomerApi.Test.Controllers.v1
{
    public class CustomerControllerTests
    {
        private readonly IMediator _mediator;
        private readonly CustomerController _testee;
        private readonly CreateCustomerModel _createCustomerModel;
        private readonly UpdateCustomerModel _updateCustomerModel;
        private readonly Guid _id = Guid.Parse("5224ed94-6d9c-42ec-ba93-dfb11fe68931");

        public CustomerControllerTests()
        {
            var mapper = A.Fake<IMapper>();
            _mediator = A.Fake<IMediator>();
            _testee = new CustomerController(mapper, _mediator);

            _createCustomerModel = new CreateCustomerModel
            {
                FirstName = "FirstName",
                LastName = "LastName",
                Birthday = new DateTime(1989, 11, 23),
                Age = 30
            };
            _updateCustomerModel = new UpdateCustomerModel
            {
                Id = _id,
                FirstName = "FirstName",
                LastName = "LastName",
                Birthday = new DateTime(1989, 11, 23),
                Age = 30
            };
            var customer = new List<Customer>
            {
                new Customer
                {
                    Id = _id,
                    FirstName = "FirstName",
                    LastName = "LastName",
                    Birthday = new DateTime(1989, 11, 23),
                    Age = 30
                },
                new Customer
                {
                    Id = Guid.Parse("654b7573-9501-436a-ad36-94c5696ac28f"),
                    FirstName = "Darth",
                    LastName = "Vader",
                    Birthday = new DateTime(1977, 05, 25),
                    Age = 43
                }
            };

            A.CallTo(() => mapper.Map<Customer>(A<Customer>._)).Returns(customer.First());
            A.CallTo(() => _mediator.Send(A<CreateCustomerCommand>._, default)).Returns(customer.First());
            A.CallTo(() => _mediator.Send(A<UpdateCustomerCommand>._, default)).Returns(customer.First());
            A.CallTo(() => _mediator.Send(A<GetCustomersQuery>._, default)).Returns(customer);
        }

        [Theory]
        [InlineData("CreateCustomerAsync: customer is null")]
        public async void Post_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<CreateCustomerCommand>._, default)).Throws(new ArgumentException(exceptionMessage));

            var result = await _testee.Customer(_createCustomerModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Theory]
        [InlineData("UpdateCustomerAsync: customer is null")]
        [InlineData("No user with this id found")]
        public async void Put_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<UpdateCustomerCommand>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.Customer(_updateCustomerModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Get_ShouldReturnCustomers()
        {
            var result = await _testee.Customers();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<List<Customer>>();
            result.Value.Count.Should().Be(2);
        }

        [Theory]
        [InlineData("Customers could not be loaded")]
        public async void Get_WhenAnExceptionOccurs_ShouldReturnBadRequest(string exceptionMessage)
        {
            A.CallTo(() => _mediator.Send(A<GetCustomersQuery>._, default)).Throws(new Exception(exceptionMessage));

            var result = await _testee.Customers();

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            (result.Result as BadRequestObjectResult)?.Value.Should().Be(exceptionMessage);
        }

        [Fact]
        public async void Post_ShouldReturnCustomer()
        {
            var result = await _testee.Customer(_createCustomerModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Customer>();
            result.Value.Id.Should().Be(_id);
        }

        [Fact]
        public async void Put_ShouldReturnCustomer()
        {
            var result = await _testee.Customer(_updateCustomerModel);

            (result.Result as StatusCodeResult)?.StatusCode.Should().Be((int)HttpStatusCode.OK);
            result.Value.Should().BeOfType<Customer>();
            result.Value.Id.Should().Be(_id);
        }
    }
}