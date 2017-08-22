using System;
using ReadModel.Events;

namespace ReadModel.Models
{
    public interface IModel
    {
        long CurrentSequenceId { get; }
        DateTimeOffset ModelCreatedDate { get; }
        void AddEvent(IEvent e);
    }
}
