using areaUsuarios.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoguinPrueba
{
    public partial class Default : System.Web.UI.Page
    {


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string clave = tPass.Text.Trim();
            string sha256 = Utilidades.conversorSHA256(clave);
            string email = tEmail.Text.Trim();

            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                string query = "SELECT u.Usu_Id, u.Usu_nombre, u.Usu_dni, u.Rol_Id, c.Categoria,u.Usu_estado " +
                       "FROM Usuarios u " +
                       "LEFT JOIN CategoriasAFIP c ON u.CategoriaAFIP = c.IdCategoria " +
                       "WHERE u.Usu_mail = @Email AND u.Usu_clave = @Clave";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Clave", sha256);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    int userId = Convert.ToInt32(reader["Usu_Id"]);
                    string userName = reader["Usu_nombre"].ToString();
                    int rolId = Convert.ToInt32(reader["Rol_Id"]);
                    string categoria = reader["Categoria"].ToString();
                    bool estado = Convert.ToBoolean(reader["Usu_estado"]);

                    Session["UserMail"] = email;
                    Session["UserId"] = userId;
                    Session["UserName"] = userName;
                    Session["Clave"] = clave;
                    Session["Categoria"] = categoria;

                    if (rolId == 1 && estado == true)
                    {
                        
                        //string codigo = Utilidades.CreaCodigo(6);
                        string codigo = Utilidades.CreaCodigoAlfanumerico(8);


                        string mensaje = $"Su código de verificación de clave para el ingreso al Sistema SINCOT es: {codigo}";

                        Utilidades.EnviarMail(email, "Envio código de verificación Sistema SINCOT", mensaje);
                        Session["CodigoVerificacion"] = codigo;
                        Session["CodigoVerificacionTimestamp"] = DateTime.Now;
                        Response.Redirect("ClaveVerificacion.aspx");
                        
                        Response.Redirect("InicioUsuario.aspx");

                    }
                    else if (rolId == 1 && estado == false)
                    {
                        lblInfo.Text = "Usuario no registrado o Bloqueado";

                    }





                    else if (rolId == 2)
                    {
                        /*
                        //string codigo = Utilidades.CreaCodigo(6);
                        String codigo = Utilidades.CreaCodigoAlfanumerico(8);

                        string mensaje = $"Su código de verificación de clave para el ingreso al Sistema SINCOT es: {codigo}";

                        Utilidades.EnviarMail(email, "Envio código de verificación Sistema SINCOT", mensaje);
                        Session["CodigoVerificacion"] = codigo;
                        Session["CodigoVerificacionTimestamp"] = DateTime.Now;
                        Response.Redirect("ClaveVerificacion.aspx");
                        */
                        Response.Redirect("AreaAdmin.aspx");
                    }
                }
                else
                {
                    lblInfo.Text = "Usuario no registrado o Bloqueado";
                }

                reader.Close();
            }
        }
    }
}


