using Company.G05.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Company.G05.PL.Helper
{
	public class EmailSetting
	{ 
		public static void SendingEmail( Email email) {

			var client = new SmtpClient("smtp.gmail.com" , 587);
			
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("abdelhakim07@gmail.com" , "csinrvtxaekfgexx");
			client.Send("abdelhakim07@gmail.com", email.To, email.Subject, email.Body);
		}
	}
}
