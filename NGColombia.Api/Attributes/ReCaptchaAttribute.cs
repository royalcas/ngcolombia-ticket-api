using NGColombia.Api.Service;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Attributes
{
    public class ReCaptchaAttribute: ValidationAttribute
    {       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var validator = (IRecaptchaTokenValidator)validationContext
                         .GetService(typeof(IRecaptchaTokenValidator));

            var token = value.ToString();
            var success = validator.IsValidToken(token).GetAwaiter().GetResult();

            if (success)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("The captcha is not valid");
            }
        }
    }
}
