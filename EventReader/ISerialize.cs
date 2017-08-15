using ReadModel.Events;

namespace EventReader
{
    public interface ISerialize : IEventStream
    {
        IEvent DeserializeEntry(EventEntry entry);
    }
}