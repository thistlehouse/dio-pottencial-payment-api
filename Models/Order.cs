using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tech_test_payment_api.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int SellerId { get; set; }
        public Seller Seller { get; set; }
        public List<Item> Items { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public EStatus Status { get; set; } = EStatus.Awaiting_Payment;
    }
}