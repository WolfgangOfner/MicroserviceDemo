using System.Collections.Generic;
using MediatR;
using OrderApi.Domain.Entities;

namespace OrderApi.Service.v1.Query
{
    public class GetPaidOrderQuery : IRequest<List<Order>>
    {
    }
}