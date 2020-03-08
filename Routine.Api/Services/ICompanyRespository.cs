using Routine.Api.Entities;
using Routine.Api.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Routine.Api.Helpers;

namespace Routine.Api.Services
{
    public interface ICompanyRespository
    {
        Task<PageList<Company>> GetCompaniesAsync(CompanyParameters parameters);
        Task<Company> GetCompanyAsync(Guid companyId);
        Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds);
        void AddCompany(Company company);
        void UpdateCompany(Company company);
        void DeleteCompany(Company company);
        Task<bool> CompanyExistsAsync(Guid companyId);


        Task<PageList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters parameters);
        Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId);
        void AddEmployee(Guid companyId, Employee emplpyee);
        void UpdateEmployee(Employee emplpyee);
        void DeleteEmployee(Employee emplpyee);
        Task<bool> SaveAsync();
    }
}
