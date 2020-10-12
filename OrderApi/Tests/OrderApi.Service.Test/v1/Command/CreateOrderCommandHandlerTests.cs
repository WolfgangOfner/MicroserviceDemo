using FakeItEasy;
using FluentAssertions;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain;
using OrderApi.Service.v1.Command;
using Xunit;

namespace OrderApi.Service.Test.v1.Command
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly IOrderRepository _orderRepository;
        private readonly CreateOrderCommandHandler _testee;

        public CreateOrderCommandHandlerTests()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _testee = new CreateOrderCommandHandler(_orderRepository);
        }

        [Fact]
        public async void Handle_ShouldReturnCreatedOrder()
        {
            A.CallTo(() => _orderRepository.AddAsync(A<Order>._)).Returns(new Order { CustomerFullName = "Bruce Wayne" });

            var result = await _testee.Handle(new CreateOrderCommand(), default);

            result.Should().BeOfType<Order>();
            result.CustomerFullName.Should().Be("Bruce Wayne");
        }

        [Fact]
        public async void Handle_ShouldCallRepositoryAddAsync()
        {
            await _testee.Handle(new CreateOrderCommand(), default);

            A.CallTo(() => _orderRepository.AddAsync(A<Order>._)).MustHaveHappenedOnceExactly();
        }
    }
}