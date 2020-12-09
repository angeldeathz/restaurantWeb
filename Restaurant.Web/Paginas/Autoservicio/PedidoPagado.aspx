<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="PedidoPagado.aspx.cs" Inherits="Restaurant.Web.Paginas.Autoservicio.PedidoPagado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div class="m-t-md col-12 col-sm-10 mx-auto"> 
    <div class="separador-sm">&nbsp;</div>
    <div class="text-center"><asp:Image ID="imgReservaOk" runat="server" ImageUrl="~/Images/icono_ok.png" /></div>
    <div class="separador-sm">&nbsp;</div>
    <h2 class="text-center">Pedido pagado</h2>
    <h5 class="text-center">Transacción realizada con éxito</h5>  
    <div class="separador-sm">&nbsp;</div>
    <div class="bg-blanco rounded shadow p-3">
      <h4 class="text-center">Detalle del pedido</h4>
      <asp:UpdatePanel ID="upArticulosPedido" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
          <ContentTemplate>
            <div class="text-center">
              <asp:Label ID="listaArticulosPedidoVacia" runat="server" 
                  Text="No hay artículos en al pedido" CssClass="d-inline-block h5 my-5"></asp:Label>
            </div>   
            <div class="table-responsive pt-3">
              <asp:Repeater ID="listaArticulosPedido" runat="server">
              <HeaderTemplate>
                <table border="1" class="table">
                  <tr>
                      <td><b>Nombre</b></td>
                      <td><b>Precio</b></td>
                      <td><b>Cantidad</b></td>
                      <td><b>Total</b></td>
                  </tr>
              </HeaderTemplate>          
              <ItemTemplate>
                  <tr>
                  <td><%# Eval("Articulo.Nombre") %></td>
                  <td><%# Eval("Precio", "${0:N0}") %></td>
                  <td><%# Eval("Cantidad") %></td>
                  <td><%# Eval("Total", "${0:N0}") %></td>
                  </tr>
              </ItemTemplate>
              <FooterTemplate>
                  </table>
              </FooterTemplate>
              </asp:Repeater>
            </div>
            <div class="text-right font-weight-bold">
              <asp:Label ID="lblTotalPedido" runat="server"></asp:Label>
              <asp:TextBox ID="txtTotalPedido" runat="server" Visible="false"></asp:TextBox>
            </div>
        </ContentTemplate>
      </asp:UpdatePanel>
    </div>
    <div class="separador-sm">&nbsp;</div>
    <h6 class="text-center">Le hemos enviado un e-mail con la información del pago</h6>
  </div>
</asp:Content>
