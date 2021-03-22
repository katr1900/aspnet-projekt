﻿using System;
namespace Foodie.Core.Entities
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
