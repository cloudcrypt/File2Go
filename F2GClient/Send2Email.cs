using System.Net.Mail;

namespace F2GClient {
    public class Send2Email {
        public static void Send(string filePath, string receiverEmail) {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp-mail.outlook.com");
            mail.From = new MailAddress("file2go@outlook.com"); // 
            mail.To.Add(receiverEmail);
            mail.Subject = "Test Mail - 1";
            mail.Body = "mail with attachment";

            System.Net.Mail.Attachment attachment;
            attachment = new System.Net.Mail.Attachment(filePath);
            mail.Attachments.Add(attachment);

            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("file2go@outlook.com", "dr0wss4p");
            SmtpServer.EnableSsl = true;

            SmtpServer.Send(mail);
        }
    }
}
