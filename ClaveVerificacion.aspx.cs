using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using areaUsuarios.Models;

namespace areaUsuarios
{
    public partial class ClaveVerificacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null || Session["UserMail"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void bVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }

        protected void bVerificar_Click(object sender, EventArgs e)
        {
            string codigover = txtClave.Text.Trim();
            string codigo = Session["CodigoVerificacion"]?.ToString();
            int userId = (int)Session["userId"];
            DateTime? codigoTimestamp = Session["CodigoVerificacionTimestamp"] as DateTime?;

            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                string query = "SELECT  Rol_Id FROM Usuarios WHERE Usu_Id = @userId";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@userId", userId);


                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    int rolId = Convert.ToInt32(reader["Rol_Id"]);


                    if (codigo != null && codigoTimestamp != null)
                    {
                        if ((DateTime.Now - codigoTimestamp.Value).TotalMinutes <= 5)
                        {
                            if (codigover == codigo)
                            {
                                if (rolId == 1)
                                {
                                    Response.Redirect("InicioUsuario.aspx");
                                }
                                if (rolId == 2)
                                {
                                    Response.Redirect("AreaAdmin.aspx");
                                }

                            }
                            else
                            {
                                lInfo.Text = "Código incorrecto";
                            }
                        }
                        else
                        {
                            lInfo.Text = "El código ha expirado";
                        }
                    }
                    else
                    {
                        lInfo.Text = "Código no encontrado. Por favor, regrese y vuelva a loguearse.";
                    }
                }
            }
        }
    }
}
