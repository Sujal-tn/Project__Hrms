using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models
{
    public class Organization
    {
        [Key]
        public int OrganizationId { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDescription { get; set; }

        public string? OrganizationAddress { get; set; }

        public string? OrganizationPhone { get; set; }

        public string? OrganizationEmail { get; set; }

        public string? OrganizationLogo { get; set; }
    }
}
