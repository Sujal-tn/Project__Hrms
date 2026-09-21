using Microsoft.AspNetCore.Mvc;
using Project_Hrms.Interface;
using Project_Hrms.Models;

namespace Project_Hrms.Controllers
{
    public class OrganizationController : Controller
    {
        IOrganizationService os;
        public OrganizationController(IOrganizationService os)
        {
            this.os = os;
        }
        public IActionResult Index()
        {
            var organization = os.GetOrganization();
            return View(organization);
        }

        [HttpPost]
        public IActionResult AddOrganization(Organization organization)
        {

            os.AddOrganization(organization);
            return RedirectToAction("Index");
        }


        public IActionResult UpdateOrganization(int id)
        {
            var or = os.fetchOrganizationById(id);
            return View(or);
        }
        [HttpPost]
        public IActionResult UpdateOrganization(Organization organization)
        {
            
            os.UpdateOrganization(organization);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteOrganization(int id)
        {
            os.DeleteOrganization(id);
            return RedirectToAction("Index");
        }
    }
}
