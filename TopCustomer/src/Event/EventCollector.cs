using System.Collections.Generic;

namespace TopCustomer.Event
{
    class EventCollector
    {
        IEnumerable<IEvent> GetCustomerEvents()
        {
            var db = new MessageHub();
        }
    }
}
