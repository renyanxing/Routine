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
using Routine.Api.Models;

namespace Routine.Api.Services
{
    public class CompanyRespository : ICompanyRespository
    {
        private readonly RoutineDbContext routineDbContext;
        private readonly IPropertyMappingService _propertyMappingService;

        public CompanyRespository(RoutineDbContext routineDbContext,IPropertyMappingService propertyMappingService)
        {
            this.routineDbContext = routineDbContext ?? throw new ArgumentNullException(nameof(routineDbContext));
            _propertyMappingService = propertyMappingService ?? throw new ArgumentNullException(nameof(propertyMappingService));
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
            var queryExpression = this.routineDbContext.Companies as IQueryable<Company>;
            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                parameters.SearchQuery = parameters.SearchQuery.Trim();
                queryExpression = queryExpression.Where(x => x.Name.Contains(parameters.SearchQuery) || x.Introduction.Contains(parameters.SearchQuery));
            }
            var mappingDictionary = this._propertyMappingService.GetPropertyMapping<CompanyDto, Company>();
            queryExpression = queryExpression.ApplySort(parameters.OrderBy, mappingDictionary);
            return await PageList<Company>.CreateAsync(queryExpression, parameters.PageNumber, parameters.PageSize);
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

        public async Task<PageList<Employee>> GetEmployeesAsync(Guid companyId, EmployeeParameters parameters)
        {
            if (companyId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(companyId));
            }
                


            var items = this.routineDbContext.Employees.Where(e => e.companyId == companyId);

            if (!string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                parameters.SearchQuery = parameters.SearchQuery.Trim();
                items = items.Where(x => x.firstName.Contains(parameters.SearchQuery) || x.lastName.Contains(parameters.SearchQuery)|| x.employeeNo.Contains(parameters.SearchQuery));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Gender))
                items = items.Where(e => e.Gender == Enum.Parse<Gender>(parameters.Gender.Trim()));
            if (!string.IsNullOrWhiteSpace(parameters.Q))
            {
                parameters.Q = parameters.Q.Trim();
                items = items.Where(e => e.employeeNo.Contains(parameters.Q) || e.firstName.Contains(parameters.Q) || e.lastName.Contains(parameters.Q));
            }

            var mappingDictionary = _propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();
            items = items.ApplySort(parameters.OrderBy, mappingDictionary);

            return await PageList<Employee>.CreateAsync(items, parameters.PageNumber, parameters.PageSize);




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
