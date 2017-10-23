using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NGColombia.Api.Attributes
{
    public class MustHaveOneElementAttribute: ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            var list = value as ICollection;

            if (list != null)
            {
                return list.Count > 0;
            }

            return false;
        }
    }
}
