using ReadModel.Events;

namespace EventReader
{
    public class EventEntry
    {
        public long SequenceId { get; }
        public short AggregateTypeId { get; }
        public short MessageTypeId { get; }
        public string Payload { get; }

        public EventEntry(long sequenceId, short aggregateTypeId, short messageTypeId, string payload)
        {
            SequenceId = sequenceId;
            AggregateTypeId = aggregateTypeId;
            MessageTypeId = messageTypeId;
            Payload = payload;
        }
    }
}