using areaUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace areaUsuarios
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si la sesión contiene el UserId
                if (Session["UserId"] != null)
                {
                    int usuarioId = (int)Session["UserId"];

                    if (usuarioId > 0)
                    {
                        string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;

                        string query = "SELECT Usu_nombre, Usu_apellido, Usu_dni, Usu_cuit , Usu_clave, Usu_mail FROM Usuarios WHERE Usu_Id = @id";

                        using (SqlConnection connection = new SqlConnection(cs))
                        {
                            SqlCommand cmd = new SqlCommand(query, connection);
                            cmd.Parameters.AddWithValue("@id", usuarioId);

                            connection.Open();

                            SqlDataReader reader = cmd.ExecuteReader();

                            if (reader.Read())
                            {
                                string nombre = reader["Usu_nombre"].ToString();
                                string apellido = reader["Usu_apellido"].ToString();
                                string correo = reader["Usu_mail"].ToString();
                                string dni = reader["Usu_dni"].ToString();
                                string cuit = reader["Usu_cuit"].ToString();

                                // Verificar si Session["clave"] no es null antes de usarlo
                                string clave = Session["clave"] != null ? Session["clave"].ToString() : string.Empty;

                                fNombre.Text = nombre;
                                fApellido.Text = apellido;
                                fCorreo.Text = correo;
                                fDNI.Text = dni;
                                fCuit.Text = cuit;
                                fClave.Text = clave;
                            }

                            reader.Close();
                        }
                    }
                    else
                    {
                        Response.Redirect("default.aspx");
                    }
                }
                else
                {
                    Response.Redirect("default.aspx");
                }
            }
        }


        protected void bConfirmarEdit_Click(object sender, EventArgs e)
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            string nombre = fNombre.Text.Trim();
            string apellido = fApellido.Text.Trim();
            string dni = fDNI.Text.Trim();
            string cuit = fCuit.Text.Trim();
            string clave = fClave.Text.Trim();
            string correo = fCorreo.Text.Trim();
            String sha256 = Utilidades.conversorSHA256(clave);
            int usuarioId = (int)Session["UserId"];
            string clavesession = Session["clave"].ToString();

            string queryCorreo = "SELECT COUNT(*) FROM Usuarios WHERE Usu_mail = @correo AND Usu_Id != @id";
            bool correoUnico = false;

            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand cmdCorreo = new SqlCommand(queryCorreo, connection);
                cmdCorreo.Parameters.AddWithValue("@correo", correo);
                cmdCorreo.Parameters.AddWithValue("@id", usuarioId);

                connection.Open();
                int countCorreo = (int)cmdCorreo.ExecuteScalar();
                correoUnico = (countCorreo == 0);
            }

            if (!correoUnico)
            {
                lMensajeConf.Text = "El correo electrónico ya está registrado.";
                return;
            }

            if (dni.Length < 7 || dni.Length > 8)
            {
                lMensajeConf.Text = "El DNI debe tener 7 u 8 dígitos.";
                return;
            }

            if (cuit.Length != 11)
            {
                lMensajeConf.Text = "El CUIT debe tener 11 dígitos.";
                return;
            }

            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("ActualizarUsuario", connection);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@id", usuarioId);
                com.Parameters.AddWithValue("@nombre", nombre);
                com.Parameters.AddWithValue("@apellido", apellido);
                com.Parameters.AddWithValue("@dni", dni);
                com.Parameters.AddWithValue("@cuit", cuit);
                com.Parameters.AddWithValue("@clave", sha256);
                com.Parameters.AddWithValue("@correo", correo);

                try
                {
                    connection.Open();
                    com.ExecuteNonQuery();
                    lMensajeConf.Text = "Datos actualizados correctamente.";
                    Session["clave"] = clave;

                    // Registrar log
                    Utilidades.RegistrarLog(usuarioId, "Actualizar Usuario Area Usuario", $"Datos actualizados para el usuario {nombre},{apellido}, con el dni: {dni}");
                }
                catch (Exception ex)
                {
                    lMensajeConf.Text = "Error al actualizar datos: " + ex.Message;

                    // Registrar log de error
                    Utilidades.RegistrarLog(usuarioId, "Error Actualizar Usuario", ex.Message);
                }
            }
        }


        protected void bVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioUsuario.aspx");
        }

    }
}

