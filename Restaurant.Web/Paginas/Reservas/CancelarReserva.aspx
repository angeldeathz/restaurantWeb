<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="CancelarReserva.aspx.cs" Inherits="Restaurant.Web.Paginas.Reservas.CancelarReserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="mt-4 col-12 col-md-5 mx-auto text-center">
            <h2>Cancelar Reserva</h2>
            <div class="form-group">
              <asp:Label ID="lblNumeroReserva" runat="server" Text="Número de la reserva"></asp:Label>
              <asp:TextBox ID="txtNumeroReserva" runat="server" TextMode="Number" CssClass="form-control" min="1"></asp:TextBox>
               <asp:RequiredFieldValidator ID="ValidacionNumeroReserva" runat="server" ControlToValidate="txtNumeroReserva" Display="Dynamic"
                CssClass="text-danger" ErrorMessage="Debe ingresar el número de su reserva" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
            </div>
            <div class="form-group">
              <asp:Label ID="lblEmail" runat="server" Text="E-mail de Ingreso"></asp:Label>
              <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
              <asp:RequiredFieldValidator ID="ValidacionEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                CssClass="text-danger" ErrorMessage="Debe ingresar su e-mail" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
                <asp:RegularExpressionValidator ID="ValidacionEmailValido" runat="server" CssClass="text-danger"
                ErrorMessage="El e-mail ingresado es inválido"  ControlToValidate="txtEmail" Display="Dynamic"
                ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"></asp:RegularExpressionValidator>
            </div>
            <div class="my-3">
                <a class="btn btn-secondary" href="/Paginas/Publica/Reservas">Volver</a>
                <asp:Button ID="btnCancelarReserva" runat="server" Text="Cancelar reserva" CssClass="btn btn-info" OnClick="btnCancelarReserva_Click"/>
            </div>          
        </div>
    </div>
</asp:Content>
