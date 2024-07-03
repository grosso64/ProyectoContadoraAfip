<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LoguinPrueba.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<link rel="stylesheet" type="text/css" href="css/estilo.css">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous"/>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-MrcW6ZMFYlzcLA8Nl+NtUVF0sA7MsXsP1UyJoMp4YLEuNSfAP+JcXn/tWtIaxVXM" crossorigin="anonymous"></script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <section class="vh-100 gradient-custom">
  <div class="container py-5 h-100">
    <div class="row d-flex justify-content-center align-items-center h-100">
      <div class="col-12 col-md-8 col-lg-6 col-xl-5">
        <div class="card bg-primary text-white" style="border-radius: 1rem;">
          <div class="card-body p-5 text-center">

            <div class="mb-md-5 mt-md-4 pb-5">

              <h2 class="fw-bold mb-2 text-uppercase">Login SINCOT System</h2>
              <p class="text-white-50 mb-5">Por Favor Ingresa tu Email y tu Password!</p>

              <div data-mdb-input-init class="form-outline form-white mb-4">
                
                  <asp:TextBox ID="tEmail" runat="server" TextMode="Email" placeholder="Ingrese  Email" CssClass="form-control form-control-lg"></asp:TextBox>
                  <label class="form-label" for="tEmail">Email</label>
              </div>

              <div data-mdb-input-init class="form-outline form-white mb-4">
                
                  <asp:TextBox ID="tPass" runat="server" TextMode="Password" placeholder="Ingrese Password" CssClass="form-control form-control-lg"></asp:TextBox>
                  <label class="form-label" for="tPass">Password</label>
              </div>

              <p class="small mb-5 pb-lg-2"><a class="text-white-50" href="Envioclave.aspx">Te olvidaste tu Password?</a></p>

              <!--<button data-mdb-button-init data-mdb-ripple-init class="btn btn-outline-light btn-lg px-5" type="submit">Login</button>-->

                <asp:Button ID="Button1" runat="server" Text="Entrar" class="btn btn-outline-light btn-lg px-5" OnClick="Button1_Click" />

            </div>
              <asp:Label ID="lblInfo" runat="server" Text=""></asp:Label>

            <div>
              <p class="mb-0">No tienes cuenta de Usuario? <a href="RegistroForm.aspx" class="text-white-50 fw-bold">Registrate</a>
              </p>
            </div>

          </div>
        </div>
      </div>
    </div>
  </div>
</section>











    </form>
</body>
</html>
