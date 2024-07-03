<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioUsuario.aspx.cs" Inherits="areaUsuarios.WebForm1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link rel="stylesheet" type="text/css" href="css/estilo.css">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous" />
    <title>Inicio Usuario</title>
    <style type="text/css">
        body {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
        }
        .custom-btn {
            display: inline-block;
            width: 100%;
            padding: 15px;
            font-size: 1.2rem;
            border: 3px solid white;
            color: white;
            background-color: transparent;
            text-align: center;
            margin-bottom: 20px;
            cursor: pointer;
            transition: background-color 0.3s, color 0.3s;
        }
        .text-outline {
            text-shadow: 1px 1px 5px black;
        }
        .container-custom {
            padding-top: 20px;

        }
        header, footer {
            background-color: rgba(0, 0, 0, 0.7);
            color: white;
            padding: 20px 0;
            text-align: center;
        }
        footer {
            position: absolute;
            width: 100%;
            bottom: 0;
        }
        .logout-btn-container {
            position: absolute;
            top: 10px;
            right: 10px;
        }
        .logout-btn {
            padding: 10px 20px;
            font-size: 1rem;
            border: 2px solid white;
            color: white;
            background-color: transparent;
            cursor: pointer;
            transition: background-color 0.3s, color 0.3s;
        }
        .logout-btn:hover {
            background-color: white;
            color: black;
        }
     
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <header>
            <asp:Label ID="lBienvenido" runat="server" CssClass="display-4 text-outline text-center" Text="¡Hola, Usuario!"></asp:Label>
            <div class="logout-btn-container">
                <asp:Button ID="btnCerrarSesion" runat="server" CssClass="logout-btn" Text="Cerrar Sesión" OnClick="btnCerrarSesion_Click" />
            </div>
        </header>
        <section class="vh-100 gradient-custom">
            <div class="container container-custom">
                <div class="row justify-content-center">
                    <div class="col-md-6">
                        <div class="d-grid gap-2">
                            <asp:Button ID="btnSubirVentas" runat="server" CssClass="custom-btn btn btn-outline-light" Text="Subir Ventas" OnClick="btnSubirVentas_Click" />
                            <asp:Button ID="BEditarDatosUsu" runat="server" CssClass="custom-btn btn btn-outline-light" Text="Editar Datos Usuario" OnClick="BEditarDatosUsu_Click" />
                            <asp:Button ID="BEditarVentasUsu" runat="server" CssClass="custom-btn btn btn-outline-light" Text="Ver Ventas" OnClick="BEditarVentasUsu_Click" />
                          
                        </div>
                    </div>
                </div>
            </div>
        </section>
        <footer>
            <p>&copy; 2024 Plataforma de Usuarios. Todos los derechos reservados.</p>
        </footer>
    </form>
</body>
</html>







