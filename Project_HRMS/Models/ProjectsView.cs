using Microsoft.AspNetCore.Http;

namespace Project_Hrms.Models
{
    public class ProjectsView
    {
        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public string ClientName { get; set; }

        public string ProjectDescription { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Priority { get; set; }

        public double ProjectValue { get; set; }

        public string PriceType { get; set; }

        public IFormFile LogoPath { get; set; }

        public IFormFile FilePath { get; set; }

        public string Status { get; set; }

        public string ManagerName { get; set; }

        public int? UserId { get; set; }
    }
}

