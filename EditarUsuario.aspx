<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditarUsuario.aspx.cs" Inherits="areaUsuarios.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="stylesheet" type="text/css" href="css/estilo.css"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <title></title>
    <style>
      .table-hover tbody tr.table-active th:first-child {
    border-right: 1px solid black; /* Borde derecho */
    font-family: inherit; /* Heredar la fuente del elemento padre (anula la fuente modificada globalmente) */
    font-weight: inherit; /* Heredar el peso de la fuente del elemento padre */
    font-size: inherit; /* Heredar el tamaño de la fuente del elemento padre */
}
    
.custom-btn {
        display: inline-block;
        padding: 5px 50px; /* Ajusta el padding según sea necesario */
        font-size: 1.5rem; /* Tamaño de fuente personalizado */
        border: 3px solid white; /* Borde sólido blanco */
        color: white; /* Color de texto */
        background-color: transparent; /* Fondo transparente */
        cursor: pointer;
        right: 300px;
    }
.custom-btn2 {
        display: inline-block;
        padding: 5px 30px; /* Ajusta el padding según sea necesario */
        font-size: 1.5rem; /* Tamaño de fuente personalizado */
        border: 3px solid white; /* Borde sólido blanco */
        color: white; /* Color de texto */
        background-color: transparent; /* Fondo transparente */
        cursor: pointer;
        right: 300px;
    }

    /* Estilos para el ValidationSummary */
    .validation-summary-errors {
        border: 2px solid #f44336; /* Color del borde */
        background-color: #f8d7da; /* Color de fondo */
        color: #721c24; /* Color del texto */
        padding: 10px; /* Espaciado interior */
        margin-bottom: 10px; /* Margen inferior */
    }
</style>
   
</head>
<body>
    <form id="form1" runat="server">
         <section class="vh-100 gradient-custom">
             <br/>
             <br/>
             <br/>
             <div >
       
              <h1 class="display-4  text-center text-outline mt">Modifica tus datos</h1>
                 </div>
            <br />
             <br />
      <div class="container"> 
    <div class="row justify-content-center ">
        <div class="col-md-13">
           <table border="1">
                <thead>
                    <tr>
                        <th scope="col" style="width: 9%; font-size: 16px;">Tipo</th>
                        <th scope="col" style="width: 15%; font-size: 20px;">Nombre</th>
                        <th scope="col" style="width: 15%; font-size: 20px;">Apellido</th>
                        <th scope="col" style="width: 15%; font-size: 20px;">DNI</th>
                        <th scope="col" style="width: 15%; font-size: 20px;">CUIT(SIN GUION)</th>
                        <th scope="col" style="width: 15%; font-size: 20px;">Correo Electrónico</th>
                        <th scope="col" style="width: 15%; font-size: 20px;">Contraseña</th>
                    </tr>
                </thead>
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="color: white; font-size: 9px;">tus datos</th>
                        <td contenteditable="false" style="font-size: 15px;">
                            <asp:TextBox runat="server" ID="fNombre"  Height="31px" Width="137px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Nombre requerido!" ID="RFnombre" ControlToValidate="fNombre" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="¡Solo caracteres permitidos!"
        ID="REnombre" ControlToValidate="fNombre" Display="Dynamic" ForeColor="Red"
        ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$" />
                        </td>
                        <td contenteditable="false" style="font-size: 15px;">
                            <asp:TextBox runat="server" ID="fApellido"  Height="31px" Width="137px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Apellido requerido!"
                                ID="RFapellido" ControlToValidate="fApellido" Display="Dynamic" ForeColor="Red" />
                             <asp:RegularExpressionValidator runat="server" ErrorMessage="¡Solo caracteres permitidos!"
        ID="REapellido" ControlToValidate="fApellido" Display="Dynamic" ForeColor="Red"
        ValidationExpression="^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$" />
                        </td>
                        <td contenteditable="false" style="font-size: 15px;">
                       <asp:TextBox runat="server" ID="fDNI" Height="31px" Width="137px" MaxLength="8"></asp:TextBox>
<asp:RequiredFieldValidator runat="server" ErrorMessage="¡DNI requerido!"
    ID="RFdni" ControlToValidate="fDNI" Display="Dynamic" ForeColor="Red" />
<asp:RegularExpressionValidator runat="server" ErrorMessage="¡Formato de DNI inválido!"
    ID="REdni" ControlToValidate="fDNI" Display="Dynamic" ForeColor="Red"
    ValidationExpression="^\d{7,8}[A-Za-z]?$" />
                        </td>
                        <td contenteditable="false" style="font-size: 15px;">
                            <asp:TextBox runat="server" ID="fCuit" Height="31px" Width="137px" MaxLength="12"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="¡CUIT requerido!"
                                ID="RFcuit" ControlToValidate="fCuit" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Formato de CUIT inválido" 
    ID="REcuit" ControlToValidate="fCuit" Display="Dynamic" ForeColor="Red" 
    ValidationExpression="^\d{11}$"></asp:RegularExpressionValidator>
                        </td>
                        <td contenteditable="false" style="font-size: 15px;">
                            <asp:TextBox runat="server" ID="fCorreo" Height="31px" Width="137px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Correo requerido!"
                                ID="RFcorreo" ControlToValidate="fCorreo" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator runat="server" ErrorMessage="Formato del correo incorrecto" 
    ID="REcorreo" ControlToValidate="fCorreo" Display="Dynamic" ForeColor="Red" 
    ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        </td>
                        <td contenteditable="false" style="font-size: 15px;">
                            <asp:TextBox runat="server" ID="fClave" Height="31px" Width="137px"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ErrorMessage="¡Contraseña requerida!"
                                ID="RFclave" ControlToValidate="fClave" Display="Dynamic" ForeColor="Red" />
                            <asp:RegularExpressionValidator runat="server" ID="REclave" ErrorMessage="La contraseña debe tener al menos 6 caracteres, sin caracteres especiales"
    ControlToValidate="fClave" ValidationExpression="^[a-zA-Z0-9]{6,}$"
    Display="Dynamic" ForeColor="Red" />

                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
          </div>
  
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
             <br />
            
    <div class="container">
     <div class="row justify-content-center" >
       
       <div class="col-6"  >
            <asp:Button ID="bVolver" CausesValidation="false" runat="server"  CssClass="custom-btn2 btn btn-outline-light" Text=" volver" OnClick="bVolver_Click" />
    <div><asp:Label ID="Label1" runat="server"></asp:Label></div>
        </div>
        <div class="col-4">
    <asp:Button ID="bConfirmarEdit" runat="server"  CssClass="custom-btn2 btn btn-outline-light" Text="Editar Datos" OnClick="bConfirmarEdit_Click" />
    <div><asp:Label ID="lMensajeConf" runat="server"></asp:Label></div>
</div>
        </div>
  
       
    </div>
           
             <asp:ValidationSummary  ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" CssClass="validation-summary-errors" />
   
             
             </section>
        
    </form>
</body>
</html>
