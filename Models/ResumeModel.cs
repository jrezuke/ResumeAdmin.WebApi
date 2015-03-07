
using ResumeAdmin.Data.Entities;

namespace ResumeAdmin.WebApi.Models
{
    public class ResumeModel
    {
        public string Url { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PersonalInfoId { get; set; }
        public PersonalInfoModel PersonalInfo { get; set; }
        public int? SummaryId { get; set; }
        public Summary Summary { get; set; }
    }
}