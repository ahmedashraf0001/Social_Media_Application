namespace Social_Media_Application.Common.Utils.Queries
{
    public class ConversationQueryOptions
    {
        public bool WithMessages { get; set; } = false;
        public bool WithUserInitiated { get; set; } = false ;
        public bool WithUserReceived { get; set; } = false;

        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 12;
    }
}
