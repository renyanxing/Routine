using Routine.Api.Entities;
using Routine.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.ValidationAttributes
{
    public class EmployeeNoMustDifferentFromFirstNameAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var addDto = validationContext.ObjectInstance as EmployeeAddOrUpdateDto;
            if (addDto.employeeNo == addDto.firstName)
            {
                return new ValidationResult(ErrorMessage, new[] { nameof(EmployeeAddOrUpdateDto) });
            }
            else
            {
                return ValidationResult.Success;
            }

        }
    }
}
