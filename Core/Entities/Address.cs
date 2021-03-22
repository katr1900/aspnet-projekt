﻿using System;
namespace Foodie.Core.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public int UserId { get; set; }
    }
}
