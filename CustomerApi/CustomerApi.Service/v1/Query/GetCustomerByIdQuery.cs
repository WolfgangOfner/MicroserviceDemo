using System;
using CustomerApi.Data.Entities;
using MediatR;

namespace CustomerApi.Service.v1.Query
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}