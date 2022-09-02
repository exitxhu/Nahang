using Microsoft.EntityFrameworkCore;
using Nahang.Data;
namespace Nahang.Data.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public string Details { get; set; }
        public User User { get; set; }
    }
}
