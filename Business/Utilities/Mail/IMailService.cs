using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete.Auth;

namespace Business.Utilities.Mail
{
    public interface IMailService
    {
        IResult SendMailAsync(string to, string subject, string body, bool isBodyHtml = true);
        IResult SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true);
        IResult SendPasswordResetMailAsync(ResetPasswordCode resetPasswordCode);
    }
}