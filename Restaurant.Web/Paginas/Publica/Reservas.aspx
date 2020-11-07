<%@ Page Title="Reservas" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Reservas.aspx.cs" Inherits="Restaurant.Web.Paginas.Publica.Reservas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row">
        <div class="col-12 col-md-5 mx-auto my-5 text-center bg-light rounded py-4 shadow">
            <h2>Sistema de Reservas</h2>
            <p class="lead text-rosado">
                Puedes reservar tu mesa con antelación o cancelar una reserva
            </p>
            <div class="col-10 col-md-8 mx-auto my-4">
                 <a class="btn btn-lg btn-info btn-block" href="/Paginas/Reservas/CrearReserva">Reservar Mesa</a>
            </div>
            <div class="col-10 col-md-8 mx-auto my-4">
                 <a class="btn btn-lg btn-danger btn-block" href="/Paginas/Reservas/CancelarReserva">Cancelar Reserva</a>
            </div>
        </div>
      </div>
</asp:Content>
