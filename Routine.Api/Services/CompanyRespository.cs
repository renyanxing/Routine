using Microsoft.EntityFrameworkCore;
using Routine.Api.Data;
using Routine.Api.Entities;
using Routine.Api.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Routine.Api.Helpers;

namespace Routine.Api.Services
{
    public class CompanyRespository : ICompanyRespository
    {
        private readonly RoutineDbContext routineDbContext;

        public CompanyRespository(RoutineDbContext routineDbContext)
        {
            this.routineDbContext = routineDbContext ?? throw new ArgumentNullException(nameof(routineDbContext));
        }
        public void AddCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));
            company.Id = Guid.NewGuid();
            if (company.Employees != null)
            {
                foreach (var item in company.Employees)
                {
                    item.Id = Guid.NewGuid();
                }
            }
            this.routineDbContext.Companies.Add(company);
        }

        public void AddEmployee(Guid companyId, Employee employee)
        {
            if (companyId == Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            if (employee == null)
                throw new ArgumentNullException(nameof(employee));
            employee.companyId = companyId;
            this.routineDbContext.Add(employee);
        }

        public async Task<bool> CompanyExistsAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            return await this.routineDbContext.Companies.AnyAsync(c => c.Id == companyId);


        }

        public void DeleteCompany(Company company)
        {
            if (company == null)
                throw new ArgumentNullException(nameof(company));
            this.routineDbContext.Companies.Remove(company);
        }

        public void DeleteEmployee(Employee emplpyee)
        {
            if (emplpyee == null)
                throw new ArgumentNullException(nameof(emplpyee));
            this.routineDbContext.Remove(emplpyee);
        }

        public async Task<PageList<Company>> GetCompaniesAsync(CompanyParameters parameters)
        {
            if (parameters == null)
            {
                throw new ArgumentNullException(nameof(parameters));
            }
            var queryExperssion = this.routineDbContext.Companies as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace(parameters.CompanyName))
            {
                queryExperssion = queryExperssion.Where(x => x.Name == parameters.CompanyName.Trim());
            }
            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                parameters.SearchQuery = parameters.SearchQuery.Trim();
                queryExperssion = queryExperssion.Where(x => x.Name.Contains(parameters.SearchQuery) || x.Introduction.Contains(parameters.SearchQuery));
            }

            return await PageList<Company>.CreateAsync(queryExperssion, parameters.PageNumber, parameters.PageSize);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(IEnumerable<Guid> companyIds)
        {
            if (companyIds == null)
                throw new ArgumentNullException(nameof(companyIds));
            return await this.routineDbContext.Companies.Where(x => companyIds.Contains(x.Id)).OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<Company> GetCompanyAsync(Guid companyId)
        {
            if (companyId == Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            return await this.routineDbContext.Companies.FirstOrDefaultAsync(c => c.Id == companyId);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesAsync(Guid companyId, string displayGender, string searchQuery)
        {
            if (companyId == Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            var employees = await this.routineDbContext.Employees.ToListAsync();
            if (string.IsNullOrWhiteSpace(displayGender) && string.IsNullOrWhiteSpace(searchQuery))
                return employees.Where(e => e.companyId == companyId).OrderBy(x => x.employeeNo);
            var items = this.routineDbContext.Employees.Where(e => e.companyId == companyId);

            if (!string.IsNullOrWhiteSpace(displayGender))
                items = items.Where(e => e.Gender == Enum.Parse<Gender>(displayGender.Trim()));
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                items = items.Where(e => e.employeeNo.Contains(searchQuery) || e.firstName.Contains(searchQuery) || e.lastName.Contains(searchQuery));
            }
            return await items.OrderBy(x => x.employeeNo).ToListAsync();




        }
        public async Task<Employee> GetEmployeeAsync(Guid companyId, Guid employeeId)
        {
            if (companyId == Guid.Empty)
                throw new ArgumentNullException(nameof(companyId));
            if (employeeId == Guid.Empty)
                throw new ArgumentNullException(nameof(employeeId));

            return await this.routineDbContext.Employees.FirstOrDefaultAsync(e => e.companyId == companyId && e.Id == employeeId);
        }


        public async Task<bool> SaveAsync()
        {
            return await this.routineDbContext.SaveChangesAsync() >= 0;
        }

        public void UpdateCompany(Company company)
        {
            //this.routineDbContext.Entry(company).State = EntityState.Modified;

        }

        public void UpdateEmployee(Employee emplpyee)
        {
            //this.routineDbContext.Entry(company).State = EntityState.Modified;
        }
    }
}
