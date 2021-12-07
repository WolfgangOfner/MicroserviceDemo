using MediatR;
using OrderApi.Domain.Entities;

namespace OrderApi.Service.v1.Command
{
    public class CreateOrderCommand : IRequest<Order>
    {
        public Order Order { get; set; }
    }
}
