using System;
using System.Collections.Generic;
using System.Text;
using ReadModel.Models.CustomerPayment;

namespace ReadModel
{
    public interface IPersist
    {
        void Write(PaymentsByYearByMonthModel model, string path, string filename);
    }
}
