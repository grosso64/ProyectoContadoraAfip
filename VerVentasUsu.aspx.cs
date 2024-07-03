using areaUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace areaUsuarios
{
    public partial class VerVentasUsu : System.Web.UI.Page
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

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string fileName = btn.CommandArgument;

            string idUsuario = Session["userId"].ToString();
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string query = @"
                SELECT A.FileName, A.FileData 
                FROM Adjuntos A
                INNER JOIN Ventas V ON A.Id_venta = V.Id_venta
                WHERE A.FileName = @fileName AND V.Id_Usuario = @idUsuario";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@fileName", fileName);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string contentType = "application/octet-stream";
                    string fileExtension = System.IO.Path.GetExtension(fileName).ToLower();

                    if (fileExtension == ".jpg" || fileExtension == ".jpeg")
                    {
                        contentType = "image/jpeg";
                    }
                    else if (fileExtension == ".png")
                    {
                        contentType = "image/png";
                    }
                    else if (fileExtension == ".gif")
                    {
                        contentType = "image/gif";
                    }
                    else if (fileExtension == ".bmp")
                    {
                        contentType = "image/bmp";
                    }

                    byte[] fileData = (byte[])reader["FileData"];

                    Response.Clear();
                    Response.ContentType = contentType;
                    Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
                    Response.BinaryWrite(fileData);
                    Response.End();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Label1.Text = "Error al descargar el archivo: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void bVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioUsuario.aspx");
        }

        protected void BindGridView()
        {
            string idUsuario = Session["userId"].ToString();
            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string query = @"SELECT V.Id_Venta, V.Factura, V.Aclaracion, V.monto, V.fecha_venta, V.apellido_del_usuario, V.dni_del_usuario, A.FileName, A.FileData
                     FROM Ventas V
                     INNER JOIN Adjuntos A ON V.Id_venta = A.Id_venta
                     WHERE V.Id_Usuario = @idUsuario";

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

            try
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                List<object> ventasList = new List<object>();

                while (reader.Read())
                {
                    ventasList.Add(new
                    {
                        IdVenta = reader["Id_Venta"].ToString(),
                        Factura = reader["Factura"].ToString(),
                        Aclaracion = reader["Aclaracion"].ToString(),
                        Monto = reader["Monto"].ToString(),
                        FechaVenta = Convert.ToDateTime(reader["fecha_venta"]).ToString("dd/MM/yyyy"),
                        ApellidoDelUsuario = reader["apellido_del_usuario"].ToString(),
                        DniDelUsuario = reader["dni_del_usuario"].ToString(),
                        FileData = reader["FileData"] != DBNull.Value ? (byte[])reader["FileData"] : null,
                        FileName = reader["FileName"] != DBNull.Value ? reader["FileName"].ToString() : null
                    });
                }

                gVer.DataSource = ventasList;
                gVer.DataBind();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Label1.Text = "Error al cargar las ventas: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        protected void gVer_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditVenta")
            {
                string idVenta = e.CommandArgument.ToString();
                pnlEdit.Visible = true;

                // Aquí debe agregar el código para cargar los datos de la venta en los controles de edición
                string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
                SqlConnection con = new SqlConnection(cs);
                string query = "SELECT Factura, Aclaracion, Monto FROM Ventas WHERE Id_Venta = @idVenta";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@idVenta", idVenta);

                try
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtFactura.Text = reader["Factura"].ToString();
                        txtAclaracion.Text = reader["Aclaracion"].ToString();
                        txtMonto.Text = reader["Monto"].ToString();
                        hiddenIdVenta.Value = idVenta; // Usa un campo oculto para almacenar Id_Venta
                    }
                }
                catch (Exception ex)
                {
                    // Manejo de errores
                    lInfo.Text = "Error al cargar la venta: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid)
            {
                lInfo.Text = "Existen errores en el formulario. Por favor, revisa y corrige los errores.";
                return;
            }
            string idVenta = hiddenIdVenta.Value;
            string factura = txtFactura.Text;
            string aclaracion = txtAclaracion.Text;
            decimal monto;
            string montoText = txtMonto.Text.Trim();
            // Usar CultureInfo.InvariantCulture para manejar correctamente el punto decimal
            if (!decimal.TryParse(montoText, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out monto))
            {
                // Intentar parsear con CultureInfo específica para 'es-ES'
                if (!decimal.TryParse(montoText, NumberStyles.AllowDecimalPoint, CultureInfo.CreateSpecificCulture("es-ES"), out monto))
                {
                    lInfo.Text = "Monto inválido: " + montoText; // Mostrar el valor que se está intentando parsear
                    return;
                }
            }

            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            SqlConnection con = new SqlConnection(cs);

            string query = "UPDATE Ventas SET Factura = @factura, Aclaracion = @aclaracion, Monto = @monto WHERE Id_Venta = @idVenta";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@factura", factura);
            cmd.Parameters.AddWithValue("@idVenta", idVenta);
            cmd.Parameters.AddWithValue("@aclaracion", aclaracion);
            cmd.Parameters.AddWithValue("@monto", monto);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();

                if (fuFile.HasFile)
                {
                    byte[] fileData = fuFile.FileBytes;
                    string fileName = fuFile.FileName;

                    string queryFile = @"
            IF EXISTS (SELECT 1 FROM Adjuntos WHERE Id_Venta = @idVenta)
            BEGIN
                UPDATE Adjuntos SET FileData = @fileData, FileName = @fileName WHERE Id_Venta = @idVenta
            END
            ELSE
            BEGIN
                INSERT INTO Adjuntos (Id_Venta, FileData, FileName)
                VALUES (@idVenta, @fileData, @fileName)
            END";

                    SqlCommand cmdFile = new SqlCommand(queryFile, con);
                    cmdFile.Parameters.AddWithValue("@idVenta", idVenta);
                    cmdFile.Parameters.AddWithValue("@fileData", fileData);
                    cmdFile.Parameters.AddWithValue("@fileName", fileName);
                    cmdFile.ExecuteNonQuery();
                    int idUsuario = ObtenerIdUsuarioDeVenta(idVenta, con);
                    if (idUsuario > 0)
                    {
                        // Obtener el correo electrónico, apellido y dni del usuario
                        var usuarioInfo = ObtenerEmailUsuario(idUsuario, con);
                        string email = usuarioInfo.email;
                        string apellido = usuarioInfo.apellido;
                        string dni = usuarioInfo.dni;

                        // Enviar correo electrónico
                        string mensaje = $"La venta con ID {idVenta} ha sido actualizada por el administrador con los siguientes valores:\n" +
                                         $"Factura: {factura}\n" +
                                         $"Aclaración: {aclaracion}\n" +
                                         $"Monto: {monto}\n";
                        Utilidades.RegistrarLog(idUsuario, "Actualizar Venta Area Usuario", $"Venta actualizada para el usuario {apellido}, con el dni: {dni}, numero de factura: {factura} ");
                        Utilidades.EnviarMailConAdjunto(email, "Actualización de Venta", mensaje, fileData, fileName);
                        Label2.Text = "Venta Actualizada";
                    }
                }

                pnlEdit.Visible = false;
                BindGridView();
            }
            catch (Exception ex)
            {
                // Manejo de errores
                lInfo.Text = "Error al actualizar la venta: " + ex.Message;
            }
            finally
            {
                con.Close();
            }
        }
        private int ObtenerIdUsuarioDeVenta(string idVenta, SqlConnection con)
        {
            int idUsuario = 0; // Asignar un valor predeterminado inválido
            string query = "SELECT Id_Usuario FROM Ventas WHERE Id_Venta = @idVenta";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idVenta", idVenta);

            try
            {
                object result = cmd.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int parsedId))
                {
                    idUsuario = parsedId;
                }
                else
                {
                    Label2.Text = $"No se encontró usuario para la venta con ID: {idVenta}.";
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                Label2.Text = "Error al obtener el ID del usuario: " + ex.Message;
            }

      

            return idUsuario;
        }

        private (string email, string apellido, string dni) ObtenerEmailUsuario(int idUsuario, SqlConnection con)
        {
            string email = string.Empty;
            string apellido = string.Empty;
            string dni = string.Empty;
            SqlDataReader reader = null;
            string query = "SELECT Usu_mail, Usu_apellido, Usu_dni FROM Usuarios WHERE Usu_Id = @idUsuario";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);

            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    email = reader["Usu_mail"].ToString();
                    apellido = reader["Usu_apellido"].ToString();
                    dni = reader["Usu_dni"].ToString();
                }
            }
            catch (Exception ex)
            {
                lInfo.Text = "Error al obtener los datos del usuario: " + ex.Message;
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }

            return (email, apellido, dni);
        }
        protected void ValidateFileUpload(object sender, ServerValidateEventArgs e)
        {
            if (fuFile.HasFile)
            {
                foreach (var Archivo in fuFile.PostedFiles)
                {
                    if (!Archivo.ContentType.StartsWith("image/"))
                    {
                        e.IsValid = false;
                        return;
                    }
                }
                e.IsValid = true;
            }
            else
            {
                e.IsValid = false;
            }
        }

        protected string GetImageTag(object fileData, object fileName)
        {
            if (fileData != null && fileName != null)
            {
                string base64Data = Convert.ToBase64String((byte[])fileData);
                string extension = System.IO.Path.GetExtension(fileName.ToString()).ToLower();

                if (extension == ".jpg" || extension == ".jpeg" || extension == ".png" || extension == ".gif" || extension == ".bmp")
                {
                    return $"<img src='data:image/{extension};base64,{base64Data}' alt='{fileName}' style='max-width: 100px; max-height: 100px;' />";
                }
            }
            return "";
        }
    }
}