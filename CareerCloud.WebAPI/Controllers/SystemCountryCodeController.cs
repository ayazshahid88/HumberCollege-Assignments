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
    [Route("api/careercloud/system/v1")]
    [ApiController]
    public class SystemCountryCodeController : ControllerBase
    {

        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeController()
        {
            var repo = new EFGenericRepository<SystemCountryCodePoco>();
            _logic = new SystemCountryCodeLogic(repo);
        }

        [HttpGet]
        [Route("system/{id}")]
        public ActionResult GetSystemCountryCode(string id) 
        {
            SystemCountryCodePoco poco = _logic.Get(id);
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
        [Route("system")]
        public ActionResult GetAllSystemCountryCode() 
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
        [Route("system")]
        public ActionResult PostSystemCountryCode( 
            [FromBody]SystemCountryCodePoco[] pocos)
        {
            _logic.Add(pocos);

            return Ok();
        }

        [HttpPut]
        [Route("system")]
        public ActionResult PutSystemCountryCode( 
            [FromBody]SystemCountryCodePoco[] pocos)
        {
            _logic.Update(pocos);

            return Ok();
        }

        [HttpDelete]
        [Route("system")]
        public ActionResult DeleteSystemCountryCode( 
            [FromBody]SystemCountryCodePoco[] pocos)
        {
            _logic.Delete(pocos);

            return Ok();
        }
    }
}