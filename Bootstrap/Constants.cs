using System;
using System.Collections.Generic;
using System.Text;

namespace Bootstrap
{
    [Obsolete]
    public class Constants
    {
        /// <summary>
        /// Class is obsolete, just keeping around to refer to connection string and queries. To be deleted.
        /// </summary>
        const string CONNECTION_STRING = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";

        const string CUSTOMER_QUERY =
                "select top 100 * " +
                "from MessageHub.Payload as t0 " +
                "join MessageHub.MessageContent as t1 " +
                "on t0.SequenceId = t1.SequenceId " +
                "where t0.MessageTypeId = 1 and t0.AggregateTypeId = 11 " +
                "and t0.SequenceId > @sequenceId ";

        const string REPAYMENT_QUERY =
                "select top 100 * " +
                "from MessageHub.Payload as t0 " +
                "join MessageHub.MessageContent as t1 " +
                "on t0.sequenceId = t1.sequenceId " +
                "where MessageTypeId in (83, 84, 85, 87, 89, 92) and " +
                "t0.AggregateTypeId = 12 and " +
                "t0.SequenceId > @sequenceId ";
    }
}
