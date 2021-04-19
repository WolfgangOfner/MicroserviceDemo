using System.Collections.Generic;
using CustomerApi.Data.Entities;
using MediatR;

namespace CustomerApi.Service.v1.Query
{
    public class GetCustomersQuery : IRequest<List<Customer>>
    {
    }
}