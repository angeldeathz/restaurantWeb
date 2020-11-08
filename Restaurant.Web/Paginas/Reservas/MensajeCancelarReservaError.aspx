<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="MensajeCancelarReservaError.aspx.cs" Inherits="Restaurant.Web.Paginas.Reservas.MensajeCancelarReservaError" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <div class="m-t-md col-12 col-sm-10 mx-auto"> 
             <div class="separador-sm">&nbsp;</div>
             <div class="text-center"><asp:Image ID="imgReservaOk" runat="server" ImageUrl="~/Images/icono_advertencia.png" /></div>
             <div class="separador-sm">&nbsp;</div>
             <h2 class="text-center">Error al cancelar reserva </h2>
             <h6 class="text-center">No pudimos cancelar la reserva, por favor intente nuevamente</h6>
            <div class="separador-sm">&nbsp;</div>
            <div class="separador-sm">&nbsp;</div>
            <h6 class="text-center">Si continúa recibiendo este error, contáctenos</h6>
            <div class="separador-sm">&nbsp;</div>
            <div class="text-center">
                   <asp:Button ID="btnContacto" class="btn btn-primary" runat="server" Text="Contacto" />
            </div>
        </div>
</asp:Content>
