using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace areaUsuarios
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verificar si la sesión contiene el ID y el nombre del usuario
                if (Session["UserId"] != null && Session["UserName"] != null)
                {
                    int userId = (int)Session["UserId"];
                    string userName = Session["UserName"].ToString();
                    string clave = Session["clave"].ToString();
                    string categoria = Session["categoria"].ToString();
                    userName = char.ToUpper(userName[0]) + userName.Substring(1);

                    // Utilizar el ID y el nombre del usuario como desees
                    lBienvenido.Text = "Bienvenido" + " " + userName + " "+"; "+"Categoria:"+categoria;
                }
                else
                {
                    // Si la sesión no contiene la información del usuario, redirigir al usuario al login
                    Response.Redirect("Default.aspx");

                }
            }
        }



        protected void btnSubirVentas_Click(object sender, EventArgs e)
        {
            Response.Redirect("VentasUsuario.aspx");
        }

        protected void BEditarDatosUsu_Click(object sender, EventArgs e)
        {
            Response.Redirect("EditarUsuario.aspx");
        }

        protected void BEditarVentasUsu_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerVentasUsu.aspx");
        }

      

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("Default.aspx");
        }
    }
}