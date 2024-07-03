using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace areaUsuarios.Models
{
    public static class Utilidades
    {


        public static String conversorSHA256(String texto)
        {
            //using System.Text;
            //USAR LA REFERENCIA DE "System.Security.Cryptography"

            byte[] bytes = Encoding.UTF8.GetBytes(texto);
            using (SHA256Managed sha256 = new SHA256Managed())
            {
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder hashBuilder = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    hashBuilder.Append(hash[i].ToString("x2"));
                }
                return hashBuilder.ToString();
            }
        }


        public static string CreaCodigoAlfanumerico(int cantCaracteres)
        {

            string caracteresPermitidos = "abcdefghijklmnopqrstuvwxyz" +
                "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%^&*()_+°";


            StringBuilder codigo = new StringBuilder();
            Random random = new Random();

            for (int i = 0; i < cantCaracteres; i++)
            {
                int index = random.Next(caracteresPermitidos.Length);
                codigo.Append(caracteresPermitidos[index]);
            }
            return codigo.ToString();
        }

        /* public static string CreaCodigo(int cantCaracteres)
         {
             string strRand = null;
             Random r = new Random();

             for (int i = 0; i < cantCaracteres; i++)
             {
                 char c = (char)r.Next(48, 58); // Genera un número aleatorio entre '0' (48) y '9' (57) en ASCII
                 strRand += c;
             }

             return strRand;
         }*/
        public static string EnviarMail(string EmailDestino, string asunto, string Mensaje)
        {
            string Resultado = "Se envio la clave a tu mail";

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("sistema.unlziiii@gmail.com", "sistema.unlziiii");
                    mail.To.Add(EmailDestino);
                    mail.Subject = asunto;
                    mail.Body = Mensaje;
                    
                    
                    mail.IsBodyHtml = false;
                    mail.Priority = MailPriority.Normal;

                    using (SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587))
                    {
                        SmtpServer.Credentials = new NetworkCredential("sistema.unlziiii@gmail.com", "ltss wmpd noir tgow");
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(mail);
                    }
                }
            }
            catch (Exception)
            {
                // Resultado = ex.Message;

                Resultado = "Hubo Error en el envio de mail";


            }

            return Resultado;

        }

        public static string EnviarMailConAdjunto(string toEmail, string subject, string body, byte[] fileData, string fileName)
        {
            string Resultado = "Se envió el mail con adjunto";

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("sistema.unlziiii@gmail.com", "sistema.unlziiii");
                    mail.To.Add(toEmail);
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    mail.Priority = MailPriority.Normal;

                    if (fileData != null && !string.IsNullOrEmpty(fileName))
                    {
                        using (MemoryStream ms = new MemoryStream(fileData))
                        {
                            mail.Attachments.Add(new Attachment(ms, fileName));
                        }
                    }

                    using (SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com", 587))
                    {
                        SmtpServer.Credentials = new NetworkCredential("sistema.unlziiii@gmail.com", "ltss wmpd noir tgow");
                        SmtpServer.EnableSsl = true;

                        SmtpServer.Send(mail);
                    }
                }
            }
            catch (Exception)
            {
                Resultado = "Hubo error en el envío del mail con adjunto";
            }

            return Resultado;
        }



        public static void RegistrarLog(int usuarioId, string accion, string detalles)
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            string query = "INSERT INTO Logs (UsuarioId, Accion, Fecha, Detalles) VALUES (@usuarioId, @accion, @fecha, @detalles)";

            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
                cmd.Parameters.AddWithValue("@accion", accion);
                cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                cmd.Parameters.AddWithValue("@detalles", detalles);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }



    }
}