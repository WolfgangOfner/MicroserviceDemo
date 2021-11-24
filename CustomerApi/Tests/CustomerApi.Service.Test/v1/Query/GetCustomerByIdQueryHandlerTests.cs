using System;
using System.Threading.Tasks;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using CustomerApi.Service.v1.Query;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Service.Test.v1.Query
{
    public class GetCustomerByIdQueryHandlerTests
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly GetCustomerByIdQueryHandler _testee;
        private readonly Customer _customer;
        private readonly Guid _id = Guid.Parse("803a95ef-89c5-43d5-aa2c-82a3695d9894");

        public GetCustomerByIdQueryHandlerTests()
        {
            _customerRepository = A.Fake<ICustomerRepository>();
            _testee = new GetCustomerByIdQueryHandler(_customerRepository);

            _customer = new Customer { Id = _id, Age = 42 };
        }

        [Fact]
        public async Task Handle_WithValidId_ShouldReturnCustomer()
        {
            A.CallTo(() => _customerRepository.GetCustomerByIdAsync(_id, default)).Returns(_customer);

            var result = await _testee.Handle(new GetCustomerByIdQuery { Id = _id }, default);

            A.CallTo(() => _customerRepository.GetCustomerByIdAsync(_id, default)).MustHaveHappenedOnceExactly();
            result.Age.Should().Be(42);
        }
    }
}