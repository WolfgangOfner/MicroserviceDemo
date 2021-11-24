using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Sender.v1;
using CustomerApi.Service.v1.Command;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Service.Test.v1.Command
{
    public class UpdateCustomerCommandHandlerTests
    {
        private readonly UpdateCustomerCommandHandler _testee;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUpdateSender _customerUpdateSender;
        private readonly Customer _customer;

        public UpdateCustomerCommandHandlerTests()
        {
            _customerRepository = A.Fake<ICustomerRepository>();
            _customerUpdateSender = A.Fake<ICustomerUpdateSender>();
            _testee = new UpdateCustomerCommandHandler(_customerUpdateSender, _customerRepository);

            _customer = new Customer
            {
                FirstName = "Yoda"
            };
        }

        [Fact]
        public async void Handle_ShouldCallCustomerUpdaterSenderSendCustomer()
        {
            A.CallTo(() => _customerRepository.UpdateAsync(A<Customer>._)).Returns(_customer);

            await _testee.Handle(new UpdateCustomerCommand(), default);

            A.CallTo(() => _customerUpdateSender.SendCustomer(_customer)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void Handle_ShouldReturnUpdatedCustomer()
        {
            A.CallTo(() => _customerRepository.UpdateAsync(A<Customer>._)).Returns(_customer);

            var result = await _testee.Handle(new UpdateCustomerCommand(), default);

            result.Should().BeOfType<Customer>();
            result.FirstName.Should().Be(_customer.FirstName);
        }

        [Fact]
        public async void Handle_ShouldUpdateAsync()
        {
            await _testee.Handle(new UpdateCustomerCommand(), default);

            A.CallTo(() => _customerRepository.UpdateAsync(A<Customer>._)).MustHaveHappenedOnceExactly();
        }
    }
}