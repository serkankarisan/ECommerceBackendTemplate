using Business.Abstract.Auths;
using Core.Entities.Concrete.Auth;
//using Business.ValidationRules.FluentValidations;
//using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Business.Utilities.Mail
{
    public class MailManager : IMailService
    {
        readonly IConfiguration _configuration;
        private readonly IResetPasswordCodeService _resetPasswordCodeService;
        private readonly IUserService _userService;

        public MailManager(IConfiguration configuration, IResetPasswordCodeService resetPasswordCodeService, IUserService userService)
        {
            _configuration = configuration;
            _resetPasswordCodeService = resetPasswordCodeService;
            _userService = userService;
        }

        public IResult SendMailAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            SendMailAsync(new[] { to }, subject, body, isBodyHtml);
            return new SuccessResult("Mail Gönderildi.");
        }

        public IResult SendMailAsync(string[] tos, string subject, string body, bool isBodyHtml = true)
        {
            MailMessage mail = new MailMessage();
            mail.IsBodyHtml = isBodyHtml;
            foreach (string to in tos)
                mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;
            mail.From = new MailAddress(_configuration["Mail:Email"], "RentACar", System.Text.Encoding.UTF8);


            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(_configuration["Mail:Email"], _configuration["Mail:Password"]);
            smtp.Port = Convert.ToInt32(_configuration["Mail:Port"]);
            smtp.EnableSsl = true;
            smtp.Host = _configuration["Mail:Host"];
            smtp.SendMailAsync(mail);
            return new SuccessResult("Mail Gönderildi.");

        }
        public IResult SendPasswordResetMailAsync(ResetPasswordCode resetPasswordCode)
        {
            StringBuilder mail = new StringBuilder();
            mail.AppendLine("Merhaba<br>Eğer yeni şifre talebinde bulunduysanız aşağıdaki linkten şifrenizi yenileyebilirsiniz.<br><strong><a target=\"_blank\" href=\"");
            mail.AppendLine(_configuration["ClientURL"]);
            mail.AppendLine("/update-password");
            mail.AppendLine("?userId=" + resetPasswordCode.UserId.ToString());
            mail.AppendLine("&code=" + resetPasswordCode.Code);
            mail.AppendLine("\">Yeni şifre talebi için tıklayınız...</a></strong><br><br><span style=\"font-size:12px;\">NOT : Eğer ki bu talep tarafınızca gerçekleştirilmemişse lütfen bu maili ciddiye almayınız.</span><br>Saygılarımızla...<br><br><br>Software Solution Team");

            SendMailAsync(resetPasswordCode.UserEmail, "Şifre Yenileme Talebi", mail.ToString());
            return new SuccessResult("Mail Gönderildi.");
        }
    }
}
