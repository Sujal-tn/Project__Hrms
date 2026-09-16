using ClosedXML.Excel;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;
using System.Reflection.Metadata;
using System.Security.Claims;
using Document = iTextSharp.text.Document;

namespace Project_Hrms.Controllers
{
    public class AdminAttendanceController : Controller
    {
        private readonly IAttendanceService attendanceService;
        public AdminAttendanceController(IAttendanceService attendanceService)
        {
            this.attendanceService = attendanceService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await attendanceService.GetAllAttendanceAsync();
            var todayRecords = await attendanceService.GetTodayAttendanceAsync();
            var employees = await attendanceService.GetAllEmployeesAsync();
            var approvedLeaveUserIds = await attendanceService.GetApprovedLeaveUserIdsForTodayAsync();

            var absentToday = todayRecords.Where(a => a.Status == "Absent").ToList();
            var absentUserIds = absentToday.Select(a => a.UserId).ToList();

            int uninformed = absentUserIds.Count(id => !approvedLeaveUserIds.Contains(id));
            int permission = absentUserIds.Count(id => approvedLeaveUserIds.Contains(id));
            int present = todayRecords.Count(a => a.Status == "Present");
            int lateLogin = todayRecords.Count(a => a.Late > 0);
            int absent = absentToday.Count;

            ViewBag.Departments = await attendanceService.GetAllDepartmentsAsync();
            ViewBag.TotalEmployees = employees.Count;
            ViewBag.Present = present;
            ViewBag.LateLogin = lateLogin;
            ViewBag.Uninformed = uninformed;
            ViewBag.Permission = permission;
            ViewBag.Absent = absent;
            ViewBag.AbsentEmployees = employees.Where(e => absentUserIds.Contains(e.UserId)).ToList();

            return View(list);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var data = await attendanceService.GetAttendanceByIdAsync(id);
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Attendance model)
        {
            await attendanceService.UpdateAttendanceAsync(model);
            TempData["msg"] = "Attendance Updated Successfully";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ExportToPDF()
        {
            var list = await attendanceService.GetAllAttendanceAsync();

            using (MemoryStream stream = new MemoryStream())
            {
                Document document = new Document(PageSize.A4.Rotate(), 25, 25, 30, 30);
                PdfWriter.GetInstance(document, stream);
                document.Open();

                Paragraph header = new Paragraph("Attendance Report") { Alignment = Element.ALIGN_CENTER };
                document.Add(header);

                PdfPTable table = new PdfPTable(8);
                table.AddCell("Date");
                table.AddCell("Employee");
                table.AddCell("Department");
                table.AddCell("Status");
                table.AddCell("Check In");
                table.AddCell("Check Out");
                table.AddCell("Break");
                table.AddCell("Production Hours");

                foreach (var item in list)
                {
                    table.AddCell(item.Date.ToString("dd-MM-yyyy"));
                    table.AddCell(item.User.FirstName + " " + item.User.LastName);
                    table.AddCell(item.User.Department != null ? item.User.Department.Name : "");
                    table.AddCell(item.Status);
                    table.AddCell(item.CheckIn?.ToString("hh:mm tt"));
                    table.AddCell(item.CheckOut?.ToString("hh:mm tt"));
                    table.AddCell(item.BreakHours.ToString());
                    table.AddCell(item.ProductionHours.ToString());
                }

                document.Add(table);
                document.Close();

                return File(stream.ToArray(), "application/pdf", "AttendanceReport.pdf");
            }
        }

        public async Task<IActionResult> ExportToExcel()
        {
            var list = await attendanceService.GetAllAttendanceAsync();

            using (var workbook = new XLWorkbook())
            {
                var sheet = workbook.Worksheets.Add("Attendance");
                sheet.Cell(1, 1).Value = "Date";
                sheet.Cell(1, 2).Value = "Employee";
                sheet.Cell(1, 3).Value = "Department";
                sheet.Cell(1, 4).Value = "Status";
                sheet.Cell(1, 5).Value = "Check In";
                sheet.Cell(1, 6).Value = "Check Out";
                sheet.Cell(1, 7).Value = "Break";
                sheet.Cell(1, 8).Value = "Production Hours";
                sheet.Row(1).Style.Font.Bold = true;

                int row = 2;
                foreach (var item in list)
                {
                    sheet.Cell(row, 1).Value = item.Date.ToString("dd-MM-yyyy");
                    sheet.Cell(row, 2).Value = item.User.FirstName + " " + item.User.LastName;
                    sheet.Cell(row, 3).Value = item.User.Department != null ? item.User.Department.Name : "";
                    sheet.Cell(row, 4).Value = item.Status;
                    sheet.Cell(row, 5).Value = item.CheckIn?.ToString("hh:mm tt");
                    sheet.Cell(row, 6).Value = item.CheckOut?.ToString("hh:mm tt");
                    sheet.Cell(row, 7).Value = item.BreakHours.ToString();
                    sheet.Cell(row, 8).Value = item.ProductionHours.ToString();
                    row++;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "AttendanceReport.xlsx");
                }
            }
        }
    }
}
