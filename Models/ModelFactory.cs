using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using ResumeAdmin.Data;
using ResumeAdmin.Data.Entities;

namespace ResumeAdmin.WebApi.Models
{
    public class ModelFactory
    {
        private UrlHelper _UrlHelper;
        private IResumeRepository _repo;
        public ModelFactory(HttpRequestMessage request, IResumeRepository repo)
        {
            _UrlHelper = new UrlHelper(request);
            _repo = repo;
        }

        public ResumeModel Create(Resume resume)
        {
            return new ResumeModel
            {
                Url = _UrlHelper.Link("Resume", new { id = resume.Id }),
                Id = resume.Id,
                Name = resume.Name,
                Description = resume.Description,
                PersonalInfoId = resume.PersonalInfoId,
                //PersonalInfo =  Create(_repo.GetPersonalInfo(resume.PersonalInfoId)),
                SummaryId = resume.SummaryId
                
            };

        }

        public ResumeModelShort Create(ResumeShort resume)
        {
            return new ResumeModelShort
            {
                Url = _UrlHelper.Link("Resume", new { id = resume.Id }),
                Id = resume.Id,
                Name = resume.Name,
                Description = resume.Description,
                PersonalInfoId = resume.PersonalInfoId

            };

        }

        public PersonalInfoModelShort Create(PersonalInfoShort personalInfo)
        {
            if(personalInfo == null)
                return null;
            return new PersonalInfoModelShort
            {
                Id = personalInfo.Id,
                Name = personalInfo.Name,
                Address1 = personalInfo.Address1,
                Address2 = personalInfo.Address2,
                City = personalInfo.City,
                State = personalInfo.State,
                Zip = personalInfo.Zip,
                Email = personalInfo.Email,
                HomePhone = personalInfo.HomePhone,
                MobilePhone = personalInfo.MobilePhone
            };
        }

        public ResumeModel Create(Summary summary)
        {
            return new ResumeModel
            {
                Id = summary.Id,
                Name = summary.Name,
                Description = summary.Description
            };

        }
        public Resume Parse(ResumeModel model)
        {
            try
            {
                var resume = new Resume()
                {
                    Name = model.Name,
                    Description = model.Description,
                    PersonalInfo = _repo.GetPersonalInfo(model.PersonalInfoId),
                    Id = model.Id,
                    Summary =  _repo.GetSummary(model.SummaryId)
                };

                return resume;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}