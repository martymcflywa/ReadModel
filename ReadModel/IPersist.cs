using System;
using System.Collections.Generic;
using System.Text;
using ReadModel.Models.CustomerPayment;

namespace ReadModel
{
    public interface IPersist
    {
        void Write(IModel model, string path, string filename);
    }
}
