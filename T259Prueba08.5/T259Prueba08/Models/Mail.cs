using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace T259Prueba08.Models
{

    public class Mail
    {
        private static string usuario = ConfigurationManager.AppSettings["mailUser"];
        private static string pass = ConfigurationManager.AppSettings["mailPass"];
        private static string server = ConfigurationManager.AppSettings["mailServer"];
        private static string puerto_temp = ConfigurationManager.AppSettings["mailPuerto"];
        private static string ssl = ConfigurationManager.AppSettings["mailSsl"];

        public Mail()
        {

        }

        public int send(string[] dest, string asunto, string mensaje, int banfn, string url)
        {
            int puerto = Convert.ToInt32(puerto_temp);
            MailMessage mail = correo(dest, asunto, mensaje, banfn, url);
            return enviar(mail, usuario, pass, server, puerto);
        }

        private static MailMessage correo(string[] destinatario, string asunto, string mensaje, int banfn, string url)
        {
            MailMessage email = new MailMessage();

            for (int i = 0; i < destinatario.Length; i++)
            {
                email.To.Add(new MailAddress(destinatario[i]));
            }

            email.From = new MailAddress(usuario);
            email.Subject = asunto;

            string link = "<a href='" + url + "reqsVer.aspx?banfn=" + banfn + "'>Ver requisición</a>";

            string body = mensaje + link;
            //string body = mensaje ;

            email.IsBodyHtml = true;
            email.Body = body;
            email.Priority = MailPriority.Normal;
            return email;

        }

        private static int enviar(MailMessage email, string use, string pas, string ser, int pue)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.Host = ser;
            smtp.Port = pue;
            smtp.EnableSsl = bool.Parse(ssl);
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(use, pas);
            if (!email.Equals(null))
            {
                try
                {
                    smtp.Send(email);
                    email.Dispose();
                    return 0;
                }
                catch
                {
                    return 1;
                }
            }
            else
            {
                return 1;
            }
        }
    }
}