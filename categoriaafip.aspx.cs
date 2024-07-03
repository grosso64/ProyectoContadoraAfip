using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace areaUsuarios
{
    public partial class categoriaafip : System.Web.UI.Page
    {
        private String cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindGridview();
            }
        }

        private void bindGridview()
        {
            SqlConnection con = new SqlConnection(cs);
            String query = "Select * from CategoriasAFIP";
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            List<object> ventasList = new List<object>();

            while (dr.Read())
            {
                ventasList.Add(new
                {
                    IdCategoria = Convert.ToInt32(dr["IdCategoria"]),
                    Categoria = dr["Categoria"].ToString(),
                    Monto = Convert.ToDouble(dr["Monto"].ToString()),
                    Estado = Convert.ToBoolean(dr["Estado"])
                });
            }
            con.Close();

            gvCategorias.DataSource = ventasList;
            gvCategorias.DataBind();
        }

        protected void gvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCategoria")
            {
                string CategoriaId = e.CommandArgument.ToString();
                pnlEdit.Visible = true;

                SqlConnection con = new SqlConnection(cs);
                string query = "SELECT IdCategoria, Categoria, Monto FROM CategoriasAFIP WHERE IdCategoria=@categoriaId";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("categoriaId", CategoriaId);

                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtCategoria.Text = reader["Categoria"].ToString();
                    txtMonto.Text = reader["Monto"].ToString();
                    hiddenCategoriaId.Value = CategoriaId;
                }
                con.Close();
            }
            else if (e.CommandName == "BlockUsuario")
            {
                int IdCategoria = Convert.ToInt32(e.CommandArgument);
                BlockUsuario(IdCategoria);
                bindGridview();
            }
            else if (e.CommandName == "UnblockUsuario")
            {
                int IdCategoria = Convert.ToInt32(e.CommandArgument);
                UnblockUsuario(IdCategoria);
                bindGridview();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            String categoria = txtCategoria.Text.Trim();
            String monto = txtMonto.Text.Trim();
            String query = "UPDATE CategoriasAFIP SET Categoria=@categoria, Monto=@monto WHERE IdCategoria=@idCategoria";
            SqlConnection con = new SqlConnection(cs);

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idCategoria", hiddenCategoriaId.Value);
            cmd.Parameters.AddWithValue("@categoria", categoria);
            cmd.Parameters.AddWithValue("@monto", monto);

            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

            txtCategoria.Text = "";
            txtMonto.Text = "";
            pnlEdit.Visible = false;
            bindGridview();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            pnlEdit.Visible = false;
        }


        private void BlockUsuario(int IdCategoria)
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string query = "UPDATE CategoriasAFIP SET Estado = 0 WHERE IdCategoria = @CategoriaId";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CategoriaId", IdCategoria);

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

        protected void UnblockUsuario(int IdCategoria)
        {
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);
            string query = "UPDATE CategoriasAFIP SET Estado = 1 WHERE IdCategoria = @CategoriaId";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@CategoriaId", IdCategoria);

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

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            pnlNueva.Visible = true;
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlNueva.Visible = false;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            String categoria = txtNuevaCategoria.Text.Trim();
            Double monto = Convert.ToDouble(txtNuevoMonto.Text.Trim());

            SqlConnection con = new SqlConnection(cs);
            con.Open();
            String query = "insert into CategoriasAFIP (Categoria,Monto,Estado) values (@categoria,@monto,1)";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@categoria", categoria);
            cmd.Parameters.AddWithValue("@monto", monto);
            cmd.ExecuteNonQuery();
            txtNuevaCategoria.Text = "";
            txtNuevoMonto.Text = "";

            pnlNueva.Visible = false;
            bindGridview();



        }

        protected void btnVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("AreaAdmin.aspx");
        }
    }
}