using System;
using MediatR;
using OrderApi.Domain.Entities;

namespace OrderApi.Service.v1.Query
{
   public class GetOrderByIdQuery : IRequest<Order>
    {
        public Guid Id { get; set; }
    }
}
