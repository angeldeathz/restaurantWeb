<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PagoTarjeta.aspx.cs" Inherits="Restaurant.Web.Paginas.Autoservicio.PagoTarjeta" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<html lang="es">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Restaurante Siglo XXI</title>

    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
    </asp:PlaceHolder>

    <webopt:BundleReference runat="server" Path="~/Content/css" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

</head>
<body>
    <form id="form1" runat="server">
      <div>
        <div class="imagen-transbank pagar">
          <div class="monto-transbank pagar">
            <asp:Label ID="lblMontoPagar" runat="server"></asp:Label>
          </div>
			  	<asp:Button ID="btnPagoCredito" runat="server" Text="" CssClass="metodo-pago-credito" OnClick="btnPagoCredito_Click"/>
        	<asp:Button ID="btnPagoDebito" runat="server" Text="" CssClass="metodo-pago-debito" OnClick="btnPagoDebito_Click"/>
        </div>
      </div>
    </form>
</body>
</html>
