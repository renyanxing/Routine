using Routine.Api.Entities;
using Routine.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.ValidationAttributes
{
    public class EmployeeGenderValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addDto = validationContext.ObjectInstance as EmployeeAddOrUpdateDto;
            if (addDto.Gender != (Gender)1 && addDto.Gender != (Gender)2)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(Gender) });
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}
