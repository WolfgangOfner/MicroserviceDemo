using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using OrderApi.Data.Repository.v1;
using OrderApi.Domain;

namespace OrderApi.Service.v1.Query
{
    public class GetOrderByCustomerGuidQueryHandler : IRequestHandler<GetOrderByCustomerGuidQuery, List<Order>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByCustomerGuidQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> Handle(GetOrderByCustomerGuidQuery request, CancellationToken cancellationToken)
        {
            // todo tests
            return await _orderRepository.GetOrderByCustomerGuidAsync(request.CustomerId, cancellationToken);
        }
    }
}