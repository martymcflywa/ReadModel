using ReadModel.Events;

namespace EventReader
{
    public interface IDeserialize
    {
        IEvent DeserializeEntry(EventEntry entry);
    }
}