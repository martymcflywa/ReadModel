using System;
using System.Collections.Generic;
using System.Text;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public interface IModel
    {
        long CurrentSequenceId { get; }
        DateTimeOffset ModelCreatedDate { get; }
        void AddEvent(IEvent e);
    }
}
