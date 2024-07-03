using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using areaUsuarios.Models;

namespace areaUsuarios
{
    public partial class VerUsuariosAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] != null)
                {
                    BindGridView();
                }
                else
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }


        protected void BindGridView(string apellido = "", string dni = "")
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = @"SELECT Usu_Id, Usu_nombre, Usu_apellido, Usu_cuit, Usu_dni, Usu_mail, Rol_Id, Usu_estado, CategoriaAFIP 
                             FROM Usuarios
                             WHERE (@apellido = '' OR Usu_apellido LIKE @apellido)
                             AND (@dni = '' OR Usu_dni LIKE @dni)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@apellido", "%" + apellido + "%");
            cmd.Parameters.AddWithValue("@dni", "%" + dni + "%");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            try
            {
                con.Open();
                sda.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    gVer.DataSource = dt;
                    gVer.DataBind();
                    lblSearchResult.Text = ""; 
                }
                else
                {
                    gVer.DataSource = null;
                    gVer.DataBind();
                    lblSearchResult.Text = "No se encontraron usuarios.";
                }
            }
            catch (Exception ex)
            {
                Label1.Text = "Error al cargar los usuarios: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string buscarApellido = tBuscarApellido.Text.Trim();
            string buscarDNI = tBuscarDNI.Text.Trim();
            BindGridView(buscarApellido, buscarDNI);
        }

        protected void gVer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditUsuario")
            {
                string usuId = e.CommandArgument.ToString();
                pnlEdit.Visible = true;

                string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "SELECT Usu_nombre, Usu_apellido, Usu_cuit, Usu_dni, Usu_mail, CategoriaAFIP FROM Usuarios WHERE Usu_Id = @Usu_Id";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Usu_Id", usuId);
                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtNombre.Text = reader["Usu_nombre"].ToString();
                        txtApellido.Text = reader["Usu_apellido"].ToString();
                        txtCuit.Text = reader["Usu_cuit"].ToString();
                        txtDni.Text = reader["Usu_dni"].ToString();
                        txtMail.Text = reader["Usu_mail"].ToString();
                        txtCategoriaAFIP.Text = reader["CategoriaAFIP"].ToString();
                        hiddenUsuId.Value = usuId;
                    }
                }
                catch (Exception ex)
                {
                    Label1.Text = "Error al cargar datos del usuario: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
            else if (e.CommandName == "BlockUsuario")
            {
                int usuId = Convert.ToInt32(e.CommandArgument);
                BlockUsuario(usuId);
                BindGridView();
            }
            else if (e.CommandName == "UnblockUsuario")
            {
                int usuId = Convert.ToInt32(e.CommandArgument);
                UnblockUsuario(usuId);
                BindGridView();
            }
            else if (e.CommandName == "GuardarRol")
            {
                int usuId = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                DropDownList ddlRol = (DropDownList)row.FindControl("ddlRol");
                int selectedRol = Convert.ToInt32(ddlRol.SelectedValue);
                GuardarRol(usuId, selectedRol);
                BindGridView();
            }
        }

        protected void GuardarRol(int usuId, int rolId)
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string query = "UPDATE Usuarios SET Rol_Id = @Rol_Id WHERE Usu_Id = @Usu_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Rol_Id", rolId);
            cmd.Parameters.AddWithValue("@Usu_Id", usuId);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Label1.Text = "Error al guardar el rol: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }




        private void BlockUsuario(int usuId)
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string query = "UPDATE Usuarios SET Usu_estado = 0 WHERE Usu_Id = @usuId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@usuId", usuId);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Label1.Text = "Error al bloquear el usuario: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        private void UnblockUsuario(int usuId)
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string query = "UPDATE Usuarios SET Usu_estado = 1 WHERE Usu_Id = @usuId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@usuId", usuId);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Label1.Text = "Error al desbloquear el usuario: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
       

        
protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(hiddenUsuId.Value);
            string Mail = txtMail.Text.Trim();
            string nombre = txtNombre.Text.Trim();
            string apellido = txtApellido.Text.Trim();
            string dni = txtDni.Text.Trim();
            string cuit = txtCuit.Text.Trim();
            string categoria = txtCategoriaAFIP.Text.Trim();
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "UPDATE Usuarios SET Usu_nombre=@Usu_nombre, Usu_apellido=@Usu_apellido, Usu_cuit=@Usu_cuit, Usu_dni=@Usu_dni, Usu_mail=@Usu_mail, CategoriaAFIP=@CategoriaAFIP WHERE Usu_Id=@Usu_Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Usu_Id", hiddenUsuId.Value);
            cmd.Parameters.AddWithValue("@Usu_nombre", txtNombre.Text.Trim());
            cmd.Parameters.AddWithValue("@Usu_apellido", txtApellido.Text.Trim());
            cmd.Parameters.AddWithValue("@Usu_cuit", txtCuit.Text.Trim());
            cmd.Parameters.AddWithValue("@Usu_dni", txtDni.Text.Trim());
            cmd.Parameters.AddWithValue("@Usu_mail", txtMail.Text.Trim());
            cmd.Parameters.AddWithValue("@CategoriaAFIP", txtCategoriaAFIP.Text.Trim());

            
            
            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    lInfo.Text = "Usuario actualizado correctamente.";
                    pnlEdit.Visible = false;
                    BindGridView();
                    string mensaje = "Su usuario ha sido modificado por el admin por los siguientes valores:\n" +
                 "Nombre: " + nombre + "\n" +
                 "Apellido: " + apellido + "\n" +
                 "DNI: " + dni + "\n" +
                 "CUIT: " + cuit + "\n" +
                 "Mail: " + Mail + "\n" +
                 "Categoría: " + categoria + "\n";
                    Utilidades.RegistrarLog(id, "Actualizar Usuario Area Admin", $"Datos actualizados para el usuario {nombre},{apellido}, con el dni: {dni}");
                    Utilidades.EnviarMail(Mail, "modificacion de sus datos", mensaje);
                }
                else
                {
                    lInfo.Text = "Error al actualizar el usuario.";
                }
            }
            catch (Exception ex)
            {
                lInfo.Text = "Error al actualizar el usuario: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEdit.Visible = false;
        }



        protected void bVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("AreaAdmin.aspx");
        }
    }
}