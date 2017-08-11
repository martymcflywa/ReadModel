namespace DataAccess
{
    public class EventEntry
    {
        public long SequenceId { get; }
        public string Message { get; }

        public EventEntry(long sequenceId, string message)
        {
            SequenceId = sequenceId;
            Message = message;
        }
    }
}