using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Entities;
using Routine.Api.Models;
using Routine.Api.Parameters;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Routine.Api.Helpers;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route(baseUri)]
    public class CompaniesController : ControllerBase
    {
        private const string baseUri = "api/companies";
        private readonly ICompanyRespository companyRespository;
        private readonly IMapper mapper;

        public CompaniesController(ICompanyRespository companyRespository, IMapper mapper)
        {
            this.companyRespository = companyRespository ?? throw new ArgumentNullException(nameof(companyRespository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        [HttpGet(Name = nameof(GetCompanies))]
        [HttpHead]
        public async Task<IActionResult> GetCompanies([FromQuery]CompanyParameters parameters)
        {
            var companies = await this.companyRespository.GetCompaniesAsync(parameters);

            var previousPageLink = companies.HasPrevious
                ? CreateCompaniesResourceUri(parameters, ResourceUriType.PriviousPage)
                : null;
            var nextPageLink = companies.HasNext
                ? CreateCompaniesResourceUri(parameters, ResourceUriType.NextPage)
                : null;
            var paginationMetaData = new
            {
                totalCount = companies.TotalCount,
                pageSize = companies.PageSize,
                currentPage = companies.CurrentPage,
                totalPage = companies.TotalPages,
                previousPageLink,
                nextPageLink
            };
            Response.Headers.Add("X-Pagination", System.Text.Json.JsonSerializer.Serialize(paginationMetaData, new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            }));
            var companyDtos = mapper.Map<IEnumerable<CompanyDto>>(companies);
            return Ok(companyDtos.ShapeData(parameters.Fields)); 
        }
        [HttpGet("{companyId}", Name = nameof(GetCompany))]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
        {
            //var exist = await this.companyRespository.CompanyExistsAsync(companyId);
            //if (exist)
            //    return NotFound();
            var company = mapper.Map<CompanyDto>(await this.companyRespository.GetCompanyAsync(companyId));

            if (company == null)
                return NotFound();
            return Ok(company);
        }
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany([FromBody]CompanyAddDto company)
        {
            var entity = this.mapper.Map<Company>(company);
            this.companyRespository.AddCompany(entity);
            await this.companyRespository.SaveAsync();
            var returnDto = this.mapper.Map<CompanyDto>(entity);
            return CreatedAtRoute(nameof(GetCompany), new { companyId = returnDto.Id }, returnDto);
        }

        [HttpDelete("{companyId}")]
        public async Task<IActionResult> DeleteForCompanyId(Guid companyId)
        {
            var companyEntity = await this.companyRespository.GetCompanyAsync(companyId);
            if (companyEntity == null)
            {
                return NotFound();
            }

            await companyRespository.GetEmployeesAsync(companyId, null);
            this.companyRespository.DeleteCompany(companyEntity);
            await this.companyRespository.SaveAsync();
            return NoContent();

        }

        private string CreateCompaniesResourceUri(CompanyParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.NextPage:
                    {
                        return Url.Link(nameof(GetCompanies), new
                        {
                            fields=parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber + 1,
                            pageSize = parameters.PageSize,
                            searchQuery = parameters.SearchQuery
                        });
                    }
                case ResourceUriType.PriviousPage:
                    {
                        return Url.Link(nameof(GetCompanies), new
                        {
                            ields = parameters.Fields,
                            orderBy = parameters.OrderBy,
                            pageNumber = parameters.PageNumber - 1,
                            pageSize = parameters.PageSize,
                            searchQuery = parameters.SearchQuery
                        }); ;
                    }
                default:
                    {
                        return Url.Link(nameof(GetCompanies), new
                        {
                            ields = parameters.Fields,
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
