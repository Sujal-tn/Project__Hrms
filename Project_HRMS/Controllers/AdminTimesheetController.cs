using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using System.Text;

namespace Project_Hrms.Controllers
{
    public class AdminTimesheetController : Controller
    {
        private readonly ITimesheetService ts;
        public AdminTimesheetController(ITimesheetService ts)
        {
            this.ts = ts;
        }

        public async Task<IActionResult> Index()
        {
            var allTimesheets = await ts.GetAllTimesheets();
            return View(allTimesheets);
        }

        [HttpPost]
        public async Task<IActionResult> Approve(List<int> ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    await ts.ApproveTimesheet(id, "Admin");
                }
            }
            TempData["msg"] = "Timesheet(s) Approved Successfully";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Reject(List<int> ids)
        {
            if (ids != null)
            {
                foreach (var id in ids)
                {
                    await ts.RejectTimesheet(id, "Admin");
                }
            }
            TempData["msg"] = "Timesheet(s) Rejected Successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExportTimesheets()
        {
            var data = await ts.GetAllTimesheets();

            var csv = new StringBuilder();
            csv.AppendLine("Employee,CreatedAt,Project,WorkedHours,Status");

            foreach (var item in data)
            {
                csv.AppendLine($"{item.User?.FirstName} {item.User?.LastName},{item.Date.ToShortDateString()},{item.Projects?.ProjectName},{item.WorkHours},{item.Status}");
            }

            var bytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(bytes, "text/csv", "Timesheets.csv");
        }

        public async Task<IActionResult> ExportToPDF()
        {
            var list = await ts.GetAllTimesheets();

            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
                PdfWriter.GetInstance(document, stream);
                document.Open();

                Paragraph header = new Paragraph("Timesheet Report")
                {
                    Alignment = Element.ALIGN_CENTER
                };

                document.Add(header);

                PdfPTable table = new PdfPTable(5);

                table.AddCell("Employee");
                table.AddCell("Date");
                table.AddCell("Project");
                table.AddCell("Worked Hours");
                table.AddCell("Status");

                foreach (var item in list)
                {
                    table.AddCell(item.User != null? item.User.FirstName + " " + item.User.LastName: "");

                    table.AddCell(item.Date.ToString("dd-MM-yyyy")
                    );

                    table.AddCell(item.Projects != null? item.Projects.ProjectName: "");

                    table.AddCell(item.WorkHours.ToString());

                    table.AddCell(item.Status);
                }

                document.Add(table);

                document.Close();

                return File(stream.ToArray(),"application/pdf","TimesheetReport.pdf");
            }
        }
    }
}