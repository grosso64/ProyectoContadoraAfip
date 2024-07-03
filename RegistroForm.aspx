<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroForm.aspx.cs" Inherits="LoguinPrueba.RegistroForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="css/estilo.css">
     <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <script src="JS/Validacion.js"></script>
</head>
<body class="gradient-custom">
    <form id="form1" runat="server">
            <section class="vh-100 gradient-custom">
              
                  <!--  <div class="card-body p-5 text-center">
                    
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-outline-light btn-lg px-5" PostBackUrl="~/Default.aspx">LinkButton</asp:LinkButton>
                    
                     </div>   -->
           
      <div class="container py-5 h-100">
        <div class="row d-flex justify-content-center align-items-center h-100">
          <div class="col-12 col-md-8 col-lg-6 col-xl-5">
            <div class="card bg-primary text-white" style="border-radius: 1rem;">
              <div class="card-body p-5 text-center">

                <div>

                  <h2 class="fw-bold mb-2 text-uppercase">Registro Usuario SINCOT System</h2>
                  <p class="text-white-50 mb-5">Por Favor completa los campos para registrarse</p>

                  <div data-mdb-input-init class="form-outline form-white mb-4">
                
                      <asp:TextBox ID="txtNombre" runat="server"  placeholder="Ingrese su Nombre" CssClass="form-control form-control-lg"></asp:TextBox>
                      <label class="form-label" for="lNombre">Nombre</label>
                      
                  </div>

                  <div data-mdb-input-init class="form-outline form-white mb-4">
                
                      <asp:TextBox ID="txtApellido" runat="server"  placeholder="Ingrese su Apellido" CssClass="form-control form-control-lg"></asp:TextBox>
                      <label class="form-label" for="lApellido">Apellido</label>
                  </div>
                  <div data-mdb-input-init class="form-outline form-white mb-4">
                
                      <asp:TextBox ID="txtDNI" runat="server"  placeholder="Ingrese su DNI(solo numeros)" CssClass="form-control form-control-lg" MaxLength="8"></asp:TextBox>
                      <label class="form-label" for="lDNI">DNI</label>
                  </div>

                   <div data-mdb-input-init class="form-outline form-white mb-4">
                
                      <asp:TextBox ID="txtCUIT" runat="server"  placeholder="Ingrese su CUIT(solo números)"  CssClass="form-control form-control-lg " MaxLength="11"></asp:TextBox>
                      <label class="form-label" for="lDNI">CUIT</label>
                  </div>
                   <div data-mdb-input-init class="form-outline form-white mb-4">
                
                      <asp:TextBox ID="txtEmail" runat="server"  placeholder="Ingrese su Email" TextMode="Email" CssClass="form-control form-control-lg"></asp:TextBox>
                      <label class="form-label" for="lEmail">Email</label>
                  </div> 
                    <div data-mdb-input-init class="form-outline form-white mb-4">
                
                      <asp:TextBox ID="txtPass" runat="server"  placeholder="Ingrese su PassWord" TextMode="Password" CssClass="form-control form-control-lg" MaxLength="8"></asp:TextBox>
                      <label class="form-label" for="lPass">PassWord</label>
                  </div>
                    <div data-mdb-input-init="" class="form-outline form-white mb-4">&nbsp;</div>                

                 <!-- <button data-mdb-button-init data-mdb-ripple-init class="btn btn-outline-light btn-lg px-5" type="submit">Login</button>-->
                    <asp:Button ID="btnRegistrarse" runat="server" Text="Registrarse" CssClass="btn btn-outline-light btn-lg px-5" OnClientClick="return validacionRegistro()" OnClick="btnRegistrarse_Click"  />
                    <div>&nbsp;</div>
                    <asp:Button ID="Button1" runat="server" Text="Volver al Login" CssClass="btn btn-outline-light btn-lg px-5" OnClick="Button2_Click" />
                 </div>
                  <div>&nbsp;</div>
                  <div>&nbsp;</div>
                  <asp:Label ID="lError" runat="server" Text=""></asp:Label>
              </div>
                <div class="card-body p-5 text-center">&nbsp;</div>
                
            </div>
              <div class="card bg-primary text-white" style="border-radius: 1rem;">&nbsp;</div>
          </div>
        </div>
      </div>




           
        </section>
        

    </form>
</body>
</html>
