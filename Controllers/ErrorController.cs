using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryWebApp.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/Error")]
        public IActionResult Error() => Problem();
    }
}
