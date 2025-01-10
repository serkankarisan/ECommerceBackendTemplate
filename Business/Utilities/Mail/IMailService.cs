using Core.Entities.Concrete.Auth;
using Core.Utilities.Results;

namespace Business.Utilities.Mail
{
    public interface IMailService
    {
        IResult SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        IResult SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        IResult SendPasswordResetMailAsync(ResetPasswordCode resetPasswordCode);
    }
}