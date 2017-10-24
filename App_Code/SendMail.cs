using System;
using System.Collections.Generic;
using System.Web;
using System.Net.Mail;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

/// <summary>
/// Summary description for SendMail
/// </summary>
public class SendMail
{
    

    public static string Sendmail()
    {
        string status = "";
        try
        {
            var apiKey = System.Environment.GetEnvironmentVariable("SENDGRID_APIKEY");
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("hieuduong08@gmail.com", "DX Team"),
                Subject = "Hello World from the SendGrid CSharp SDK!",
                PlainTextContent = "Hello, Email!",
                HtmlContent = "<strong>Hello, Email!</strong>"
            };
            msg.AddTo(new EmailAddress("hieuduong08@gmail.com", "Test User"));
            client.SendEmailAsync(msg);
            status = "completed";
        }
        catch (Exception ex)
        {
            status = ex.ToString();
        }
        return status;
    }

    /*
    public static void SendErrorMessage(string FromUser, string MessageSubject, string MessageContent)
    {
        MailMessage message = new MailMessage();
        message.From = new MailAddress(FromUser);
        message.To.Add("hoanghieu3@yahoo.com");
        message.Subject = MessageSubject;
        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(MessageContent, null, "text/html");
        message.AlternateViews.Add(htmlView);

        SmtpClient client = new SmtpClient("relay");
        try
        {
            client.Send(message);
        }
        catch
        {

        }
    }

    public static void SendErrorMessage(string MessageSubject, string MessageContent)
    {
        HttpContext context = HttpContext.Current;
        string FromUser = "";
        try
        {
            FromUser = context.Session["UserID"].ToString().Trim();
        }
        catch
        {
            FromUser = "hduong";
        }

        MailMessage message = new MailMessage();
        message.From = new MailAddress(FromUser);
        message.To.Add("hoanghieu3@yahoo.com");
        message.Subject = MessageSubject;
        AlternateView htmlView = AlternateView.CreateAlternateViewFromString(MessageContent, null, "text/html");
        message.AlternateViews.Add(htmlView);

        SmtpClient client = new SmtpClient("relay");
        try
        {
            client.Send(message);
        }
        catch
        {

        }
    }*/
}