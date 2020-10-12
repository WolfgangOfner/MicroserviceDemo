using System;
using System.Collections.Generic;
using System.Linq;
using FakeItEasy;
using FluentAssertions;
using OrderApi.Data.Database;
using OrderApi.Data.Repository.v1;
using OrderApi.Data.Test.Infrastructure;
using OrderApi.Domain;
using Xunit;

namespace OrderApi.Data.Test.Repository.v1
{
    public class RepositoryTests : DatabaseTestBase
    {
        private readonly OrderContext _orderContext;
        private readonly Repository<Order> _testee;
        private readonly Repository<Order> _testeeFake;
        private readonly Order _newOrder;

        public RepositoryTests()
        {
            _orderContext = A.Fake<OrderContext>();
            _testeeFake = new Repository<Order>(_orderContext);
            _testee = new Repository<Order>(Context);
            _newOrder = new Order
            {
                CustomerGuid = Guid.Parse("a4498698-bda1-4241-b3b7-4c8fe9649c54"),
                CustomerFullName = "Son Goku",
                OrderState = 1
            };
        }

        [Theory]
        [InlineData("Changed")]
        public async void UpdateOrderAsync_WhenOrderIsNotNull_ShouldReturnOrder(string newOrderFullName)
        {
            var order = Context.Order.First();
            order.CustomerFullName = newOrderFullName;

            var result = await _testee.UpdateAsync(order);

            result.Should().BeOfType<Order>();
            result.CustomerFullName.Should().Be(newOrderFullName);
        }

        [Theory]
        [InlineData("Changed")]
        public async void UpdateOrderAsync_WhenOrdersIsNotNull_ShouldUpdateOrders(string newOrderFullName)
        {
            var orders = _testee.GetAll().ToList();
            orders.ForEach(x => x.CustomerFullName = newOrderFullName);

            await _testee.UpdateRangeAsync(orders);

            Context.Order.All(x => x.CustomerFullName == newOrderFullName).Should().BeTrue();
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _orderContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new Order())).Should().Throw<Exception>().WithMessage("entity could not be saved Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public async void CreateOrderAsync_WhenOrderIsNotNull_ShouldReturnOrder()
        {
            var result = await _testee.AddAsync(_newOrder);

            result.Should().BeOfType<Order>();
        }

        [Fact]
        public async void CreateOrderAsync_WhenOrderIsNotNull_ShouldShouldAddOrder()
        {
            var orderCount = Context.Order.Count();

            await _testee.AddAsync(_newOrder);

            Context.Order.Count().Should().Be(orderCount + 1);
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _orderContext.Set<Order>()).Throws<Exception>();

            _testeeFake.Invoking(x => x.GetAll()).Should().Throw<Exception>().WithMessage("Couldn't retrieve entities Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public void UpdateAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.UpdateAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _orderContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.UpdateAsync(new Order())).Should().Throw<Exception>().WithMessage("entity could not be updated Exception of type 'System.Exception' was thrown.");
        }

        [Fact]
        public void UpdateRangeAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.UpdateRangeAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateRangeAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _orderContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.UpdateRangeAsync(new List<Order>())).Should().Throw<Exception>().WithMessage("entities could not be updated Exception of type 'System.Exception' was thrown.");
        }
    }
}