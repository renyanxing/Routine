using Routine.Api.Entities;
using Routine.Api.ValidationAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Models
{
    [EmployeeDateOfBirthArea(ErrorMessage ="出生日期必须在1900-01-01 至 今")]
    [EmployeeGenderValidation(ErrorMessage ="性别不能为空或者值必须为1或2")]
    [EmployeeNoMustDifferentFromFirstNameAttribute(ErrorMessage = "员工号不能和姓名一样！")]
    abstract public class EmployeeAddOrUpdateDto : IValidatableObject
    {
        [Display(Name = "员工号")]
        [Required(ErrorMessage = "{0}是必填项")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "{0}长度为{1}")]
        public string employeeNo { get; set; }
        [Display(Name = "姓氏")]
        [MaxLength(20, ErrorMessage = "{0}长度不能超过{1}")]
        [Required(ErrorMessage = "{0}是必填项")]
        public string firstName { get; set; }
        [Display(Name = "名字")]
        [Required(ErrorMessage = "{0}是必填项"), MaxLength(20, ErrorMessage = "{0}长度不能超过{1}")]
        public string lastName { get; set; }
        [Display(Name = "性别")]
        public Gender Gender { get; set; }
        [Display(Name = "出生日期")]
        public DateTime dateOfBirth { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (firstName == lastName)
            {
                yield return new ValidationResult("姓和名不可以一样！"
                    //,new[] { nameof(firstName) , nameof(lastName)
                    , new[] { nameof(firstName) , nameof(lastName)
                    });
            }
        }
    }
}
