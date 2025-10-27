using FloorPlanApplication.Dtos.Company;
using FloorPlanApplication.Interfaces;
using FloorPlanApplication.Mappers;
using FloorPlanApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;

namespace FloorPlanApplication.Controllers
{
    [Route("api/Company")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int? index)
        {
            var list = await _companyRepository.GetCompanies(index ?? 0, 10);

            var Companies = list.Select(c => c.ToCompanyDTO());

            return Ok(Companies);
        }

        [HttpPost]
        [Route("AddCompany")]
        public async Task<IActionResult> AddCompany([FromBody] CreateCompanyDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Company company = DTO.ToCompanyFromCreateDTO();

            bool added =  _companyRepository.AddCompany(company);

            if (!added)
                return BadRequest();

            return CreatedAtAction(nameof(GetCompanyDetails), new { ID = company.ID }, company.ToCompanyDTO());
        }

        [HttpPut]
        [Route("UpdateCompany/{ID:int}")]
        public async Task<IActionResult> UpdaetCompany([FromRoute] int ID, [FromBody] UpdateCompanyDTO DTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Company Company = await _companyRepository.GetCompanyByID(ID);

            if (Company == null)
                return NotFound();

            Company.PhoneNumber = DTO.PhoneNumber;
            Company.Address = DTO.Address;
            Company.CompanyName = DTO.CompanyName;

            bool saved = _companyRepository.UpdateCompany(Company);

            if (!saved)
                return BadRequest();

            return Ok(Company.ToCompanyDTO());
        }

        [HttpDelete]
        [Route("DeleteCompany/{ID:int}")]
        public async Task<IActionResult> DeleteCompany([FromRoute] int ID)
        {
            Company company = await _companyRepository.GetCompanyByID(ID);

            if (company == null)
                return NotFound();

            bool deleted = _companyRepository.DeleteCompany(company);

            if (!deleted)
                return BadRequest();

            return NoContent();
        }

        [HttpGet("{ID:int}")]
        public async Task<IActionResult> GetCompanyDetails([FromRoute] int ID)
        {
            Company Company = await _companyRepository.GetCompanyByID(ID);

            if(Company == null)
                return NotFound();

            return Ok(Company.ToCompanyDTO());
        }

        [HttpGet]
        [Route("GetCompaniesByName/{name}")]
        public async Task<IActionResult> GetCompaniesByName([FromRoute] string name)
        {
            if (!ModelState.IsValid || name.Equals(null) || name.Length < 3)
                return BadRequest(ModelState);

            //TODO: check special symbols using regex

            var list = await _companyRepository.GetCompanyByName(name);

            if(list.Count() == 0)
            {
                return NotFound();
            }

            var Companies = list.Select(c => c.ToCompanyDTO());

            return Ok(Companies);
        }

        [HttpGet]
        [Route("GetCompanyByPhoneNUmber/{number}")]
        public async Task<IActionResult> GetCompanyByPhoneNUmber([FromRoute] string number)
        {
            Regex rg = new Regex(@"\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})");
            Match match = rg.Match(number);

            if (!ModelState.IsValid || !match.Success)
                return BadRequest();

            Company company = await _companyRepository.GetCompanyByPhoneNumber(number);

            if (company == null)
                return NotFound();

            return Ok(company.ToCompanyDTO());
        }
    }
}
