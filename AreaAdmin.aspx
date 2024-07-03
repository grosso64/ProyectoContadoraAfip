<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AreaAdmin.aspx.cs" Inherits="areaUsuarios.AreaAdmin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
            margin-left: 390px;
        }
        .auto-style2 {
            text-align: center;
        }
        
    </style>
</head>
<body style="height: 596px">
    <form id="form1" runat="server">
        <asp:Panel ID="Panel1" runat="server" CssClass="auto-style1" Height="582px" Width="1017px">
            <div class="auto-style2">
                <br />
                <br />
                BIENVENIDO<br />
                <br />
                <asp:Button ID="bEditarUsuario" runat="server" Text="Editar Usuario" Width="194px" OnClick="bEditarUsuario_Click" />
                <br />
                <br />
                <asp:Button ID="bVerUsuarios" runat="server" Text="Ver Ventas Usuarios" Width="199px" OnClick="bVerUsuarios_Click" />
                <br />
                <br />
                <asp:Button ID="Button1" runat="server" Text="Editar Categorias AFIP" OnClick="Button1_Click" />
                <br />
                <br />
                <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Logs tabl usuarios" Width="196px" />
                <br />
                <br />
                <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="Abrir Power BI" Width="196px" />
                <br />

                <br />
                <asp:Label ID="lblInfo" runat="server"></asp:Label>

                <br />
                <br />
                <asp:Button ID="bVolver" runat="server" OnClick="bVolver_Click" Text="Volver" />
            </div>
        </asp:Panel>
    </form>
</body>
</html>
