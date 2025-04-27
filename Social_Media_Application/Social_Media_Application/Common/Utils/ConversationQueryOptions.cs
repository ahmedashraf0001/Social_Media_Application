namespace Social_Media_Application.Common.Utils
{
    public class ConversationQueryOptions
    {
        public bool WithMessages { get; set; } = false;
        public bool WithUserInitiated { get; set; } = false ;
        public bool WithUserReceived { get; set; } = false;
    }
}
