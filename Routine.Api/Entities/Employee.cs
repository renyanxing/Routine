using System;

namespace Routine.Api.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public Guid companyId { get; set; }
        public string employeeNo { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime dateOfBirth { get; set; }
        public Company Company { get; set; }


    }
}