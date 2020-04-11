using FakeItEasy;
using FluentAssertions;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain;
using OrderApi.Service.v1.Command;
using Xunit;

namespace OrderApi.Service.Test.v1.Command
{
    public class PayOrderCommandHandlerTests
    {
        private readonly IRepository<Order> _repository;
        private readonly PayOrderCommandHandler _testee;

        public PayOrderCommandHandlerTests()
        {
            _repository = A.Fake<IRepository<Order>>();
            _testee = new PayOrderCommandHandler(_repository);
        }

        [Fact]
        public async void Handle_ShouldReturnUpdatedOrder()
        {
            var order = new Order
            {
                CustomerFullName = "Bruce Wayne"
            };
            A.CallTo(() => _repository.UpdateAsync(A<Order>._)).Returns(order);

            var result = await _testee.Handle(new PayOrderCommand(), default);

            result.Should().BeOfType<Order>();
            result.CustomerFullName.Should().Be("Bruce Wayne");
        }

        [Fact]
        public async void Handle_ShouldCallRepositoryUpdateAsync()
        {
            await _testee.Handle(new PayOrderCommand(), default);

            A.CallTo(() => _repository.UpdateAsync(A<Order>._)).MustHaveHappenedOnceExactly();
        }
    }
}