using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain;

namespace OrderApi.Service.v1.Query
{
    public class GetOrderByCustomerGuidQueryHandler : IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>
    {
        private readonly IRepository<Order> _repository;

        public GetOrderByCustomerGuidQueryHandler(IRepository<Order> repository)
        {
            _repository = repository;
        }

        public async Task<List<Order>> Handle(GetOrderByCustomerGuidQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAll().Where(x => x.CustomerGuid == request.CustomerCuid).ToListAsync(cancellationToken);
        }
    }
}