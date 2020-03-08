using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Entities;
using Routine.Api.Models;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Routine.Api.Parameters;
using Routine.Api.Helpers;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route(baseUri)]
    public class EmployeesController : ControllerBase
    {
        private const string baseUri = "api/companies/{companyId}/employees";
        private readonly IMapper mapper;
        private readonly ICompanyRespository companyRespository;

        public EmployeesController(IMapper mapper, ICompanyRespository companyRespository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.companyRespository = companyRespository ?? throw new ArgumentNullException(nameof(companyRespository));
        }
        [HttpGet(Name = nameof(GetEmployeesForCompany))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId, [FromQuery]EmployeeParameters parameters)
        {
            if (!await this.companyRespository.CompanyExistsAsync(companyId))
                NotFound();


            var employees = await this.companyRespository.GetEmployeesAsync(companyId, parameters);
            var previousPageLink = employees.HasPrevious
    ? CreateEmployeesResourceUri(parameters, ResourceUriType.PriviousPage)
    : null;
            var nextPageLink = employees.HasNext
                ? CreateEmployeesResourceUri(parameters, ResourceUriType.NextPage)
                : null;


            var employeeDtos = mapper.Map<IEnumerable<EmployeeDto>>(employees);
            return Ok(employeeDtos);

        }
        [HttpGet("{EmployeeId}", Name = nameof(GetEmployeeForCompany))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await this.companyRespository.CompanyExistsAsync(companyId))
                NotFound();
            var employee = await this.companyRespository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
                return NotFound();
            var employeeDto = mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);

        }
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDto employee)
        {
            if (!await this.companyRespository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var entity = this.mapper.Map<Employee>(employee);
            this.companyRespository.AddEmployee(companyId, entity);
            await this.companyRespository.SaveAsync();
            var dto = this.mapper.Map<EmployeeDto>(entity);
            return CreatedAtRoute(nameof(GetEmployeeForCompany), new { companyId = companyId, employeeId = dto.Id }, dto);
        }
        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeForCompany(Guid companyId, Guid employeeId, EmployeeUpdateDto employee)
        {
            if (!await this.companyRespository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employeeEntity = await this.companyRespository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                var employeeToAddEntity = this.mapper.Map<Employee>(employee);
                employeeToAddEntity.Id = employeeId;
                this.companyRespository.AddEmployee(companyId, employeeToAddEntity);

                await this.companyRespository.SaveAsync();
                var dto = this.mapper.Map<EmployeeDto>(employeeToAddEntity);
                return CreatedAtRoute(nameof(GetEmployeeForCompany), new { companyId = companyId, employeeId = dto.Id }, dto);
            }
            this.mapper.Map(employee, employeeEntity);
            this.companyRespository.UpdateEmployee(employeeEntity);
            await this.companyRespository.SaveAsync();
            return NoContent();

        }
        [HttpPatch("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> PartiallyUpdateEmployeeForCompany(Guid companyId, Guid employeeId, JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            if (!await this.companyRespository.CompanyExistsAsync(companyId))
            {
                return NotFound();
            }
            var employeeEntity = await this.companyRespository.GetEmployeeAsync(companyId, employeeId);
            if (employeeEntity == null)
            {
                var employeeDto = new EmployeeUpdateDto();
                patchDocument.ApplyTo(employeeDto, ModelState);
                if (!TryValidateModel(employeeDto))
                {
                    return ValidationProblem(ModelState);
                }
                else
                {
                    var employeeToAdd = this.mapper.Map<Employee>(employeeDto);
                    employeeToAdd.Id = employeeId;
                    this.companyRespository.AddEmployee(companyId, employeeToAdd);
                    await this.companyRespository.SaveAsync();
                    var dto = this.mapper.Map<EmployeeDto>(employeeToAdd);
                    return CreatedAtRoute(nameof(GetEmployeeForCompany),
                        new { companyId = companyId, employeeId = dto.Id }, dto);
                }
            }
            var dtoToPatch = this.mapper.Map<EmployeeUpdateDto>(employeeEntity);

            patchDocument.ApplyTo(dtoToPatch, ModelState);

            if (!TryValidateModel(dtoToPatch))
            {
                //return UnprocessableEntity(ModelState);
                //return new UnprocessableEntityObjectResult(ModelState);
                return ValidationProblem(ModelState);
            }
            this.mapper.Map(dtoToPatch, employeeEntity);
            this.companyRespository.UpdateEmployee(employeeEntity);
            await this.companyRespository.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await this.companyRespository.CompanyExistsAsync(companyId))
            {
                NotFound();
            }
            var employee = await this.companyRespository.GetEmployeeAsync(companyId, employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            this.companyRespository.DeleteEmployee(employee);
            await this.companyRespository.SaveAsync();
            return NoContent();
        }

        public override ActionResult ValidationProblem(ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();
            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
        private string CreateEmployeesResourceUri(EmployeeParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.NextPage:
                    {
                        return Url.Link(nameof(GetEmployeesForCompany), new
                        {
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize,
                            searchQuery = parameters.SearchQuery
                        });
                    }   
                case ResourceUriType.PriviousPage:
                    {
                        return Url.Link(nameof(GetEmployeesForCompany), new
                        {
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize,

                            searchQuery = parameters.SearchQuery
                        }); ;
                    }
                default:
                    {
                        return Url.Link(nameof(GetEmployeesForCompany), new
                        {
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber,
                            pageSize = parameters.PageSize,
                            searchQuery = parameters.SearchQuery
                        });
                    }
            }
        }
    }


}
