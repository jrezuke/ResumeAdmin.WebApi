using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.UI;
using ResumeAdmin.Data;
using ResumeAdmin.Data.Entities;
using ResumeAdmin.WebApi.Models;

namespace ResumeAdmin.WebApi.Controllers
{
    [EnableCors(origins: "http://localhost:63342", headers: "*", methods: "*")]
    [RoutePrefix("api/resume")]
    public class ResumeController : BaseApiController
    {
        public ResumeController(IResumeRepository repository)
            : base(repository)
        {
        }

        [Route(Name = "Resumes")]
        public IHttpActionResult Get()
        {
            IQueryable<Resume> query = Repository.GetAllResumes();
            var results = query.ToList().Select(r => TheModelFactory.Create(r));
            return Ok<IEnumerable<ResumeModelLong>>(results);
        }

        [Route("ResumesShort", Name = "ResumesShort")]
        public IHttpActionResult GetResumesShort()
        {
            IQueryable<Resume> query = Repository.GetAllResumes();
            var results = query.ToList().Select(r => TheModelFactory.Create(r));
            return Ok<IEnumerable<ResumeModelLong>>(results);
        }

        [Route("{id:int}", Name = "Resume")]
        public IHttpActionResult Get(int id)
        {
            var resume = Repository.GetResume(id);
            var result = TheModelFactory.Create(resume);
            return Ok<ResumeModelLong>(result);
        }

        [Route(Name="AddResume")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]ResumeModelLong model)
        {
            try
            {
                var resume = TheModelFactory.Parse(model);
                if (resume == null)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not get resume from body");
                }

                //duplicate check
                //if (Repository.GetAllResumes().Any(r => r.Id == resume.Id))
                //{
                //    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Duplicate resumes not allowed.");
                //}

                if (Repository.AddorUpdate(resume) && Repository.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, TheModelFactory.Create(resume));
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Could not save to the database.");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex); 
            }
        }
    }
}
