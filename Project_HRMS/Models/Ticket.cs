using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Project_Hrms.Models.EmployeeModel;

namespace Project_Hrms.Models
{
    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public string? TicketNo { get; set; }

        [Required(ErrorMessage = "subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "description is required")]
        public string Description { get; set; }

        [Required]
        public string Priority { get; set; } = "Medium";
        public string Status { get; set; } = "Open";

        [Required(ErrorMessage = "select sender employee")]
        [ForeignKey(nameof(User))]
        public int RaisedByUserId { get; set; }
        public int? AssignedToUserId { get; set; }
        public string? ManagerNote { get; set; }
        public string? Solution { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ResolvedDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public virtual User? RaisedByUser { get; set; }
        public virtual User? AssignedToUser { get; set; }
    }
}
