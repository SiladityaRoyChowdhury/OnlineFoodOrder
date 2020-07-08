namespace Omf.Common.Events
{
    public class OrderReadyEvent
    {
        public OrderReadyEvent(string fromAddress, string address, int ordderId)
        {
            FromAddress = fromAddress;
            ToAddress = address;
            OrderId = ordderId;
        }
        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int OrderId { get; set; }
    }
}