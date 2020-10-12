using System.Collections.Generic;
using FakeItEasy;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain;
using OrderApi.Service.v1.Command;
using Xunit;

namespace OrderApi.Service.Test.v1.Command
{
    public class UpdateOrderCommandHandlerTests
    {
        private readonly UpdateOrderCommandHandler _testee;
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandlerTests()
        {
            _orderRepository = A.Fake<IOrderRepository>();
            _testee = new UpdateOrderCommandHandler(_orderRepository);
        }

        [Fact]
        public async void Handle_ShouldCallRepositoryAddAsync()
        {
            await _testee.Handle(new UpdateOrderCommand(), default);

            A.CallTo(() => _orderRepository.UpdateRangeAsync(A<List<Order>>._)).MustHaveHappenedOnceExactly();
        }
    }
}