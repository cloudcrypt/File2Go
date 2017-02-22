using MailKit;
using MailKit.Net.Smtp;
using MimeKit;
using System.IO;

namespace F2GWeb.Services {
    public class EmailService {
        public static void Send(F2G.Models.File file, string receiverEmail) {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("File2Go", "file2go@outlook.com"));
            emailMessage.To.Add(new MailboxAddress("", "daniel.dastoor@hotmail.com"));
            emailMessage.Subject = "Test Mail - 1";
            var body = new TextPart("plain") { Text = "mail with attachment" };
            //emailMessage.Body = new TextPart("plain") { Text = "mail with attachment" };
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
                //client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                client.Connect("smtp-mail.outlook.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("file2go@outlook.com", "dr0wss4p");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
                //MailMessage mail = new MailMessage();
                //SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
                //mail.From = new MailAddress("file2go@outlook.com"); // 
                //mail.To.Add(receiverEmail);
                //mail.Subject = "Test Mail - 1";
                //mail.Body = "mail with attachment";

                //System.Net.Mail.Attachment attachment;
                //attachment = new System.Net.Mail.Attachment(filePath);
                //mail.Attachments.Add(attachment);

                //SmtpServer.Port = 587;
                //SmtpServer.Credentials = new System.Net.NetworkCredential("file2go@outlook.com", "dr0wss4p");
                //SmtpServer.EnableSsl = true;

                //SmtpServer.Send(mail);
        }
    }
}
//public async Task SendEmailAsync(string email, string subject, string message)
//{
//    var emailMessage = new MimeMessage();

//    emailMessage.From.Add(new MailboxAddress("Joe Bloggs", "jbloggs@example.com"));
//    emailMessage.To.Add(new MailboxAddress("", email));
//    emailMessage.Subject = subject;
//    emailMessage.Body = new TextPart("plain") { Text = message };

//    using (var client = new SmtpClient())
//    {
//        client.Connect("smtp-mail.outlook.com", 587, true);
//        client.Authenticate("file2go@outlook.com", "dr0wss4p");
//        client.Send(emailMessage);
//        client.Disconnect(true);
//        await client.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
//        await client.SendAsync(emailMessage).ConfigureAwait(false);
//        await client.DisconnectAsync(true).ConfigureAwait(false);
//    }
//}
