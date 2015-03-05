using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.UI;
using ResumeAdmin.Data;
using ResumeAdmin.Data.Entities;
using ResumeAdmin.WebApi.Models;

namespace ResumeAdmin.WebApi.Controllers
{
    [RoutePrefix("api/resume")]
    public class ResumeController : BaseApiController
    {
        public ResumeController(IResumeRepository repository)
            : base(repository)
        {
        }

        [Route(Name = "Resumes")]
        //public IQueryable<Resume> Get()
        //{
        //    IQueryable<Resume> query;
        //    query = Repository.GetAllResumes();
        //    //var results = query.Select(r => TheModelFactory.Create(r)); 
        //    return query;
        //}

        public IHttpActionResult Get()
        {
            IQueryable<Resume> query = Repository.GetAllResumes();
            var results = query.ToList().Select(r => TheModelFactory.Create(r));
            return Ok<IEnumerable<ResumeModel>>(results);
        }

        [Route("{id:int}", Name = "Resume")]
        public IHttpActionResult Get(int id)
        {
            var resume = Repository.GetResume(id);
            var result = TheModelFactory.Create(resume);
            return Ok<ResumeModel>(result);
        }

        [Route(Name="AddResume")]
        [HttpPost]
        public HttpResponseMessage Post([FromBody]ResumeModel model)
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
