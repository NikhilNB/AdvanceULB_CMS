using AdvanceULB_CMS.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvanceULB_CMS.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckLogin(Accounts obj)
        {
            if(obj.username== "UlbCreation@project.com" && obj.password== "admin#123")
            {
                return RedirectToAction("Index", "ULB");
            }
            return RedirectToAction("CheckLogin", "Login");
        }
    }
}
