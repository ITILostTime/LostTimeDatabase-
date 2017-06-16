using System;
using System.Diagnostics;
using System.ComponentModel;
using System.IO;
using System.Web.Mail;

public class Mail
{
    public mail(StringBuilder Contenu, string Subject, string MailTo, string MailFrom, string SMTPServer, bool InHTML)
{
	string Retour = string.Empty;
	MailMessage msg = null;
	System.Text.Encoding MyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
	try
	{
		msg = new MailMessage(); 
		msg.Body = Contenu.ToString();
		msg.BodyEncoding = MyEncoding;
		if (InHTML)
		{
			msg.BodyFormat = MailFormat.Html;
		}
		else
		{
			msg.BodyFormat = MailFormat.Text;
		}
		msg.Subject = Subject; 
		msg.From =MailFrom; 
		msg.To = MailTo; 
		SmtpMail.SmtpServer = SMTPServer; 
		SmtpMail.Send(msg); 
		Retour = "Mail sent to "+ MailTo;
	}
	catch(Exception ex)
	{
		Retour = "Error in Sendmail function - Details : "+ ex.ToString();
	}
	finally
	{
		msg = null;
		MyEncoding = null;
	}
	return Retour;
}
}