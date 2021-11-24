using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using CustomerApi.Service.v1.Command;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Service.Test.v1.Command
{
    public class CreateCustomerCommandHandlerTests
    {
        private readonly CreateCustomerCommandHandler _testee;
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandlerTests()
        {
            _customerRepository = A.Fake<ICustomerRepository>();
            _testee = new CreateCustomerCommandHandler(_customerRepository);
        }

        [Fact]
        public async void Handle_ShouldCallAddAsync()
        {
            await _testee.Handle(new CreateCustomerCommand(), default);

            A.CallTo(() => _customerRepository.AddAsync(A<Customer>._)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedCustomer()
        {
            A.CallTo(() => _customerRepository.AddAsync(A<Customer>._)).Returns(new Customer
            {
                FirstName = "Yoda"
            });

            var result = await _testee.Handle(new CreateCustomerCommand(), default);

            result.Should().BeOfType<Customer>();
            result.FirstName.Should().Be("Yoda");
        }
    }
}