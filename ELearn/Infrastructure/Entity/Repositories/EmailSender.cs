using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ELearn.Infrastructure.Entity.Repositories
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}