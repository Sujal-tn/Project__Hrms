using Microsoft.EntityFrameworkCore;
using Project_Hrms.Data;
using Project_Hrms.Interface;
using Project_Hrms.Interface.EmployeeInterface;
using Project_Hrms.Interface.LoginInterface;
<<<<<<< HEAD
using Project_Hrms.Services.EmployeeService;
using Project_Hrms.Services.LoginService;
using Project_Hrms.Interface;
using Project_Hrms.Services;
using Project_Hrms.Services.MasterDocuments;
using Project_Hrms.Interface.TrainingInterface;
=======
>>>>>>> origin/main
using Project_Hrms.Interface.MasterDocuments;
using Project_Hrms.Interface.MasterDocuments.Documents;
using Project_Hrms.Interface.PayrollInterface;
using Project_Hrms.Interface.TrainingInterface;
using Project_Hrms.Models;
using Project_Hrms.Service;
<<<<<<< HEAD
<<<<<<< HEAD
=======
using Project_Hrms.Services.Training;
>>>>>>> 7b3b1cadb92f4eda1bfe6b1bd61e9eec49ac4aed
=======

using Project_Hrms.Services;
using Project_Hrms.Services.Documents;
using Project_Hrms.Services.EmployeeService;
using Project_Hrms.Services.LoginService;
using Project_Hrms.Services.MasterDocuments;
using Project_Hrms.Services.PayrollService;
>>>>>>> origin/main

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IDesignationService, DesignationService>();
builder.Services.AddScoped<IEmpService, EmpService>();

builder.Services.AddSession();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("dbconn")
    ));

builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ILeaveService, LeaveService>();
builder.Services.AddScoped<IEarningTypeService, EarningTypeService>();
builder.Services.AddScoped<IEarningService, EarningService>();
builder.Services.AddScoped<IDeductionTypeService, DeductionTypeService>();
builder.Services.AddScoped<IDeductionService, DeductionService>();
builder.Services.AddScoped<IEmployeeSalaryService, EmployeeSalaryService>();
builder.Services.AddScoped<IPayslipService, PayslipService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAttendanceReport, AttendanceReportService>();
builder.Services.AddScoped<IProject, ProjectService>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();
builder.Services.AddScoped<ITrainingType, TrainingTypeServices>();
builder.Services.AddScoped<IPromotion, PromotionService>();
builder.Services.AddScoped<IAddTrainers, AddTrainerServicescs>();
builder.Services.AddScoped<ITrainingList, AddTrainingListServices>();
builder.Services.AddScoped<IAdminDocumentsServices, AdminDocumentsAddServices>();
builder.Services.AddScoped<IEmployeeDocumentsServices, EmployeeDocumentsAddServices>();
builder.Services.AddScoped<IAdminFileUpload, AdminFileUploadServices>();
builder.Services.AddScoped<IUploadedDocument, UploadedDocumentListServices>();
builder.Services.AddScoped<IEmployeeFileUpload, EmployeeFileUploadServices>();
builder.Services.AddScoped<IEmailService, EmailServices>();




builder.Services.AddScoped<ITask, TaskService>();
builder.Services.AddScoped<IEvent, EventService>();
builder.Services.AddScoped<IMasterEvent, MasterEventService>();

builder.Services.AddScoped<IDailyReportService, DailyReportService>();
builder.Services.AddScoped<IPaySlipsReportService, PaySlipsReportService>();
builder.Services.AddScoped<ILeaveReportService, LeaveReportService>();
builder.Services.AddScoped<IProjectReportService, ProjectReportService>();
builder.Services.AddScoped<IAddTrainers, AddTrainerServicescs>();
builder.Services.AddScoped<IResignation, ResignationService>();
builder.Services.AddScoped<ITaskReportService, TaskReportService>();
builder.Services.AddScoped<ITermination, TerminationService>();
builder.Services.AddScoped<ITicket, TicketService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();                  
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}")
    .WithStaticAssets();

app.Run();