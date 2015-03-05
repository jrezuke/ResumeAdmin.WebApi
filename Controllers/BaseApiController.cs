using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ResumeAdmin.Data;
using ResumeAdmin.WebApi.Models;

namespace ResumeAdmin.WebApi.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private IResumeRepository _repository;
        private ModelFactory _modelFactory;

        public BaseApiController(IResumeRepository repository)
        {
            _repository = repository;
        }

        protected IResumeRepository Repository
        {
            get { return _repository; }
        }

        protected ModelFactory TheModelFactory
        {
            get
            {
                if (_modelFactory == null)
                {
                    _modelFactory = new ModelFactory(Request, Repository);
                }
                return _modelFactory;
            }
        }
    }
}
