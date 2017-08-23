using System;
using System.Collections.Generic;
using ReadModel.Events;

namespace ReadModel.Models.CustomerPayment
{
    public class PaymentsByYearByMonthModel : IModel
    {
        private readonly IPersist _modelStore;
        public long CurrentSequenceId { get; private set; }
        public DateTimeOffset ModelCreatedDate { get; }
        public string Filename { get; }
        public Dictionary<DateTime, PaymentsByMonth> Years { get; }

        public PaymentsByYearByMonthModel(IPersist modelStore)
        {
            _modelStore = modelStore;
            Filename = "PaymentsByYearByMonth.json";
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
            _modelStore.Write(this, Filename);
        }
    }
}
