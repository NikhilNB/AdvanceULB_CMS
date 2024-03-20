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

        public IActionResult CheckLogin()
        {
            Accounts accounts = new Accounts();
            if(accounts.username== "admin" &&  accounts.password== "admin")
            {
                return RedirectToAction("Index", "ULB");
            }
            return View();
        }
    }
}
