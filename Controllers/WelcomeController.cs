using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class WelcomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(string name = "Miguel", int numTimes = 5)
        {
            //return HtmlEncoder.Default.Encode($"Welcome {name}, to the Online Library {numTimes}");
            ViewData["Message"] = "Welcome " + name + ", to the Online Library";
            ViewData["NumTimes"] = numTimes;
            return View();
        }
    }
}
