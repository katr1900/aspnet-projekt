using System;
using Microsoft.EntityFrameworkCore;
using Foodie.Core.Data;
using Foodie.Core.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Foodie.Core.Models
{
    public class OrderModel
    {
        private DataContext dataContext;

        public OrderModel(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public async Task RegisterOrder(User user, Cart cart)
        {
            var order = new Order { UserId = user.Id, OrderDate = DateTime.Now, OrderLines = new List<OrderLine>() };
            foreach (var cartLine in cart.CartLines)
            {
                var orderLine = new OrderLine { ProductId = cartLine.Product.Id, Quantity = cartLine.Quantity };
                order.OrderLines.Add(orderLine);
            }

            dataContext.Orders.Add(order);
            await dataContext.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAll()
        {
            return await dataContext.Orders.Include(o => o.OrderLines).ToListAsync();
        }

        public async Task DeleteOrder(int id)
        {
            var order = await dataContext.Orders.FirstOrDefaultAsync(c => c.Id == id);
            dataContext.Orders.Remove(order);
            await dataContext.SaveChangesAsync();
        }
    }
}
