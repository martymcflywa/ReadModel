namespace Repository
{
    public class SourceEntry
    {
        public long SequenceId { get; }
        public string Message { get; }

        public SourceEntry(long sequenceId, string message)
        {
            SequenceId = sequenceId;
            Message = message;
        }
    }
}