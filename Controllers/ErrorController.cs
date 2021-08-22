using Microsoft.AspNetCore.Mvc;

namespace LibraryWebApp.Controllers
{
    public class ErrorController : ControllerBase
    {
        [Route("/Error")]
        public IActionResult Error() => Problem();
    }
}
