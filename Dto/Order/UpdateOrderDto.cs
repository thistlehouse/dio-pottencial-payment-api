using tech_test_payment_api.Models;

namespace tech_test_payment_api.Dto.Order
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public EStatus Status { get; set; } = EStatus.Awaiting_Payment;
    }
}