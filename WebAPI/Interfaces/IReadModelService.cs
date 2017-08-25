using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReadModel.Models.CustomerPayment;

namespace WebAPI.Interfaces
{
    public interface IReadModelService
    {
        IEnumerable<IReadModel> Get();
    }
}
