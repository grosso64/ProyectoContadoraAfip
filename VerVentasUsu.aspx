<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerVentasUsu.aspx.cs" Inherits="areaUsuarios.VerVentasUsu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="css/estilo.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <title>Editar Ventas</title>
    <style>
        .vh-100 {
            min-height: 118vh;
            display: flex;
            flex-direction: column;
            justify-content: center;
            align-items: center;
            background-color: #f8f9fa; /* Color de fondo para toda la sección */
        }
        .table-responsive {
            max-width: 100%;
            max-height: 60vh; /* Altura máxima para la tabla */
            overflow-y: auto; /* Permite el desplazamiento vertical */
        }
        .table-custom {
            background-color: white;
            color: black;
            border-radius: 10px;
            overflow: hidden;
            width: 100%;
        }
        .table-custom th, .table-custom td {
            padding: 15px;
            text-align: center;
        }
        .table-custom th {
            background-color: #150460;
            color: white;
        }
        .custom-btn2 {
            display: inline-block;
            padding: 10px 20px;
            font-size: 1.2rem;
            border: 3px solid white;
            color: white;
            background-color: transparent;
            cursor: pointer;
            margin-right: 10px;
        }
        .container-scroll {
            width: 100%;
            background-color: #f8f9fa; /* Para mantener el color de fondo del section */
            padding: 15px;
            border-radius: 10px;
            display: none;
        }
        .container-scroll.show {
            display: block;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="vh-100 gradient-custom">
            <br />
            <br />
            <asp:Label runat="server" Text="Ver Ventas" class="display-4 text-center text-outline mt" style="display: block; text-align: center;" />
            <br />
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gVer" runat="server" AutoGenerateColumns="False" CssClass="table table-custom" OnRowCommand="gVer_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="Factura" HeaderText="Factura" />
                                    <asp:BoundField DataField="Aclaracion" HeaderText="Aclaración" />
                                    <asp:BoundField DataField="Monto" HeaderText="Monto" />
                                    <asp:BoundField DataField="FechaVenta" HeaderText="Fecha de Venta" />
                                    <asp:BoundField DataField="ApellidoDelUsuario" HeaderText="Apellido del Usuario" />
                                    <asp:BoundField DataField="DniDelUsuario" HeaderText="DNI del Usuario" />
                                    <asp:TemplateField HeaderText="Imagen">
                                        <ItemTemplate>
                                            <asp:Literal ID="imgProductoLiteral" runat="server" Text='<%# GetImageTag(Eval("FileData"), Eval("FileName")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDownload" runat="server" CommandArgument='<%# Eval("FileName") %>' OnClick="btnDownload_Click">Descargar</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" CommandName="EditVenta" CommandArgument='<%# Eval("IdVenta") %>' Text="Edit" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-6">
                
                            <asp:Panel ID="pnlEdit" runat="server" Visible="false">
                                <h3>Editar Venta</h3>
                                <asp:Label ID="lblFactura" runat="server" Text="Factura:" AssociatedControlID="txtFactura"></asp:Label>
                                <asp:TextBox ID="txtFactura" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Numero requerido!" ID="RFnombreFactura" ControlToValidate="txtFactura" Display="Dynamic" ForeColor="Red" ValidationGroup="ventas" />
                                <asp:RegularExpressionValidator runat="server" ErrorMessage="¡Formato incorrecto, solo números y guiones!" ID="REnombreFactura" ControlToValidate="txtFactura" Display="Dynamic" ForeColor="Red" ValidationExpression="^[\d-]+$" ValidationGroup="ventas" />
                                <asp:Label ID="lblAclaracion" runat="server" Text="Aclaración:" AssociatedControlID="txtAclaracion"></asp:Label>
                                <asp:TextBox ID="txtAclaracion" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:Label ID="lblMonto" runat="server" Text="Monto:" AssociatedControlID="txtMonto"></asp:Label>
                                <asp:TextBox ID="txtMonto" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Monto requerido!" ID="RFmonto" ControlToValidate="txtMonto" Display="Dynamic" ForeColor="Red" ValidationGroup="ventas" />
                                <asp:RegularExpressionValidator runat="server" ErrorMessage="¡Formato del monto inválido!" ID="REmonto" ControlToValidate="txtMonto" Display="Dynamic" ForeColor="Red" ValidationExpression="^\d+(,\d{1,2})?$|^\d+(\.\d{1,2})?$" ValidationGroup="ventas" />
                                <asp:Label ID="lblFile" runat="server" Text="Archivo adjunto:"></asp:Label>
                                <asp:FileUpload ID="fuFile" runat="server" CssClass="form-control-file" />
                                <asp:CustomValidator ID="fileUploadValidator" runat="server" ErrorMessage="¡Debe subir al menos una imagen!" OnServerValidate="ValidateFileUpload" Display="Dynamic" ForeColor="Red" ValidationGroup="ventas" />
                                <br />
                                <asp:HiddenField ID="hiddenIdVenta" runat="server" />
                                <br />
                                <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" CssClass="btn btn-primary" OnClick="btnUpdate_Click" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True" ShowSummary="False" CssClass="validation-summary-errors" ValidationGroup="ventas" />
                                <asp:Label ID="lInfo" runat="server" CssClass="text-danger"></asp:Label>
                            </asp:Panel>
                        </div>
                   
                </div>
            </div>
            <br />
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-6 text-center">
                        <asp:Button ID="bVolver" CausesValidation="false" runat="server" CssClass="custom-btn2 btn btn-outline-light" Text="Volver" OnClick="bVolver_Click" />
                        <div>
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <asp:Label runat="server" id="Label2" class="text-center text-outline mt" style="display: block; text-align: center;" />
        </div>
    </form>
</body>
</html>

