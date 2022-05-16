using System.Collections.Generic;

namespace Nahang.Data.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int UserId { get; set; }
        public HashSet<CartItem> CartItems { get; set; }
        public User User { get; set; }
        public class CartItem
        {
            public int CartItemId { get; set; }
            public int CartId { get; set; }
            public int ProductId { get; set; }
            public int Amount { get; set; }
        }
    }
}
