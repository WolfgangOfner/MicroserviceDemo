using MediatR;
using OrderApi.Domain.Entities;

namespace OrderApi.Service.v1.Command
{
   public class PayOrderCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }
}
