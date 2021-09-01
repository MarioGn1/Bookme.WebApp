using Bookme.Data;
using Bookme.Data.Models;
using Bookme.Services.Contracts;
using Bookme.ViewModels.Booking;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bookme.Services
{
    public class EmailService : IEmailService
    {
        private const string clientEmailSubject = "Successfully booked service";
        private const string businessEmailSubject = "You have a new booking";

        private readonly BookmeDbContext data;

        public EmailService(BookmeDbContext data)
        {
            this.data = data;
        }

        public async Task<bool> SendEmailBookingConfirmation(BookServiceViewModel model)
        {
            var businessInfo = data.Users
                .Include(x => x.Business)
                .Where(x => x.Id == model.OwnerId)
                .FirstOrDefault();

            var clientInfo = data.Users.Find(model.ClientId);

            SmtpClient clientSmtp = new SmtpClient();
            clientSmtp.Host = GmailSender.SMTP_HOST_GMAIL;
            clientSmtp.Port = Convert.ToInt32(GmailSender.SMTP_PORT_GMAIL);
            clientSmtp.UseDefaultCredentials = false;
            clientSmtp.Credentials = new System.Net.NetworkCredential(GmailSender.FROM_EMAIL_ACCOUNT, GmailSender.FROM_EMAIL_PASSWORD);
            clientSmtp.EnableSsl = true;

            MailAddress from = new MailAddress(GmailSender.FROM_EMAIL_ACCOUNT, GmailSender.FROM_DISPLAY_NAME, Encoding.UTF8);

            var clientEmailBody = PrepareEmailBody(model, true, businessInfo, clientInfo);
            var businessEmailBody = PrepareEmailBody(model, false, businessInfo, clientInfo);

            var resultClient = await SendMailTo(from, clientInfo.Email, clientEmailSubject, clientEmailBody, clientSmtp);
            var resultBusiness = await SendMailTo(from, businessInfo.Email, businessEmailSubject, businessEmailBody, clientSmtp);

            return resultClient && resultBusiness;
        }

        private async Task<bool> SendMailTo(MailAddress from, string emailTo, string subject, string body, SmtpClient client)
        {
            bool isSucceed;

            MailAddress to = new MailAddress(emailTo);

            MailMessage message = new MailMessage(from, to);
            message.Body = body;
            message.Body += Environment.NewLine;
            message.BodyEncoding = Encoding.UTF8;
            message.Subject = subject;
            message.SubjectEncoding = Encoding.UTF8;

            try
            {
                string userState = emailTo;

                await client.SendMailAsync(message);

                isSucceed = true;
            }
            catch (Exception e)
            {
                //Exception can be logged if need
                isSucceed = false;
            }

            message.Dispose();
            return isSucceed;
        }

        private string PrepareEmailBody(BookServiceViewModel model, bool isClient, ApplicationUser businessInfo, ApplicationUser clientInfo)
        {
            var sb = new StringBuilder();
            if (isClient)
            {
                sb.AppendLine($"Hello {model.FirstName} {model.LastName},");
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine("Thank you for using Bookme-bg!");
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine($"You successfuly book {model.Name} for {model.Date} with duration {model.Duration} minutes.");
                sb.AppendLine("Operator contacts:");
                sb.AppendLine($" - Name of the Business: {businessInfo.Business.BusinessName}");
                sb.AppendLine($" - Phone: {businessInfo.Business.PhoneNumber}");
                sb.AppendLine($" - Email: {businessInfo.Email}");
                sb.AppendLine($" - Address: {businessInfo.Business.Address}");
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine("For more details regarding this booking please contact the operator!");
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine("You can track all your booked hours in Settings menu => My Bookings");
            }
            else
            {
                sb.AppendLine($"Hello {businessInfo.FirstName} {businessInfo.LastName},");
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine("Thank you for using Bookme-bg!");
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine($"You have a new booked {model.Name} for {model.Date} with duration {model.Duration} minutes.");
                sb.AppendLine("Client contacts:");
                sb.AppendLine($" - Names: {model.FirstName} {model.LastName}");
                sb.AppendLine($" - Phone: {model.PhoneNumber}");
                sb.AppendLine($" - Email: {clientInfo.Email}");
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine("For more details regarding this booking please check you Business bookings or contact the client!");
                sb.AppendLine(Environment.NewLine);
                sb.AppendLine("You can track all your upcoming bookings in Settings menu => Business Bookings");
            }

            sb.AppendLine(Environment.NewLine);
            sb.AppendLine("The team of Bookme-bg wish you a pleasant day!");

            return sb.ToString().TrimEnd();
        }
    }
}
