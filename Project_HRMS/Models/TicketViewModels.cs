using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_Hrms.Models
{
    public class TicketViewModels
    {
        public int TicketId { get; set; }
        public string? TicketNo { get; set; }
        public string? Subject { get; set; }
        public string? Description { get; set; }
        public string? Priority { get; set; }
        public string? Status { get; set; }
        public int RaisedByUserId { get; set; }
        public string? RaisedByName { get; set; }
        public int? AssignedToUserId { get; set; }
        public string? AssignedToName { get; set; }
        public string? ManagerNote { get; set; }
        public string? Solution { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? FormattedDate { get; set; }
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}
