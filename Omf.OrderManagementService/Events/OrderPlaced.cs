namespace Omf.Common.Events
{
    public class PaymentInitiatedEvent
    {
        public PaymentInitiatedEvent(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }
    }
}