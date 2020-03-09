using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/company/v1")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileController() 
        {
            var repo = new EFGenericRepository<CompanyProfilePoco>();
            _logic = new CompanyProfileLogic(repo);
        }

        [HttpGet]
        [Route("company/{id}")]
        public ActionResult GetCompanyProfile(Guid id) 
        {
            CompanyProfilePoco poco = _logic.Get(id);
            if (poco == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(poco);
            }
        }

        [HttpGet]
        [Route("company")]
        public ActionResult GetAllCompanyProfile() 
        {
            var applicants = _logic.GetAll();

            if (applicants == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(applicants);
            }
        }

        [HttpPost]
        [Route("company")]
        public ActionResult PostCompanyProfile( 
            [FromBody]CompanyProfilePoco[] pocos)
        {
            _logic.Add(pocos);

            return Ok();
        }

        [HttpPut]
        [Route("company")]
        public ActionResult PutCompanyProfile( 
            [FromBody]CompanyProfilePoco[] pocos) 
        {
            _logic.Update(pocos);

            return Ok();
        }

        [HttpDelete]
        [Route("company")]
        public ActionResult DeleteCompanyProfile(
    [FromBody]CompanyProfilePoco[] pocos) 
        {
            _logic.Delete(pocos);

            return Ok();
        }

    }
}