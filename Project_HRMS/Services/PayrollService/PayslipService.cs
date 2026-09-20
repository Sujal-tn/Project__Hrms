using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface.PayrollInterface;
using Project_Hrms.Models;
using Project_Hrms.Models.EmployeeModel;
using System.Globalization;

namespace Project_Hrms.Services.PayrollService
{
    public class PayslipService : IPayslipService
    {
        private readonly ApplicationDbContext db;
        private readonly IWebHostEnvironment env;

        public PayslipService(ApplicationDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            this.env = env;
        }

        private async Task<Payslips?> BuildPayslipDataAsync(int userId, string month, int year)
        {
            var user = await db.Users.Include(u => u.Designation).Include(u => u.Department).FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null)
                return null;

            var salary = await db.EmployeeSalaries.Where(s => s.UserId == userId).OrderByDescending(s => s.CreatedDate).FirstOrDefaultAsync();

            if (salary == null)
                return null;

            var earnings = await db.Earnings.Include(e => e.EarningType).Where(e => e.DepartmentId == user.DepartmentId && e.DesignationId == user.DesignationtId).ToListAsync();

            var deductions = await db.Deductions.Include(d => d.DeductionType).Where(d => d.DepartmentId == user.DepartmentId && d.DesignationId == user.DesignationtId).ToListAsync();

            if (!DateTime.TryParseExact(month, "MMMM", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedMonth))
                return null;

            int monthNumber = parsedMonth.Month;
            decimal totalHoursInMonth = DateTime.DaysInMonth(year, monthNumber) * 9m;
            decimal workedHours = 0m;
            decimal hourlyRate = totalHoursInMonth > 0 ? Math.Round(salary.TotalSalary / totalHoursInMonth, 2) : 0m;
            decimal totalEarnings = earnings.Sum(e => Math.Round(workedHours * e.EarningsPercentage * hourlyRate / 100m, 2));
            decimal totalDeductions = deductions.Sum(d => Math.Round(workedHours * d.DeductionPercentage * hourlyRate / 100m, 2));
            decimal netSalary = totalEarnings - totalDeductions;

            return new Payslips
            {
                UserId = userId,
                Month = month,
                Year = year,
                User = user,
                TotalHoursInMonth = totalHoursInMonth,
                WorkedHours = workedHours,
                HourlyRate = hourlyRate,
                Earnings = earnings,
                Deductions = deductions,
                TotalEarnings = totalEarnings,
                TotalDeductions = totalDeductions,
                NetSalary = netSalary
            };
        }

        public async Task<Payslips?> GetPayslipDataAsync(int userId, string month, int year)
        {
            return await BuildPayslipDataAsync(userId, month, year);
        }

