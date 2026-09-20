using Project_Hrms.Models.EmployeeModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project_Hrms.Models
{
    public class BankInformation
    {
        [Key]
        public int BankInfoId { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }
        public string IFSCNumber { get; set; }

        public string BranchName { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }
    }

 }