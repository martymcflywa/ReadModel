using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventReader;
using Persistence;
using ReadModel;
using ReadModel.Models.CustomerPayment;
using WebAPI.Interfaces;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class PaymentsByCustomerByDateService : IReadModelService
    {
        private const string ConnectionString = @"Server=AUPERPSVSQL07;Database=EventHub.OnPrem;Trusted_Connection=True;";
        private const string Path = @"C:\source\ReadModel\Samples";
        private const int WritePageSize = 70000;

        private SqlSource _source;
        private PaymentsByCustomerByDateProcessor _processor;
        private EventDispatcher _dispatcher;

        public IEnumerable<IReadModel> Get()
        {
            if (_processor == null)
            {
                InitService();
            }

            foreach (var result in GetResults())
            {
                yield return new HighestPayingCustomer(
                    result.Value.YearMonth,
                    result.Value.Customer.CustomerId,
                    result.Value.Customer.FirstName,
                    result.Value.Customer.Surname,
                    result.Value.Customer.AmountPaid);
            }
        }

        private void InitService()
        {
            _source = new SqlSource(ConnectionString);
            var modelStore = new ModelStore(Path, WritePageSize);
            _dispatcher = new EventDispatcher();
            _processor = new PaymentsByCustomerByDateProcessor(modelStore);
            _processor.Register(_dispatcher);
        }

        private Dictionary<DateTime, MonthlyResult> GetResults()
        {
            _dispatcher.Dispatch(_source.Read(_processor.InitSequenceId));
            return _processor.GetHighestPayingCustomers();
        }
    }
}
