using Routine.Api.Entities;
using Routine.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.ValidationAttributes
{
    public class EmployeeDateOfBirthArea : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dto = (EmployeeAddOrUpdateDto)validationContext.ObjectInstance;
            if (dto.dateOfBirth >= Convert.ToDateTime("1900-01-01") && dto.dateOfBirth <= DateTime.Now)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage, new [] { nameof(dto.dateOfBirth) });
            }
        }
    }
}
