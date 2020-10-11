using System.Collections.Generic;
using MediatR;
using OrderApi.Domain;

namespace OrderApi.Service.v1.Command
{
    public class UpdateOrderCommand : IRequest
    {
        public List<Order> Orders { get; set; }
    }
}