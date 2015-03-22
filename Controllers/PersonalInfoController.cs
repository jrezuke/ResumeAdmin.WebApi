using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ResumeAdmin.Data;
using ResumeAdmin.Data.Entities;

namespace ResumeAdmin.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:63342", headers: "*", methods: "*")]
    [RoutePrefix("api/personalinfo")]
    public class PersonalInfoController : BaseApiController
    {
        public PersonalInfoController(IResumeRepository repository)
            :base(repository)
        {
        }

        [Route("PersonalInfosShort", Name = "PersonalInfosShort")]
        [HttpGet]
        public IHttpActionResult GetPersonalInfosShort()
        {
            var query = Repository.GetAllPersonalInfoShort().ToList();
            return Ok(query);

        }
    }
}
