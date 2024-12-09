using Microsoft.AspNetCore.Identity.UI.Services;

namespace IzunaDrop.Services
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            // Do nothing, just return completed task.
            return Task.CompletedTask;
        }
    }
}
