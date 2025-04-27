namespace Social_Media_Application.DataAccess.Interfaces
{
    public class MessageQueryOptions
    {
        public bool WithSenderInfo { get; set; } = false;
        public bool WithReceiverInfo { get; set; } = false;
    }
}