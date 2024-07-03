<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClaveVerificacion.aspx.cs" Inherits="areaUsuarios.ClaveVerificacion" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <title>Verificación de Clave</title>
    <style>
        .vh-100 {
            min-height: 100vh;
            display: flex;
            flex-direction: column;
            justify-content: flex-start; /* Cambiado de center a flex-start */
            align-items: center;
            background: linear-gradient(to right, rgba(106, 17, 203, 1), rgba(37, 117, 252, 1));
            padding-top: 20px; /* Ajustado para agregar espacio en la parte superior */
        }
        .container-custom {
            max-width: 800px; /* Aumentado el tamaño del contenedor */
            padding: 60px; /* Aumentado el padding */
            border: 1px solid rgba(255, 255, 255, 0.2);
            border-radius: 10px;
            background-color: rgba(0, 0, 0, 0.5);
            color: white;
            box-shadow: 0px 0px 10px rgba(0, 0, 0, 0.5);
            margin-top: 150px; /* Espacio entre el título y el contenedor */
        }
        .form-control-custom {
            width: 105%;
            height: 50px; /* Aumentado el tamaño del text box */
            font-size: 1rem; /* Aumentado el tamaño de la fuente */
            margin-bottom: 20px; /* Aumentado el margen inferior */
        }
        .custom-btn {
            display: inline-block;
            padding: 10px 20px;
            font-size: 1.2rem;
            border: 3px solid white;
            color: white;
            background-color: transparent;
            cursor: pointer;
            margin-right: 20px;
        }
        .text-outline {
            margin-top: 50px;
            text-align: center;
            font-family: "Arial Black", sans-serif;
            font-weight: bold;
            font-size: 60px;
            color: #fff;
            text-shadow: 0 1px 0 #ddd, 0 2px 0 #ccc, 0 3px 0 #bbb, 0 4px 0 #aaa, 0 5px 0 #acacac, 0 6px 1px rgba(0,0,0,0.1), 0 0 5px rgba(0,0,0,0.1), 0 1px 3px rgba(0,0,0,0.3), 0 3px 5px rgba(0,0,0,0.2), 0 5px 10px rgba(0,0,0,0.25), 0 10px 10px rgba(0,0,0,0.2), 0 20px 20px rgba(0,0,0,0.15);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="vh-100 gradient-custom">
            <h1 class="display-4 text-center text-outline">Verificacion Mail</h1>
            <div class="container-custom">
                <asp:Label ID="lblMessage" runat="server" CssClass="text-center" EnableViewState="False" />
                <asp:TextBox ID="txtClave" runat="server" CssClass="form-control form-control-custom" placeholder="Ingrese su clave de verificación" />
                   <asp:Button ID="bVolver" runat="server" CssClass="custom-btn" Text="volver" OnClick="bVolver_Click" />
                <asp:Button ID="bVerificar" runat="server" CssClass="custom-btn" Text="Verificar" OnClick="bVerificar_Click" />
             <asp:Label ID="lInfo" runat="server" CssClass="text-center" />
            </div>
        </div>
    </form>
</body>
</html>
