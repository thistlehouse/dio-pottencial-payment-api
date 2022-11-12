using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace tech_test_payment_api.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Base Item";
        public string Description { get; set; } = "Base Item Description";
        // [JsonIgnore]
        // public List<Order> Orders { get; set; }
    }
}