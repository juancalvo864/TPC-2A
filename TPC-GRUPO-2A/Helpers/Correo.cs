using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class Correo
    {
        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("administracion@callcenter.com", "CallCenter");
                mail.To.Add(destinatario);
                mail.Subject = asunto;
                mail.Body = cuerpo;
                mail.IsBodyHtml = false;

                smtpServer.Port = 587;
                smtpServer.Credentials = new NetworkCredential("armetal.autopartes@gmail.com", "pqtsxlnvbkgfsgln");
                smtpServer.EnableSsl = true;
                smtpServer.Send(mail);
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception("Error SMTP: " + smtpEx.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al enviar el correo: " + ex.Message);
            }
        }
    }
}
