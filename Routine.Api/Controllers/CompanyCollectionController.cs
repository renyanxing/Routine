using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Routine.Api.Entities;
using Routine.Api.Helpers;
using Routine.Api.Models;
using Routine.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Routine.Api.Controllers
{
    [ApiController]
    [Route("api/cpmpanycollections")]
    public class CompanyCollectionController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICompanyRespository companyRespository;

        public CompanyCollectionController(IMapper mapper, ICompanyRespository companyRespository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.companyRespository = companyRespository ?? throw new ArgumentNullException(nameof(companyRespository));
        }
        [HttpGet("{ids}", Name = nameof(GetCompanyCollection))]
        public async Task<IActionResult> GetCompanyCollection([FromRoute][ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                return BadRequest();
            }

            var entities = await this.companyRespository.GetCompaniesAsync(ids);

            if (ids.Count() != entities.Count())
            {
                return NotFound();
            }
            var dtosToReturn = this.mapper.Map<IEnumerable<CompanyDto>>(entities);
            return Ok(dtosToReturn);

        }



        [HttpPost]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection(IEnumerable<CompanyAddDto> companyCollection)
        {

            var companyEntities = this.mapper.Map<IEnumerable<Company>>(companyCollection);
            foreach (var company in companyEntities)
            {
                this.companyRespository.AddCompany(company);
            }
            await this.companyRespository.SaveAsync();
            var dtos = this.mapper.Map<IEnumerable<CompanyDto>>(companyEntities);
            var idsString = string.Join(",", dtos.Select(x => x.Id));
            return CreatedAtRoute(nameof(GetCompanyCollection), new { ids = idsString }, dtos);
        }
        [HttpOptions]
        public IActionResult GetCompaniesOptions() {

            Response.Headers.Add("allow", "Get,POST,OPTIONS");
            return Ok();
        }

    }
}
