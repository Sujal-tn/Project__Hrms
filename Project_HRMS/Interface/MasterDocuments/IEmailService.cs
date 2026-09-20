namespace Project_Hrms.Interface.MasterDocuments
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail,string subject,string body,List<string> filePaths);
    }
}
