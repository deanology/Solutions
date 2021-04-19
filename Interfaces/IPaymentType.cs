using Solution.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Interfaces
{
    public interface IPaymentType
    {
        bool AddPaymentType(PaymentType model);
    }
}
