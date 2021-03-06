﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ReadModel.Events
{
    public interface IEventStream
    {
        IEnumerable<IEvent> Get();
        IEnumerable<IEvent> Get(long initSequenceId);
    }
}
