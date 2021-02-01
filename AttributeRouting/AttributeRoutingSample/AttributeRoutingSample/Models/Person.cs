using AttributeRoutingSample.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AttributeRoutingSample.Models
{
    public class Person : IValidatableObject
    {
        public Guid SchoolId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Error - Age is required property.")]
        public int Age { get; set; }

        public bool IsOfAge { get; set; }

        [Required]
        public string DateOfPrimarySchoolGraduation { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(SchoolId == default)
            {
                yield return new ValidationResult("Not valid SchoolId");
            }

            if (IsOfAge && Age < PersonConstant.MinimumAgeForBeingAdult)
            {
                yield return new ValidationResult("Incorrect age. If Person is of Age, they must set Age in minimum value 18");
            }

            if(!IsOfAge && Age >= PersonConstant.MinimumAgeForBeingAdult)
            {
                yield return new ValidationResult("Incorrect age. If Person is not of Age, they must set Age less than 18");
            }

            if (!DateTime.TryParse(DateOfPrimarySchoolGraduation, out var dateOfGraduation))
            {
                yield return new ValidationResult("DateOfGraduation is not in date format");
            }

            if(dateOfGraduation <= DateTime.Now.AddYears(-PersonConstant.MinimumYearGapFromGraduation))
            {
                yield return new ValidationResult("DateOfGraduation cannot be more than one year");
            }
        }
    }
}
