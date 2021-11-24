using CustomerApi.Domain.Entities;
using MediatR;

namespace CustomerApi.Service.v1.Command
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}