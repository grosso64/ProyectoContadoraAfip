<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VerLogs.aspx.cs" Inherits="areaUsuarios.VerLogs" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ver Logs</title>
    <style>
        .vh-100 {
            min-height: 100vh;
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
         <section class="vh-100 gradient-custom">
            <br />
            <br />
            <asp:Label runat="server" Text="Ver Logs" CssClass="display-4 text-center text-outline mt" style="display: block; text-align: center;" />
            <br />
            
            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvLogs" runat="server" AutoGenerateColumns="false" CssClass="table table-custom">
                                <Columns>
                                    <asp:BoundField DataField="LogId" HeaderText="ID" />
                                    <asp:BoundField DataField="UsuarioId" HeaderText="Usuario ID" />
                                    <asp:BoundField DataField="Accion" HeaderText="Acción" />
                                    <asp:BoundField DataField="Fecha" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm:ss}" />
                                    <asp:BoundField DataField="Detalles" HeaderText="Detalles" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
             <br /> 
             <asp:Button ID="Button1" runat="server" Text="Volver" OnClick="Button1_Click" />
       
        </section>
           


    </form>
</body>
</html>