using System.Threading;
using System.Threading.Tasks;
using CustomerApi.Data.Entities;
using CustomerApi.Data.Repository.v1;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CustomerApi.Service.v1.Query
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, Customer>
    {
        private readonly IRepository<Customer> _repository;

        public GetCustomerByIdQueryHandler(IRepository<Customer> repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        }
    }
}