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
    public class CompanyJobSkillController : ControllerBase
    {

        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillController() 
        {
            var repo = new EFGenericRepository<CompanyJobSkillPoco>();
            _logic = new CompanyJobSkillLogic(repo);
        }

        [HttpGet]
        [Route("company/{id}")]
        public ActionResult GetCompanyJobSkill(Guid id) 
        {
            CompanyJobSkillPoco poco = _logic.Get(id);
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
        public ActionResult GetAllCompanyJobSkill() 
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
        public ActionResult PostCompanyJobSkill( 
            [FromBody]CompanyJobSkillPoco[] pocos)
        {
            _logic.Add(pocos);

            return Ok();
        }

        [HttpPut]
        [Route("company")]
        public ActionResult PutCompanyJobSkill( 
            [FromBody]CompanyJobSkillPoco[] pocos)
        {
            _logic.Update(pocos);

            return Ok();
        }

        [HttpDelete]
        [Route("company")]
        public ActionResult DeleteCompanyJobSkill( 
         [FromBody]CompanyJobSkillPoco[] pocos)
        {
            _logic.Delete(pocos);

            return Ok();
        }

    }
}