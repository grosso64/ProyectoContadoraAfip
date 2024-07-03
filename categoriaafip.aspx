<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="categoriaafip.aspx.cs" Inherits="areaUsuarios.categoriaafip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        function confirmUpdate() {
            return confirm("¿Estás seguro de que deseas actualizar la información de la categoria AFIP?");
        }
        function confirmAgregar() {
            return confirm("¿Estás seguro de que deseas agregar la nueva categoria AFIP?");
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvCategorias" runat="server" AutoGenerateColumns="False" Width="555px" OnRowCommand="gvCategorias_RowCommand">
                <Columns>
                    <asp:BoundField DataField="idCategoria" HeaderText="ID" />
                    <asp:BoundField DataField="Categoria" HeaderText="Categoría" />
                    <asp:BoundField DataField="Monto" HeaderText="Monto" DataFormatString="{0:C}" />
                    <asp:TemplateField HeaderText="Estado">
                        <ItemTemplate>
                            <%# Convert.ToInt32(Eval("Estado")) == 1 ? "Activo" : "Bloqueado" %>
                         </ItemTemplate>
                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="Acciones">
                        <ItemTemplate>
                            <asp:Button ID="btnEdit" runat="server" Text="Editar" CommandName="EditCategoria" CommandArgument='<%# Eval("IdCategoria") %>' CssClass="btn btn-warning" />
                            <asp:Button ID="btnBlockUnblock" runat="server" Text='<%# Convert.ToInt32(Eval("Estado")) == 1 ? "Bloquear" : "Desbloquear" %>'
                                CommandName='<%# Convert.ToInt32(Eval("Estado")) == 1 ? "BlockUsuario" : "UnblockUsuario" %>'
                                CommandArgument='<%# Eval("IdCategoria") %>'
                                CssClass='<%# Convert.ToInt32(Eval("Estado")) == 1 ? "btn btn-danger" : "btn btn-success" %>'
                                OnClientClick='return confirm("¿Está seguro de que desea <%# Convert.ToInt32(Eval("Estado")) == 1 ? "bloquear" : "desbloquear" %> esta Categoria?");' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
        <div>
             <asp:Button ID="btnAgregar" runat="server" Text="Agregar Nueva Categoria" CssClass="btn btn-success" OnClick="btnAgregar_Click" />

            <asp:Panel ID="pnlNueva" runat="server" Visible="False">
                <h2>Nueva Categoria AFIP</h2>
            <asp:Label ID="Label3" runat="server" Text="Categoria"></asp:Label>
            <asp:TextBox ID="txtNuevaCategoria" runat="server"></asp:TextBox><br />
            <asp:Label ID="Label4" runat="server" Text="Monto"></asp:Label>
            <asp:TextBox ID="txtNuevoMonto" runat="server"></asp:TextBox><br />
            
            <asp:Button ID="btnAceptar" runat="server" Text="Agregar"  OnClientClick="return confirmAgregar();" OnClick="btnAceptar_Click" />
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click"  />
            <asp:Label ID="Label5" runat="server"></asp:Label>
        </asp:Panel> 

        </div>



        <asp:Panel ID="pnlEdit" runat="server" Visible="False">
            <h2>Editar Categoria AFIP</h2>
            <asp:Label ID="Label2" runat="server" Text="Categoria"></asp:Label>
            <asp:TextBox ID="txtCategoria" runat="server"></asp:TextBox><br />
            <asp:Label ID="Label1" runat="server" Text="Monto"></asp:Label>
            <asp:TextBox ID="txtMonto" runat="server"></asp:TextBox><br />
            <asp:HiddenField ID="hiddenCategoriaId" runat="server" />
            <asp:Button ID="btnUpdate" runat="server" Text="Actualizar" OnClick="btnUpdate_Click" OnClientClick="return confirmUpdate();" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancelar" OnClick="btnCancel_Click" />
            <asp:Label ID="lInfo" runat="server"></asp:Label>
        </asp:Panel>
        <br />
        <asp:Button ID="btnVolver" runat="server" Text="Volver" OnClick="btnVolver_Click" />
    </form>
</body>
</html>