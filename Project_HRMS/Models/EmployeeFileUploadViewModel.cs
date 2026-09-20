namespace Project_Hrms.Models
{
    public class EmployeeFileUploadViewModel
    {
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public List<EmployeeFileUploadItemViewModel> Documents { get; set; }

    }
        

    public class EmployeeFileUploadItemViewModel
    {
            
        public int DocumentId { get; set; }

        public string? DocumentName { get; set; }
        public IFormFile? File { get; set; }
    }

}
