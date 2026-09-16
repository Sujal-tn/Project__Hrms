using Project_Hrms.Interface;
using Project_Hrms.Models;
using Microsoft.AspNetCore.Mvc;

public class EmployeeReportController : Controller
{
    private readonly IUserService userService;

    public EmployeeReportController(IUserService userService)
    {
        this.userService = userService;
    }

    public IActionResult Index()
    {
        var users = userService.FeatchUser();

        var TotalUsers = users.Count();
        var ActiveUsers = users.Count(u => u.Status == "Active");
        var InActiveUsers = users.Count(u => u.Status != "Active");
        var RoleCount = users.Select(u => u.RoleId).Distinct().Count();

        var monthData = users.GroupBy(x => new
        {
            Year = DateTime.Parse(x.DateOfJoining).Year,
            Month = DateTime.Parse(x.DateOfJoining).Month
        })
    .Select(x => new YearData
    {
        Year = x.Key.Year,
        Month = x.Key.Month,
        Active = x.Count(u => u.Status == "Active"),
        Inactive = x.Count(u => u.Status != "Active")
    })
    .OrderBy(x => x.Year)
    .ThenBy(x => x.Month)
    .ToList();

        ViewBag.YearData = monthData;

        ViewBag.TUser = TotalUsers;
        ViewBag.Auser = ActiveUsers;
        ViewBag.InUser = InActiveUsers;
        ViewBag.Rcount = RoleCount;

        return View(users);
    }
}