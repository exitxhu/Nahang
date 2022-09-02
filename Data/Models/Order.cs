using System.Collections.Generic;

namespace Nahang.Data.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double TotalPrice { get; set; }
        public int UserId { get; set; }
        public int PaymentId { get; set; }
        public User User { get; set; }
        public HashSet<OrderItem> OrderItems { get; set; }
        public OrderStatusEnum Status { get; set; }
        public Payment? Payment { get; set; }

        public enum OrderStatusEnum
        {
            NEW = 1,
            PAID = 2,
            PROCCESSED = 3,
            REJECTED = 4
        }
        public class OrderItem
        {
            public int OrderItemId { get; set; }
            public int OrderId { get; set; }
            public int ProductId { get; set; }
            public int Amount { get; set; }
        }
    }
}
