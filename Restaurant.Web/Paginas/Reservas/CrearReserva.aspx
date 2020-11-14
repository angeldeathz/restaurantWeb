<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="CrearReserva.aspx.cs" Inherits="Restaurant.Web.Paginas.Reservas.CrearReserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12 col-md-10 col-lg-7 mx-auto my-5 text-center bg-light rounded px-5 py-4">
            <h2>Crear Reserva</h2>
            <div class="row">
                 <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                </div> 
                <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                </div> 
            </div>
            <div class="row">
                 <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblEmailIngreso" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
                </div> 
                <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblComensales" runat="server" Text="Cantidad de Comensales"></asp:Label>
                    <asp:TextBox ID="txtComensales" runat="server" TextMode="Number" CssClass="form-control" min="1"></asp:TextBox>
                </div> 
            </div>
            <div class="row">
                 <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                    <!--<asp:TextBox ID="txtFechaF" runat="server" TextMode="Date" CssClass="form-control" OnTextChanged="txtFecha_TextChanged"></asp:TextBox>-->
                    <asp:TextBox ID="txtFecha" runat="server" TextMode="DateTimeLocal" CssClass="form-control" min="2020-11-07T10:00"></asp:TextBox>

                </div> 
                <div class="col-12 col-md-6 form-group my-1 text-left">
                   <!-- <asp:Label ID="lblHora" runat="server" Text="Hora"></asp:Label>
                    <asp:DropDownList ID="ddlHora" runat="server" CssClass="form-control"></asp:DropDownList>-->
                </div> 
            </div>

            <div class="my-3">
                <a class="btn btn-secondary" href="/Paginas/Publica/Reservas">Volver</a>
                <asp:Button ID="btnCrearReserva" runat="server" Text="Crear reserva" CssClass="btn btn-info" OnClick="btnCrearReserva_Click"/>
            </div>          
        </div>
    </div>
</asp:Content>
