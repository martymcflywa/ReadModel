using System;
using System.Collections.Generic;
using System.Linq;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class PaymentsByYearByMonthModel : IModel
    {
        public long CurrentSequenceId { get; set; }
        public DateTimeOffset ModelCreatedDate { get; set; }
        public string Filename { get; set; }
        public Dictionary<DateTime, PaymentsByMonth> Years { get; set; }

        public PaymentsByYearByMonthModel(string filename)
        {
            Filename = filename;
            CurrentSequenceId = 0;
            ModelCreatedDate = DateTimeOffset.Now;
            Years = new Dictionary<DateTime, PaymentsByMonth>();
        }

        /// <summary>
        /// Add payment to model that represents highest paying customer per month, per year.
        /// </summary>
        /// <param name="e"></param>
        public void AddEvent(IEvent e)
        {
            var repaymentEvent = (IRepaymentEvent) e;
            var year = new DateTime(repaymentEvent.GetTransactionDate().Year, 1, 1);
            if (!Years.ContainsKey(year))
            {
                Years.Add(year, new PaymentsByMonth());
            }
            Years[year].Add(repaymentEvent);
            CurrentSequenceId = repaymentEvent.SequenceId;
        }
    }
}
