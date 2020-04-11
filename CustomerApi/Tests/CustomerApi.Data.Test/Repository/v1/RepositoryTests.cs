using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CustomerApi.Data.Database;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Data.Test.Infrastructure;
using FakeItEasy;
using FluentAssertions;
using Xunit;

namespace CustomerApi.Data.Test.Repository.v1
{
    public class RepositoryTests : DatabaseTestBase
    {
        private readonly CustomerContext _CustomerContext;
        private readonly Repository<Customer> _testee;
        private readonly Repository<Customer> _testeeFake;
        private readonly Customer _newCustomer;

        public RepositoryTests()
        {
            _CustomerContext = A.Fake<CustomerContext>();
            _testeeFake = new Repository<Customer>(_CustomerContext);
            _testee = new Repository<Customer>(Context);
            _newCustomer = new Customer
            {
                FirstName = "Son",
                LastName = "Goku",
                Birthday = new DateTime(737, 04, 16),
                Age = 1282
            };
        }

        [Theory]
        [InlineData("Changed")]
        public async void UpdateCustomerAsync_WhenCustomerIsNotNull_ShouldReturnCustomer(string firstName)
        {
            var customer = Context.Customer.First();
            customer.FirstName = firstName;

            var result = await _testee.UpdateAsync(customer);

            result.Should().BeOfType<Customer>();
            result.FirstName.Should().Be(firstName);
        }

        [Fact]
        public void AddAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.AddAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void AddAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _CustomerContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.AddAsync(new Customer())).Should().Throw<Exception>().WithMessage("entity could not be saved");
        }

        [Fact]
        public async void CreateCustomerAsync_WhenCustomerIsNotNull_ShouldReturnCustomer()
        {
            var result = await _testee.AddAsync(_newCustomer);

            result.Should().BeOfType<Customer>();
        }

        [Fact]
        public async void CreateCustomerAsync_WhenCustomerIsNotNull_ShouldShouldAddCustomer()
        {
            var CustomerCount = Context.Customer.Count();

            await _testee.AddAsync(_newCustomer);

            Context.Customer.Count().Should().Be(CustomerCount + 1);
        }

        [Fact]
        public void GetAll_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _CustomerContext.Set<Customer>()).Throws<Exception>();

            _testeeFake.Invoking(x => x.GetAll()).Should().Throw<Exception>().WithMessage("Couldn't retrieve entities");
        }

        [Fact]
        public void UpdateAsync_WhenEntityIsNull_ThrowsException()
        {
            _testee.Invoking(x => x.UpdateAsync(null)).Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void UpdateAsync_WhenExceptionOccurs_ThrowsException()
        {
            A.CallTo(() => _CustomerContext.SaveChangesAsync(default)).Throws<Exception>();

            _testeeFake.Invoking(x => x.UpdateAsync(new Customer())).Should().Throw<Exception>().WithMessage("entity could not be updated");
        }
    }
}