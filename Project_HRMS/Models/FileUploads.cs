using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class FileUploads
    {
        public int id { get; set; }

        public string FileName { get; set; }
        public string FilePath { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
