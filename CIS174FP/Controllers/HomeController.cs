using CIS174FP.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CIS174FP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}