using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace areaUsuarios
{
    public partial class AreaAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void bVolver_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }

        protected void bEditarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerUsuariosAdmin.aspx");
        }

        protected void bVerUsuarios_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerVentasAdmin.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerLogs.aspx");
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            String power = @"E:\Microsoft Power BI Desktop\bin\PBIDesktop.exe";
            try
            {
                // Inicia Power BI Desktop
                Process.Start(power);
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción
                lblInfo.Text = ($"Error al abrir Power BI Desktop: {ex.Message}");
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("categoriaafip.aspx");
        }
    }
}