namespace Omf.Common.Events
{
    public class ExceptionEvent
    {
        public ExceptionEvent(string code, string message, dynamic eventData)
        {
            Code = code;
            Message = message;
            EventData = eventData;
        }
        public string Code { get; set; }
        public string Message { get; set; }
        public dynamic EventData { get; set; }
    }
}