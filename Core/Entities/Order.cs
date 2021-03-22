using System;
using System.Collections.Generic;

namespace Foodie.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderLine> OrderLines { get; set; }
    }
}
