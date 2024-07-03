using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using areaUsuarios.Models;
using System.Security.Claims;
using System.Configuration;
using System.Net.Mail;
using System.Net;

namespace LoguinPrueba
{
    public partial class RegistroForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {

            //Capturo los campos
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string cuit = txtCUIT.Text.Trim();
            string dni = txtDNI.Text.Trim();
            string pass = txtPass.Text.Trim();
            String sha256 = Utilidades.conversorSHA256(pass);
            string email = txtEmail.Text.Trim();


            //A la base de datos
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString; ;//cadena de conexion
                                                                                              // lError.Text = "Despues de la cadena de conexion";
            SqlConnection conn = new SqlConnection(cs);
            conn.Open();

            /*using (SqlConnection con = new SqlConnection(cs))
            {
                try
                {
                    
                    con.Open();
                     lError.Text = "Despues de Abrir la conexion";*/

            string query = "select count(*) from Usuarios where Usu_mail=@mail";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@mail", email);
            int count = (int)cmd.ExecuteScalar();

            if (count == 1)
            {
                lError.Text = "Usuario Registrado,Redirijase a 'Te olvidaste tu password',por favor";
            }
            else
            {
                using (SqlCommand command = new SqlCommand("InsertarUsuario", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add(new SqlParameter("@nombre", nombre));
                    command.Parameters.Add(new SqlParameter("@apellido", apellido));
                    command.Parameters.Add(new SqlParameter("@cuit", cuit));
                    command.Parameters.Add(new SqlParameter("@dni", dni));
                    command.Parameters.Add(new SqlParameter("@clave", sha256));
                    command.Parameters.Add(new SqlParameter("@mail", email));





                    command.ExecuteNonQuery();

                    lError.Text = "Usuario Registrado";
                    string mensaje = "Usted sea registrado en el sistema SINCOT, le enviamos por adjunto las condiciones y terminos de uso del sistema";
                    //hay que fijarse la direccion en el adjunto
                    string rutaAdjunto = Server.MapPath("~/PDF/declaracion.docx");
                    string mail = EnviarMail(email, "Bienvenido usuario SINCOT", mensaje, rutaAdjunto);



                }

            }



            
            //BORRO CAMPOS
            txtNombre.Text = "";
            txtApellido.Text = "";
            txtDNI.Text = "";
            txtCUIT.Text = "";
            txtEmail.Text = "";
            txtPass.Text = "";

            //Volver al Loguin

            // Server.Transfer("Default.aspx");

        }
        public string EnviarMail(string EmailDestino, string asunto, string Mensaje, string RutaAdjunto)
        {
            string Resultado = "OK";

            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress("sistema.unlziiii@gmail.com", "sistema.unlziiii");
                    mail.To.Add(EmailDestino);
                    mail.Subject = asunto;
                    mail.Body = Mensaje;
                    Attachment adjunto = new Attachment(RutaAdjunto);
                    mail.Attachments.Add(adjunto);
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
            catch (Exception ex)
            {
                Resultado = ex.Message;
            }

            return Resultado;

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}