        public async Task<bool> GeneratePayslipPdfAsync(int userId, string month, int year)
        {
            var data = await BuildPayslipDataAsync(userId, month, year);

            if (data == null || data.User == null)
                return false;

            string firstName = string.IsNullOrWhiteSpace(data.User.FirstName) ? "Employee" : data.User.FirstName;
            string lastName = string.IsNullOrWhiteSpace(data.User.LastName) ? "" : data.User.LastName;
            string fileName = $"Payslip_{firstName}_{lastName}_{month}_{year}.pdf";
            string directoryPath = Path.Combine(env.WebRootPath, "payslips");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string filePath = Path.Combine(directoryPath, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document(PageSize.A4, 30f, 30f, 30f, 30f);
                PdfWriter.GetInstance(document, fileStream);
                document.Open();

                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 18f);
                Font headingFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 13f);
                Font normalFont = FontFactory.GetFont(FontFactory.HELVETICA, 10f);
                Font boldFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 10f);

                Paragraph title = new Paragraph("Salary Payslip", titleFont);
                title.Alignment = Element.ALIGN_CENTER;
                document.Add(title);

                Paragraph monthParagraph = new Paragraph($"Month: {data.Month} {data.Year}", normalFont);
                monthParagraph.Alignment = Element.ALIGN_CENTER;
                document.Add(monthParagraph);

                document.Add(new Paragraph(" "));

                PdfPTable employeeTable = new PdfPTable(2);
                employeeTable.WidthPercentage = 100f;
                employeeTable.SetWidths(new float[] { 1f, 1f });

                PdfPCell employeeCell = new PdfPCell(new Phrase($"To: {data.User.FirstName} {data.User.LastName}", boldFont));
                employeeCell.Border = Rectangle.NO_BORDER;
                employeeTable.AddCell(employeeCell);

                string designationName = data.User.Designation != null ? data.User.Designation.DesignationName : "";
                PdfPCell designationCell = new PdfPCell(new Phrase(designationName, normalFont));
                designationCell.Border = Rectangle.NO_BORDER;
                designationCell.HorizontalAlignment = Element.ALIGN_RIGHT;
                employeeTable.AddCell(designationCell);

                document.Add(employeeTable);

                document.Add(new Paragraph($"Total Working Hours in {data.Month}: {data.TotalHoursInMonth}", normalFont));
                document.Add(new Paragraph($"Worked Hours: {data.WorkedHours}", normalFont));
                document.Add(new Paragraph($"Hourly Rate: {data.HourlyRate:C}", normalFont));
                document.Add(new Paragraph(" "));

                PdfPTable outerTable = new PdfPTable(2);
                outerTable.WidthPercentage = 100f;
                outerTable.SetWidths(new float[] { 1f, 1f });

                PdfPCell earningsOuterCell = new PdfPCell();
                earningsOuterCell.Border = Rectangle.NO_BORDER;

                earningsOuterCell.AddElement(new Paragraph("Earnings", headingFont));

                PdfPTable earningsTable = new PdfPTable(2);
                earningsTable.WidthPercentage = 100f;
                earningsTable.SetWidths(new float[] { 2f, 1f });

                PdfPCell earningsDescriptionHeader = new PdfPCell(new Phrase("Description", boldFont));
                earningsDescriptionHeader.BackgroundColor = new BaseColor(230, 230, 230);
                earningsTable.AddCell(earningsDescriptionHeader);

                PdfPCell earningsAmountHeader = new PdfPCell(new Phrase("Amount", boldFont));
                earningsAmountHeader.BackgroundColor = new BaseColor(230, 230, 230);
                earningsTable.AddCell(earningsAmountHeader);

                foreach (var earning in data.Earnings)
                {
                    decimal amount = Math.Round(data.WorkedHours * earning.EarningsPercentage * data.HourlyRate / 100m, 2);
                    earningsTable.AddCell(new PdfPCell(new Phrase(earning.EarningType.EarningName, normalFont)));
                    earningsTable.AddCell(new PdfPCell(new Phrase(amount.ToString("C"), normalFont)));
                }

                earningsTable.AddCell(new PdfPCell(new Phrase("Total Earnings", boldFont)));
                earningsTable.AddCell(new PdfPCell(new Phrase(data.TotalEarnings.ToString("C"), boldFont)));

                earningsOuterCell.AddElement(earningsTable);
                outerTable.AddCell(earningsOuterCell);

                PdfPCell deductionsOuterCell = new PdfPCell();
                deductionsOuterCell.Border = Rectangle.NO_BORDER;

                deductionsOuterCell.AddElement(new Paragraph("Deductions", headingFont));

                PdfPTable deductionsTable = new PdfPTable(2);
                deductionsTable.WidthPercentage = 100f;
                deductionsTable.SetWidths(new float[] { 2f, 1f });

                PdfPCell deductionsDescriptionHeader = new PdfPCell(new Phrase("Description", boldFont));
                deductionsDescriptionHeader.BackgroundColor = new BaseColor(230, 230, 230);
                deductionsTable.AddCell(deductionsDescriptionHeader);

                PdfPCell deductionsAmountHeader = new PdfPCell(new Phrase("Amount", boldFont));
                deductionsAmountHeader.BackgroundColor = new BaseColor(230, 230, 230);
                deductionsTable.AddCell(deductionsAmountHeader);

                foreach (var deduction in data.Deductions)
                {
                    decimal amount = Math.Round(data.WorkedHours * deduction.DeductionPercentage * data.HourlyRate / 100m, 2);
                    deductionsTable.AddCell(new PdfPCell(new Phrase(deduction.DeductionType.DeductionsName, normalFont)));
                    deductionsTable.AddCell(new PdfPCell(new Phrase(amount.ToString("C"), normalFont)));
                }

                deductionsTable.AddCell(new PdfPCell(new Phrase("Total Deductions", boldFont)));
                deductionsTable.AddCell(new PdfPCell(new Phrase(data.TotalDeductions.ToString("C"), boldFont)));

                deductionsOuterCell.AddElement(deductionsTable);
                outerTable.AddCell(deductionsOuterCell);

                document.Add(outerTable);
                document.Add(new Paragraph(" "));

                Paragraph netSalary = new Paragraph($"Net Salary: {data.NetSalary:C}", headingFont);
                netSalary.Alignment = Element.ALIGN_CENTER;
                document.Add(netSalary);

                Paragraph generatedOn = new Paragraph($"Generated on {DateTime.Now:dd MMM yyyy}", FontFactory.GetFont(FontFactory.HELVETICA, 9f));
                generatedOn.Alignment = Element.ALIGN_RIGHT;
                document.Add(generatedOn);

                document.Close();
            }

            var payslip = new Payslips
            {
                PayslipId = 0,
                UserId = userId,
                Month = month,
                Year = year,
                User = data.User,
                PayslipPath = filePath,
                GeneratedOn = DateTime.Now,
                TotalHoursInMonth = data.TotalHoursInMonth,
                WorkedHours = data.WorkedHours,
                HourlyRate = data.HourlyRate,
                Earnings = data.Earnings,
                Deductions = data.Deductions,
                TotalEarnings = data.TotalEarnings,
                TotalDeductions = data.TotalDeductions,
                NetSalary = data.NetSalary
            };

            db.Entry(data.User).State = EntityState.Unchanged;
            db.Payslips.Add(payslip);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<List<int>> GeneratePayslipsForSelectedUsersAsync(List<int> userIds, string month, int year)
        {
            var failedIds = new List<int>();

            foreach (var userId in userIds)
            {
                bool result = await GeneratePayslipPdfAsync(userId, month, year);

                if (!result)
                    failedIds.Add(userId);
            }

            return failedIds;
        }

        public async Task<List<Payslips>> GetTransactionHistoryAsync(string? month, int? year, int? departmentId, int? designationId)
        {
            var query = db.Payslips.Include(p => p.User).ThenInclude(u => u.Department).Include(p => p.User).ThenInclude(u => u.Designation).AsQueryable();

            if (!string.IsNullOrEmpty(month))
                query = query.Where(p => p.Month == month);

            if (year.HasValue)
                query = query.Where(p => p.Year == year.Value);

            if (departmentId.HasValue)
                query = query.Where(p => p.User.DepartmentId == departmentId.Value);

            if (designationId.HasValue)
                query = query.Where(p => p.User.DesignationtId == designationId.Value);

            return await query.OrderByDescending(p => p.GeneratedOn).ToListAsync();
        }

        public async Task<bool> DeletePayslipAsync(int payslipId)
        {
            var payslip = await db.Payslips.FindAsync(payslipId);

            if (payslip == null)
                return false;

            if (!string.IsNullOrEmpty(payslip.PayslipPath) && File.Exists(payslip.PayslipPath))
                File.Delete(payslip.PayslipPath);

            db.Payslips.Remove(payslip);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await db.Departments.ToListAsync();
        }

        public async Task<List<Designation>> GetDesignationsAsync()
        {
            return await db.Designations.ToListAsync();
        }

        public async Task<List<Payslips>> GetMyPayslipsAsync(int userId, string? month = null, int? year = null)
        {
            var query = db.Payslips.Include(p => p.User).Where(p => p.UserId == userId).AsQueryable();

            if (!string.IsNullOrEmpty(month))
                query = query.Where(p => p.Month == month);

            if (year.HasValue)
                query = query.Where(p => p.Year == year.Value);

            return await query.OrderByDescending(p => p.Year).ThenByDescending(p => p.Month).ToListAsync();
        }
    }
}