namespace EventReader.Read
{
    public class EventEntry
    {
        public long SequenceId { get; }
        public short AggregateTypeId { get; }
        public short MessageTypeId { get; }
        public string Message { get; }

        public EventEntry(long sequenceId, short aggregateTypeId, short messageTypeId, string message)
        {
            SequenceId = sequenceId;
            AggregateTypeId = aggregateTypeId;
            MessageTypeId = messageTypeId;
            Message = message;
        }
    }
}