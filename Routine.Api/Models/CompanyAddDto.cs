using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Models
{
    public class CompanyAddDto
    {
        [Display(Name = "公司名称")]
        [Required(ErrorMessage = "{0}字段不能为空！")]
        [MaxLength(100, ErrorMessage = "{0}最大长度不能超过{1}")]
        public string Name { get; set; }


        [StringLength(500, MinimumLength = 10, ErrorMessage = "{0}长度范围从{2}到{1}")]
        public string Introduction { get; set; }
        public ICollection<EmployeeAddDto> Employees { get; set; } = new List<EmployeeAddDto>();

    }
}
