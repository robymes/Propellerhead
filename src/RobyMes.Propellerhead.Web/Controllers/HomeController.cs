using Microsoft.AspNetCore.Mvc;
using RobyMes.Propellerhead.Web.Models;
using System.Diagnostics;

namespace RobyMes.Propellerhead.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Customer(string id)
        {
            this.ViewData["CustomerId"] = id;
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier
                }
            );
        }
    }
}
