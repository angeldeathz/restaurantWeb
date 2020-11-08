<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="MensajeCrearReservaExito.aspx.cs" Inherits="Restaurant.Web.Paginas.Reservas.DetalleReserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="m-t-md col-12 col-sm-10 mx-auto"> 
             <div class="separador-sm">&nbsp;</div>
             <div class="text-center"><asp:Image ID="imgReservaOk" runat="server" ImageUrl="~/Images/icono_ok.png" /></div>
             <div class="separador-sm">&nbsp;</div>
             <h2 class="text-center">Reserva Nº 
                 <asp:Label ID="lblIdReserva" runat="server"></asp:Label>
             </h2>
             <h5 class="text-center">Su reserva fue creada con éxito</h5>
             <h5 class="text-center">Fecha: 
                 <asp:Label ID="lblFecha" runat="server"></asp:Label>&nbsp;Hora:
                 <asp:Label ID="lblHora" runat="server"></asp:Label>
             </h5>
             <h5 class="text-center">Comensales: <asp:Label ID="lblComensales" runat="server"></asp:Label></h5>
            <div class="separador-sm">&nbsp;</div>
            <div class="separador-sm">&nbsp;</div>
            <h6 class="text-center">Le enviaremos un e-mail con la información de su reserva</h6>
            <div class="separador-sm">&nbsp;</div>
            <h6 class="text-center">Si necesita cancelar su reserva haga click
                 <a class="font-weight-bold" runat="server" href="/Paginas/Reservas/CancelarReserva">aquí</a>
            </h6>
            <div class="separador-sm">&nbsp;</div>
    
        </div>
</asp:Content>
