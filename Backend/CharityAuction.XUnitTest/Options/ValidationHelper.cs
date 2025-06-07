using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CharityAuction.Infrastructure.Tests.Options
{
    public static class ValidationHelper
    {
        public static IList<ValidationResult> ValidateObject(object obj)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(obj);
            Validator.TryValidateObject(obj, context, results, validateAllProperties: true);
            return results;
        }
    }
}
