<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="CancelarReserva.aspx.cs" Inherits="Restaurant.Web.Paginas.Reservas.CancelarReserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12 col-md-5 m-auto text-center">
            <h2>Cancelar Reserva</h2>
            <div class="form-group">
                 <asp:Label ID="lblNumeroReserva" runat="server" Text="Número de la reserva"></asp:Label>
                 <asp:TextBox ID="txtNumeroReserva" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                 <asp:Label ID="lblEmail" runat="server" Text="E-mail de Ingreso"></asp:Label>
                 <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="my-3">
                <a class="btn btn-secondary" href="/Paginas/Publica/Reservas"><- Volver</a>
                <asp:Button ID="btnCancelarReserva" runat="server" Text="Cancelar" CssClass="btn btn-info" OnClick="btnCancelarReserva_Click"/>
            </div>          
        </div>
    </div>
</asp:Content>
