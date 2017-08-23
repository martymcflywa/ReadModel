using System;
using ReadModel.Events;

namespace ReadModel.Models
{
    public interface IModel
    {
        string Filename { get; }
        long CurrentSequenceId { get; }
        DateTimeOffset ModelCreatedDate { get; }
        void AddEvent(IEvent e);
    }
}
