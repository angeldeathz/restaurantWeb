﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="MensajeCancelarReservaExito.aspx.cs" Inherits="Restaurant.Web.Paginas.Reservas.MensajeReservaCancelada" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="m-t-md col-12 col-sm-10 mx-auto"> 
             <div class="separador-sm">&nbsp;</div>
             <div class="text-center"><asp:Image ID="imgReservaOk" runat="server" ImageUrl="~/Images/icono_ok.png" /></div>
             <div class="separador-sm">&nbsp;</div>
             <h2 class="text-center">Reserva Nº 
                 <asp:Label ID="lblIdReserva" runat="server"></asp:Label>
                 &nbsp; cancelada
             </h2>
            <h6 class="text-center">su reserva fue cancelada con éxito</h6>
            <div class="separador-sm">&nbsp;</div>
            <h6 class="text-center">Si necesita crear una nueva reserva haga click
                 <a class="font-weight-bold" runat="server" href="/Paginas/Reservas/CrearReserva">aquí</a>
            </h6>
            <div class="separador-sm">&nbsp;</div>
    
        </div>
</asp:Content>
