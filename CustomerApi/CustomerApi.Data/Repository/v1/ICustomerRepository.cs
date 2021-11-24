using System;
using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Data.Repository.v1
{
    public interface ICustomerRepository: IRepository<Customer>
    {
        Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}