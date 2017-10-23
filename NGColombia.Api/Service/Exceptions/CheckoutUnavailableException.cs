using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Service.Exceptions
{
    public class CheckoutUnavailableException: Exception
    {
        public CheckoutUnavailableException(string message)
            :base(message)
        {
        }
    }
}
