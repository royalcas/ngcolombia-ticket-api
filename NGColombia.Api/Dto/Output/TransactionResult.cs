using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Dto.Output
{
    public class TransactionResult
    {
        public bool Success { get; set; }
        public bool Pending { get; set; }
        public string Message { get; set; }
    }
}
