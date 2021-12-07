using System;
using System.Collections.Generic;
using MediatR;
using OrderApi.Domain.Entities;

namespace OrderApi.Service.v1.Query
{
    public class GetOrderByCustomerGuidQuery : IRequest<List<Order>>
    {
        public Guid CustomerId { get; set; }
    }
}