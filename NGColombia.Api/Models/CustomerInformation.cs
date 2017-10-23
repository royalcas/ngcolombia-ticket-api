using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Models
{
    public class Customer: Entity
    {
        public string Name { get; set; }
        public string IdentificationNumber { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<Transaction> Transactions { get; set; }
    }
}
