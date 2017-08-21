using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using ReadModel.Events;

namespace EventReader
{
    public static class Reader
    {
        public static IEnumerable<IEvent> Read(this IDataSource source)
        {
            var sequenceId = -1L;
            var entries = source.ExecuteQuery(sequenceId);
            foreach (var entry in entries)
            {
                sequenceId = entry.SequenceId;
                yield return entry.Deserialize();
            }
        }

        // Overloaded to allow selection of starting SequenceId. First Customer imported starts at 11927
        public static IEnumerable<IEvent> Read(this IDataSource source, long initSequenceId)
        {
            var sequenceId = initSequenceId;
            var entries = source.ExecuteQuery(sequenceId);
            foreach (var entry in entries)
            {
                sequenceId = entry.SequenceId;
                yield return entry.Deserialize();
            }
        }

        private static IEvent Deserialize(this EventEntry entry)
        {
            return entry.DeserializeEntry().AddEventKey();
        }

        private static Tuple<EventEntry, IEvent> DeserializeEntry(this EventEntry entry)
        {
            if (entry.AggregateTypeId == 11 && (entry.MessageTypeId == 1 || entry.MessageTypeId == 16))
            {
                var customerEvent = JsonConvert.DeserializeObject<CustomerCreatedEvent>(entry.Payload);
                return Tuple.Create(entry, (IEvent)customerEvent);
            }
            if (entry.AggregateTypeId == 12)
            {
                switch (entry.MessageTypeId)
                {
                    case 83:
                    case 84:
                    case 85:
                    case 87:
                    case 92:
                        var bankingEvent = JsonConvert.DeserializeObject<BankingPaymentEvent>(entry.Payload);
                        return Tuple.Create(entry, (IEvent)bankingEvent);
                    case 89:
                        var directDebitEvent = JsonConvert.DeserializeObject<DirectDebitRepaymentEvent>(entry.Payload);
                        return Tuple.Create(entry, (IEvent)directDebitEvent);
                    default:
                        throw new NotImplementedException("This type of event is not implemented.");
                }
            }
            throw new NotImplementedException("This type of event is not implemented.");
        }

        private static IEvent AddEventKey(this Tuple<EventEntry, IEvent> tuple)
        {
            var e = tuple.Item2;
            e.SequenceId = tuple.Item1.SequenceId;
            e.Key = new EventKey(tuple.Item1.AggregateTypeId, tuple.Item1.MessageTypeId);
            return e;
        }
    }
}
