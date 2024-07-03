<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerUsuariosAdmin.aspx.cs" Inherits="areaUsuarios.VerUsuariosAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="css/estilo.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <title>Editar Usuarios</title>
     <script type="text/javascript">
        function confirmUpdate() {
            return confirm("¿Estás seguro de que deseas actualizar la información del usuario?");
        }
    </script>
    <style>
        .vh-100 {
            min-height: 123vh;
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
            position: sticky;
            top: 0; /* Fija los encabezados en la parte superior al desplazarse */
        }
        .table-custom td {
            border-bottom: 1px solid #ddd;
        }
        .form-container {
            width: 100%;
            background-color: #fff;
            padding: 20px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            border-radius: 10px;
        }
        .form-container .form-group {
            margin-bottom: 15px;
        }
        .form-container .form-control {
            border-radius: 5px;
            border: 1px solid #ccc;
        }
        .btn-custom {
            background-color: #150460;
            color: white;
            border: none;
            border-radius: 5px;
            padding: 10px 20px;
            cursor: pointer;
        }
        .btn-custom:hover {
            background-color: #190c6e;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <section class="vh-100 gradient-custom">
            <h2>Gestión de Usuarios</h2>
            <div class="form-group">
                <label for="tBuscarApellido">Buscar por Apellido:</label>
                <asp:TextBox ID="tBuscarApellido" runat="server" CssClass="form-control" />
            </div>
            <div class="form-group">
                <label for="tBuscarDNI">Buscar por DNI:</label>
                <asp:TextBox ID="tBuscarDNI" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnSearch" runat="server" Text="Buscar" OnClick="btnSearch_Click" CssClass="btn btn-primary" />
            <asp:Label ID="lblSearchResult" runat="server" CssClass="text-danger mt-3"></asp:Label>

            <div class="table-responsive mt-4">
               <asp:GridView ID="gVer" runat="server" AutoGenerateColumns="false" CssClass="table table-custom" OnRowCommand="gVer_RowCommand">
    <Columns>
        <asp:BoundField DataField="Usu_Id" HeaderText="ID" />
        <asp:BoundField DataField="Usu_nombre" HeaderText="Nombre" />
        <asp:BoundField DataField="Usu_apellido" HeaderText="Apellido" />
        <asp:BoundField DataField="Usu_cuit" HeaderText="CUIT" />
        <asp:BoundField DataField="Usu_dni" HeaderText="DNI" />
        <asp:BoundField DataField="Usu_mail" HeaderText="Mail" />
        <asp:TemplateField HeaderText="Rol">
            <ItemTemplate>
                <asp:DropDownList ID="ddlRol" runat="server" SelectedValue='<%# Eval("Rol_Id") %>'>
                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Estado">
            <ItemTemplate>
                <%# Convert.ToInt32(Eval("Usu_estado")) == 1 ? "Activo" : "Bloqueado" %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="CategoriaAFIP" HeaderText="Categoría AFIP" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnEdit" runat="server" Text="Editar" CommandName="EditUsuario" CommandArgument='<%# Eval("Usu_Id") %>' CssClass="btn btn-warning" />
                <asp:Button ID="btnBlockUnblock" runat="server" Text='<%# Convert.ToInt32(Eval("Usu_estado")) == 1 ? "Bloquear" : "Desbloquear" %>'
                    CommandName='<%# Convert.ToInt32(Eval("Usu_estado")) == 1 ? "BlockUsuario" : "UnblockUsuario" %>'
                    CommandArgument='<%# Eval("Usu_Id") %>'
                    CssClass='<%# Convert.ToInt32(Eval("Usu_estado")) == 1 ? "btn btn-danger" : "btn btn-success" %>'
                    OnClientClick='return confirm("¿Está seguro de que desea <%# Convert.ToInt32(Eval("Usu_estado")) == 1 ? "bloquear" : "desbloquear" %> este usuario?");' />
                <asp:Button ID="btnSaveRole" runat="server" Text="Guardar Rol" CommandName="GuardarRol" CommandArgument='<%# Eval("Usu_Id") %>' CssClass="btn btn-primary" OnClientClick='return confirm("¿Está seguro de que desea guardar este rol?");' />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

            </div>

            <asp:Button ID="bVolver" runat="server" Text="Volver" CssClass="btn btn-primary" OnClick="bVolver_Click" />


            <asp:Panel ID="pnlEdit" runat="server" Visible="False">
                <h2>Editar Usuario</h2>
                <asp:Label ID="Label2" runat="server" Text="Nombre"></asp:Label>
                <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label3" runat="server" Text="Apellido"></asp:Label>
                <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label4" runat="server" Text="CUIT"></asp:Label>
                <asp:TextBox ID="txtCuit" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label5" runat="server" Text="DNI"></asp:Label>
                <asp:TextBox ID="txtDni" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label6" runat="server" Text="Email"></asp:Label>
                <asp:TextBox ID="txtMail" runat="server"></asp:TextBox><br />
                <asp:Label ID="Label9" runat="server" Text="Categoría AFIP"></asp:Label>
                <asp:TextBox ID="txtCategoriaAFIP" runat="server"></asp:TextBox><br />
                <asp:HiddenField ID="hiddenUsuId" runat="server" />
                <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" OnClick="btnUpdate_Click" OnClientClick="return confirmUpdate();" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" />
                <asp:Label ID="lInfo" runat="server"></asp:Label>
            </asp:Panel>
            <asp:Label ID="Label1" runat="server" CssClass="text-danger" />
        </section>
    </form>
</body>
</html>
