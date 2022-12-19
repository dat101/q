using Microsoft.EntityFrameworkCore;
using System.Linq;
namespace PetShop.Data
{
    public class EFOrderRepository : IOrderRepository
    {
        private PetShopContext context;
        public EFOrderRepository(PetShopContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Order> Orders => context.Orders
        .Include(o => o.Lines)
        .ThenInclude(l => l.Pet);
        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(l => l.Pet));
            if (order.OrderID == 0)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
    }
}