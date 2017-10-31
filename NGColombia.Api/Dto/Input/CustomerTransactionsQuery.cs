using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Dto.Input
{
    public class CustomerTransactionsQuery
    {
        [StringLength(255)]
        [RegularExpression("^([a-zA-Z0-9 .&'-]+)$", ErrorMessage = "Invalid Query")]
        [Required]
        public string query { get; set; }
    }
}
