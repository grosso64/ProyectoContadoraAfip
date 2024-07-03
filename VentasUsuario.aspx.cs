using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace areaUsuarios
{
    public partial class VentasUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserId"] == null || Session["UserName"] == null)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }

        protected void bConfVenta_Click(object sender, EventArgs e)
        {
            string nombreFactura = tNombreFactura.Text.Trim();
            decimal monto;
            string aclaraciones = tAclaraciones.Text.Trim();
            int userId = (int)Session["UserId"];

            // Validar y convertir monto
            if (!decimal.TryParse(tMonto.Text.Trim(), System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.CultureInfo.InvariantCulture, out monto))
            {
                lMensajeConf.Text = "Monto inválido";
                return;
            }

            // Crear un DataTable para los adjuntos
            DataTable adjuntosTable = new DataTable();
            adjuntosTable.Columns.Add("FileName", typeof(string));
            adjuntosTable.Columns.Add("FileData", typeof(byte[]));

            // Procesar los archivos adjuntos
            if (fileUpload.HasFiles)
            {
                foreach (HttpPostedFile uploadedFile in fileUpload.PostedFiles)
                {
                    string fileName = Path.GetFileName(uploadedFile.FileName);
                    byte[] fileData;

                    using (BinaryReader br = new BinaryReader(uploadedFile.InputStream))
                    {
                        fileData = br.ReadBytes(uploadedFile.ContentLength);
                    }

                    // Agregar fila al DataTable
                    adjuntosTable.Rows.Add(fileName, fileData);
                }
            }

            string cs = ConfigurationManager.ConnectionStrings["sincotDB"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(cs))
            {
                SqlCommand com = new SqlCommand("InsertarVenta", connection);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@nombreFactura", nombreFactura);
                com.Parameters.AddWithValue("@monto", monto);
                com.Parameters.AddWithValue("@aclaraciones", aclaraciones);
                com.Parameters.AddWithValue("@usu_id", userId);

                // Agregar el parámetro del tipo de tabla
                SqlParameter adjuntosParam = com.Parameters.AddWithValue("@adjuntos", adjuntosTable);
                adjuntosParam.SqlDbType = SqlDbType.Structured;
                adjuntosParam.TypeName = "dbo.AdjuntoTableType";

                try
                {
                    connection.Open();
                    com.ExecuteNonQuery();
                    lMensajeConf.Text = "Venta confirmada";
                    tMonto.Text = "";
                    tNombreFactura.Text = "";
                    tAclaraciones.Text = "";
                }
                catch (Exception ex)
                {
                    lMensajeConf.Text = "Error al confirmar la venta: " + ex.Message;
                }
            }
        }
        protected void ValidateFileUpload(object sender, ServerValidateEventArgs e)
        {
            if (fileUpload.HasFile)
            {
                foreach (var Archivo in fileUpload.PostedFiles)
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

        protected void bVolver_Click(object sender, EventArgs e)
        {
            Response.Redirect("InicioUsuario.aspx");
        }
    }
}