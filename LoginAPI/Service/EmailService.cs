using FluentEmail.Core;

namespace LoginAPI.Service
{
    public interface IEmailService
    {
        Task SendConfirmationEmailAsync(string email, string confirmationLink);
        Task SendPasswordResetEmailAsync(string email, string resetLink);
    }

    public class EmailService : IEmailService
    {
        private readonly IFluentEmail _fluentEmail;
        private readonly IConfiguration _configuration;

        public EmailService(IFluentEmail fluentEmail, IConfiguration configuration)
        {
            _fluentEmail = fluentEmail;
            _configuration = configuration;
        }

        public async Task SendConfirmationEmailAsync(string email, string confirmationLink)
        {
            var template = @"
        <h2>Welcome to Our App!</h2>
        <p>Please confirm your email by clicking the link below:</p>
        <a href='@Model.ConfirmationLink'>Confirm Email</a>
        <p>If you didn't request this, please ignore this email.</p>";

            await _fluentEmail
                .To(email)
                .Subject("Confirm your email")
                .UsingTemplate(template, new { ConfirmationLink = confirmationLink })
                .SendAsync();
        }

        public async Task SendPasswordResetEmailAsync(string email, string resetLink)
        {
            var template = @"
        <h2>Password Reset Request</h2>
        <p>We received a request to reset your password. Click the link below to reset it:</p>
        <a href='@Model.ResetLink'>Reset Password</a>
        <p>If you didn't request this, please ignore this email.</p>
        <p>This link will expire in 24 hours.</p>";

            await _fluentEmail
                .To(email)
                .Subject("Password Reset Request")
                .UsingTemplate(template, new { ResetLink = resetLink })
                .SendAsync();
        }
    }
}
