using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OrderApi.Data.Database;
using OrderApi.Domain.Entities;

namespace OrderApi.Data.Repository.v1
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext orderContext) : base(orderContext)
        {
        }

        public async Task<List<Order>> GetPaidOrdersAsync(CancellationToken cancellationToken)
        {
            return await OrderContext.Order.Where(x => x.OrderState == 2).ToListAsync( cancellationToken);
        }

        public async Task<Order> GetOrderByIdAsync(Guid orderId, CancellationToken cancellationToken)
        {
            return await OrderContext.Order.FirstOrDefaultAsync(x => x.Id == orderId, cancellationToken);
        }

        public async Task<List<Order>> GetOrderByCustomerGuidAsync(Guid customerId, CancellationToken cancellationToken)
        {
            return await OrderContext.Order.Where(x => x.CustomerGuid == customerId).ToListAsync(cancellationToken);
        }
    }
}