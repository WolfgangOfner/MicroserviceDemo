using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using CustomerApi.Service.v1.Query;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Service.Test.v1.Query
{
    public class GetCustomersQueryHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly GetCustomersQueryHandler _testee;
        private readonly List<Customer> _customers;

        public GetCustomersQueryHandlerTests()
        {
            _customerRepository = A.Fake<ICustomerRepository>();
            _testee = new GetCustomersQueryHandler(_customerRepository);

            _customers = new List<Customer>
            {
                new Customer
                {
                    Id = new Guid(),
                    Age = 42
                },
                new Customer
                {
                    Id = new Guid(),
                    Age = 22
                }
            };
        }

        [Fact]
        public async Task Handle_ShouldReturnCustomers()
        {
            A.CallTo(() => _customerRepository.GetAll()).Returns(_customers);

            var result = await _testee.Handle(new GetCustomersQuery(), default);

            A.CallTo(() => _customerRepository.GetAll()).MustHaveHappenedOnceExactly();
            result.Should().BeOfType<List<Customer>>();
            result.Count.Should().Be(2);
        }
    }
}