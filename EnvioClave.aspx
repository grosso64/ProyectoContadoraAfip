<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnvioClave.aspx.cs" Inherits="areaUsuarios.EnvioClave" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>





            <br />
            <br />
            <asp:TextBox ID="txtMail" runat="server" ValidationGroup="grupo1"></asp:TextBox>
            <br />
             <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Correo requerido!"
               ID="RFcorreo" ControlToValidate="txtMail" Display="Dynamic" ForeColor="Red" ValidationGroup="grupo1" />
             <asp:RegularExpressionValidator runat="server" ErrorMessage="Formato del correo incorrecto" 
                ID="REcorreo" ControlToValidate="txtMail" Display="Dynamic" ForeColor="Red" 
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="grupo1"></asp:RegularExpressionValidator>   
            <br />
            <br />
            <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Generar Clave" Width="112px" ValidationGroup="grupo1" />
            <br />
            <br />
            <asp:Label ID="lblInfo" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Button ID="btnVolver" runat="server" Text="Volver  " Width="117px" OnClick="btnVolver_Click" />
            <br />
            <br />





        </div>
    </form>
</body>
</html>
