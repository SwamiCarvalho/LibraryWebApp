using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace LibraryWebApp.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}
