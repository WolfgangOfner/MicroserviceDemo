using CustomerApi.Data.Entities;
using MediatR;

namespace CustomerApi.Service.v1.Command
{
    public class UpdateCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}