using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;

namespace F2GWeb.Services {
    public class EmailService {
        public static void Send(F2G.Models.File file, string receiverEmail) {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("File2Go", "file2go@outlook.com"));
            emailMessage.To.Add(new MailboxAddress("", file.response.request.client.User.email));
            emailMessage.Subject = "Test Mail - 1";
            var body = new TextPart("plain") { Text = "mail with attachment" };
            var attachment = new MimePart()
            {
                ContentObject = new ContentObject(new MemoryStream(file.contents)),
                ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                ContentTransferEncoding = ContentEncoding.Base64,
                FileName = file.name
            };
            var multipart = new Multipart("mixed");
            multipart.Add(body);
            multipart.Add(attachment);
            emailMessage.Body = multipart;

            using (var client = new SmtpClient())
            {
                client.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("file2go@outlook.com", "dr0wss4p");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}
