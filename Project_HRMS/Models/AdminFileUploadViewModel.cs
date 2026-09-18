namespace Project_Hrms.Models
{
    public class AdminFileUploadViewModel
    {
        
        public int UserId { get; set; }

        public string? UserName { get; set; }

        public List<AdminFileUploadItemViewModel> Documents { get; set; }
           
    }


    public class AdminFileUploadItemViewModel
    {
      
        public int DocumentId { get; set; }

        public string? DocumentName { get; set; }
        public IFormFile? File { get; set; }
    }

}
