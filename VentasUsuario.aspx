<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VentasUsuario.aspx.cs" Inherits="areaUsuarios.VentasUsuario" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="css/estilo.css" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <title>Subir Ventas</title>
    <style>
        .text-outline {
            text-shadow: 1px 1px 5px black;
        }

        .custom-btn {
            display: inline-block;
            padding: 10px 20px;
            font-size: 1.2rem;
            border: 3px solid white;
            color: white;
            background-color: transparent;
            cursor: pointer;
            margin-right: 10px;
        }

        .form-control {
            width: 100%;
        }

        .container-custom {
            max-width: 600px;
            margin: auto;
            padding: 20px;
            border: 1px solid rgba(255, 255, 255, 0.2);
            border-radius: 10px;
            background-color: rgba(0, 0, 0, 0.5);
            color: white;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.5);
        }

        .vh-100 {
            display: flex;
            align-items: center;
            justify-content: center;
            height: 100vh;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" enctype="multipart/form-data">
        <section class="vh-100 gradient-custom">
            <div class="container-custom">
                <h1 class="display-4 text-center text-outline">Subir Ventas</h1>
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label runat="server" CssClass="text-center" Font-Size="15" Text="Numero de Factura:" />
                        <asp:TextBox runat="server" ID="tNombreFactura" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Numero requerido!" ID="RFnombreFactura" ControlToValidate="tNombreFactura" Display="Dynamic" ForeColor="Red" ValidationGroup="ventas" />
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="¡Formato incorrecto, solo números y guiones!" ID="REnombreFactura" ControlToValidate="tNombreFactura" Display="Dynamic" ForeColor="Red" ValidationExpression="^[\d-]+$" ValidationGroup="ventas" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-6">
                        <asp:Label runat="server" CssClass="text-center" Font-Size="15" Text="Monto:" />
                        <asp:TextBox runat="server" ID="tMonto" CssClass="form-control" />
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Monto requerido!" ID="RFmonto" ControlToValidate="tMonto" Display="Dynamic" ForeColor="Red" ValidationGroup="ventas" />
                        <asp:RegularExpressionValidator runat="server" ErrorMessage="¡Formato del monto inválido!" ID="REmonto" ControlToValidate="tMonto" Display="Dynamic" ForeColor="Red" ValidationExpression="^\d+(,\d{1,2})?$|^\d+(\.\d{1,2})?$" ValidationGroup="ventas" />
                    </div>
                    <div class="col-md-6">
                        <asp:Label runat="server" CssClass="text-center" Font-Size="15" Text="Adjuntos:" />
                        <asp:FileUpload ID="fileUpload" runat="server" AllowMultiple="true" />
                        <asp:CustomValidator ID="fileUploadValidator" runat="server" ErrorMessage="¡Debe subir al menos una imagen!" OnServerValidate="ValidateFileUpload" Display="Dynamic" ForeColor="Red" ValidationGroup="ventas" />
                    </div>
                </div>
                <br />
                <div class="row">
                    <div class="col-md-12">
                        <asp:Label runat="server" CssClass="text-center" Font-Size="15" Text="Aclaraciones:" />
                        <asp:TextBox runat="server" ID="tAclaraciones" CssClass="form-control" TextMode="MultiLine" Rows="4" />
                    </div>
                </div>
                <br />
                <div class="row justify-content-center">
                    <div class="col-md-4 text-center">
                        <asp:Button ID="bVolver" CausesValidation="false" runat="server" CssClass="custom-btn" Text="Volver" OnClick="bVolver_Click" />
                    </div>
                    <div class="col-md-4 text-center">
                        <asp:Button ID="bConfirmarVenta" runat="server" CssClass="custom-btn" Text="Subir Ventas" OnClick="bConfVenta_Click" ValidationGroup="ventas" />
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" CssClass="validation-summary-errors" ValidationGroup="ventas" />
                        <div><asp:Label ID="lMensajeConf" runat="server"></asp:Label></div>
                    </div>
                </div>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" CssClass="validation-summary-errors" />
        </section>
    </form>
</body>
</html>