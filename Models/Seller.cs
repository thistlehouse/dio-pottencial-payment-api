using System.Collections.Generic;
using System.Text.Json.Serialization;


namespace tech_test_payment_api.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public int Cpf { get; set; } = 123456789;
        public string Name { get; set; } = "Base Seller";
        public string Email { get; set; } = "base_seller@email.com";
        public string Phone { get; set; } = "222222";        

    }
}