using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Models
{
    public class EmpolyeeDto { 
         
        public Guid Id { get; set; }
        public Guid companyId { get; set; }
        public string employeeNo { get; set; }
        public string Name { get; set; }

        public string GenderDisplay { get; set; }
        public int age { get; set; }

    }
}
