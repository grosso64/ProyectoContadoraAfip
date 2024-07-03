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
    public partial class EnvioClave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEnviar_Click(object sender, EventArgs e)
        {


            String email = txtMail.Text.Trim();

           

            //lblInfo.Text = clave;
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;

            string query = "SELECT count(*) FROM Usuarios WHERE Usu_mail = @email";
            SqlConnection con = new SqlConnection(cs);
            con.Open();


            SqlCommand cmd= new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@email",email);

            int count = (int)cmd.ExecuteScalar();
            con.Close();
            if (count==1)

            {
                String clave = Utilidades.CreaCodigoAlfanumerico(8);// x mail
                
                String claveHash = Utilidades.conversorSHA256(clave);
               
                String query2 = "Update Usuarios set Usu_clave=@clave where Usu_mail=@email2";
                SqlConnection con2 = new SqlConnection(cs);
                con2.Open();

                SqlCommand cmd2 = new SqlCommand(query2, con2);
                cmd2.Parameters.AddWithValue("@clave",claveHash);
                cmd2.Parameters.AddWithValue("@email2", email);
                
                cmd2.ExecuteNonQuery();
               
                
                con2.Close();

                //******************************************************************************
                //generar clave y enviar x mail

                string mensaje = "Su nueva clave para el ingreso al Sistema SINCOT es:" + clave;

                 //a la base de datos
                 String Resultado = Utilidades.EnviarMail(email,"Envio Clave Recuperacion Sistema SINCOT", mensaje);
                 lblInfo.Text = Resultado;
                 /**
                  UPDATE table_name
                     SET column1 = value1, column2 = value2, ...
                     WHERE condition;


                  */
                //Update en la base de datos

                Response.Redirect("Default.aspx");
                /************************************************************************* */









            }
            else
            {
                lblInfo.Text = "Mail No Registrado";
                con.Close();
            }





        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}