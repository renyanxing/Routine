using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Routine.Api.Parameters
{
    public class CompanyParameters : IValidatableObject
    {
        private const int MaxPageSize = 20;
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 5;
        public string OrderBy { get; set; } = "CompanyName";
        public int PageSize
        {
            get => this._pageSize;
            set => this._pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public string Fields { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (PageNumber < 1)
            {
                yield return new ValidationResult("每页显示条数不得小于1", new[] { nameof(PageSize) });
            }
            if (PageSize < 1)
            {
                yield return new ValidationResult("页码不得小于1", new[] { nameof(PageSize) });
            }
        }
    }
}
