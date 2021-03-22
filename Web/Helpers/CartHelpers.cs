using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foodie.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Foodie.Web.Helpers
{
    public class CartHelpers
    {
        private ISession session;

        public CartHelpers(ISession session)
        {
            this.session = session;
        }

        public Cart GetCart()
        {
            var value = session.GetString("cart");
            return value == null ? new Cart { CartLines = new List<CartItem>() } : JsonSerializer.Deserialize<Cart>(value);
        }

        public void EmptyCart()
        {
            var cart = GetCart();
            cart.CartLines = new List<CartItem>();
            SaveCart(cart);
        }

        public void UpdateCart(Product product, int quantity)
        {
            var cart = GetCart();
            var cartItem = cart.CartLines.FirstOrDefault(i => i.Product.Id == product.Id);
            if (cartItem == null)
            {
                if (quantity > 0)
                {
                    cart.CartLines.Add(new CartItem { Product = product, Quantity = quantity });
                }
            }
            else
            {
                if (quantity == 0)
                {
                    cart.CartLines.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity = quantity;
                }
            }

            SaveCart(cart);
        }

        public void SaveCart(Cart cart)
        {
            session.SetString("cart", JsonSerializer.Serialize(cart));
        }
    }
}