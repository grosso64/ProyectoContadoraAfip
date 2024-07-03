using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace areaUsuarios
{
    public partial class VerLogs : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] != null)
                {
                    CargarLogs();
                }
                else
                {
                   Response.Redirect("default.aspx");
                }
                   
            }
            
        }

        private void CargarLogs()
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            DataTable dtLogs = new DataTable();

            using (SqlConnection connection = new SqlConnection(cs))
            {
                string query = "SELECT LogId, UsuarioId, Accion, Fecha, Detalles FROM Logs ORDER BY Fecha DESC";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        connection.Open();
                        da.Fill(dtLogs);
                    }
                }
            }

            gvLogs.DataSource = dtLogs;
            gvLogs.DataBind();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("AreaAdmin.aspx");
        }
    }
}