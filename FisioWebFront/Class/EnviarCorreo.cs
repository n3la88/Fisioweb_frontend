using System.Net.Mail;
using System.Net;
using FisioWebFront.Entities;

namespace FisioWebFront.Class
{
    public class EnviarCorreo
    {
        public bool SendMail(string to, string subject, string body)
        {
            string from = "FisiovetCR@outlook.com";
            string password = "Fidelitas123";
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(from);
                mailMessage.To.Add(to);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                mailMessage.IsBodyHtml = true;
                
                SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587);
                smtpClient.Credentials = new NetworkCredential(from, password);
                smtpClient.EnableSsl = true;
                smtpClient.Send(mailMessage);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool RecoveryPassword(string to, string recoveryPassword)
        {
            try
            {
                string subject = "Correo de recuperacion de contraseña";
                string body = @"<style>
                                  h1{
                                       text-color:blue;
                                       font-color:blue;
                                    }
                                </style>
                                <h1>Correo de recuperacion de contraseña</h1>
                                <p>La contraseña de recuperación es : " + recoveryPassword+" </p>";
                SendMail(to,subject,body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CitaConfirmada(string to, string fecha)
        {
            try
            {
                string subject = "Correo de confirmacion de cita";
                string body = @"<style>
                                  h1{
                                       text-color:blue;
                                       font-color:blue;
                                    }
                                </style>
                                <h1>Correo de confirmación de cita</h1>
                                <p>Su cita ha quedado confirmada en la sigueinte fecha: "+ fecha + " </p>";
                SendMail(to, subject, body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


		public bool ConsultaRealizada(CorreoOBJ correoUsuario)
		{
			try
			{

				string correo = "FisiovetCR@outlook.com";

				string subject = "Consulta: " + correoUsuario.subject ;
				string body = @"<style>
                                  h1{
                                       text-color:blue;
                                       font-color:blue;
                                    }
                                </style>
                                <p>Consulta hecha por: " +  correoUsuario.nombre + "</p> " +
                                "<p> Telefono: " +  correoUsuario.telefono + " </p> " +
								"<p> Correo: " + correoUsuario.correo_to + " </p> " +
								"<p> Consulta Realizada: " + correoUsuario.contenido + "</p>";

	           	SendMail( correo, subject, body);

				string notificacion = @"<style>
                                  h1{
                                       text-color:blue;
                                       font-color:blue;
                                    }
                                </style>
                                <h1>Consulta realizada a Fisiovet!</h1>
                                <p>Su consulta sera respondida al correo y/o telefono proporcionado dentro de pronto.</p>";
				SendMail(correoUsuario.correo_to, correoUsuario.subject, notificacion);
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}


		public bool NuevoEmpleado(string to, string claveTemporal)
        {
            try
            {
                string subject = "Hemos recibido su solcitud de registro";
                string body = @"<style>
                                  h1{
                                       text-color:blue;
                                       font-color:blue;
                                    }
                                </style>
                                <h1>Seguidamente te brindamos tu contraseña temporal, te aconsejamos cambiarla en cuanto antes</h1>
                                <p>La contraseña temporal es : " + claveTemporal + " </p>";
                SendMail(to, subject, body);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
