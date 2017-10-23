using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using NGColombia.Api.Attributes;

namespace NGColombia.Api.Dto.Input
{
    public class TransactionInputModel
    {
        [StringLength(60, MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)]
        [Required]
        public string CustomerId { get; set; }

        [StringLength(255, MinimumLength = 3)]
        [DataType(DataType.Text)]
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [MustHaveOneElementAttribute(ErrorMessage = "At least a ticket is required")]
        public ICollection<TicketInputModel> Tickets { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [StringLength(255, MinimumLength = 3)]
        [EmailAddress()]
        [Compare("Email", ErrorMessage = "Email and confirmation do not match")]
        public string ConfirmationEmail { get; set; }

        [StringLength(500, MinimumLength = 3)]
        [Required]
        [ReCaptchaAttribute]
        public string TokenCaptcha { get; set; }

        [StringLength(100, MinimumLength = 3)]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhoneNumber { get; set; }
    }

    public class TicketInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string TicketCode { get; set; }
        
        [Range(1, 3, ErrorMessage = "It is not possible to buy more that 3 tickets per requests")]
        public int Quantity { get; set; }

        public TicketInputModel()
        {
            Quantity = 1;
        }
    }
}
