﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="CrearReserva.aspx.cs" Inherits="Restaurant.Web.Paginas.Reservas.CrearReserva" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12 col-md-10 col-lg-7 mx-auto my-5 text-center bg-light rounded px-5 py-4">
            <h2>Crear Reserva</h2>
            <div class="row">
                 <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblNombre" runat="server" Text="Nombre"></asp:Label>
                    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidacionNombre" runat="server" ControlToValidate="txtNombre" Display="Dynamic"
                    CssClass="text-danger" ErrorMessage="Debe ingresar su nombre" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
                </div> 
                <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblApellido" runat="server" Text="Apellido"></asp:Label>
                    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control"></asp:TextBox>
                   <asp:RequiredFieldValidator ID="ValidacionApellido" runat="server" ControlToValidate="txtApellido" Display="Dynamic"
                    CssClass="text-danger" ErrorMessage="Debe ingresar su apellido" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
                </div> 
            </div>
            <div class="row">
                 <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblEmailIngreso" runat="server" Text="E-mail"></asp:Label>
                    <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidacionEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                    CssClass="text-danger" ErrorMessage="Debe ingresar su e-mail" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="ValidacionEmailValido" runat="server" CssClass="text-danger"
                    ErrorMessage="El e-mail ingresado es inválido" ControlToValidate="txtEmail" Display="Dynamic" ValidationGroup="Validacion"
                    ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"></asp:RegularExpressionValidator>
                </div> 
                <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblComensales" runat="server" Text="Cantidad de Comensales"></asp:Label>
                    <asp:TextBox ID="txtComensales" runat="server" TextMode="Number" CssClass="form-control" min="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidacionComensales" runat="server" ControlToValidate="txtComensales" Display="Dynamic"
                    CssClass="text-danger" ErrorMessage="Debe ingresar el número de comensales" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
                </div> 
            </div>
            <div class="row">
                 <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblFecha" runat="server" Text="Fecha"></asp:Label>
                    <asp:TextBox ID="txtFecha" runat="server" TextMode="Date" CssClass="form-control" OnTextChanged="txtFecha_TextChanged" AutoPostBack="true"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidacionFecha" runat="server" ControlToValidate="txtFecha" Display="Dynamic"
                    CssClass="text-danger" ErrorMessage="Debe seleccionar una fecha" ValidationGroup="Validacion"></asp:RequiredFieldValidator>

                </div> 
                <div class="col-12 col-md-6 form-group my-1 text-left">
                    <asp:Label ID="lblHora" runat="server" Text="Hora"></asp:Label>
                    <div class="row">
                      <div class="col-5 pr-1">
                        <asp:DropDownList ID="ddlHora" runat="server" CssClass="form-control"></asp:DropDownList>
                      </div>
                      <div class="col-1 p-0 text-center">:</div>
                      <div class="col-5 pl-1">
                        <asp:DropDownList ID="ddlMinuto" runat="server" CssClass="form-control"></asp:DropDownList>
                      </div>
                      <div class="col-1 p-0"><p class="mb-0 mt-2">Hrs</p></div>
                    </div>
                </div> 
            </div>

            <div class="my-3">
                <a class="btn btn-secondary" href="/Paginas/Publica/Reservas">Volver</a>
                <asp:Button ID="btnCrearReserva" runat="server" Text="Crear reserva" CssClass="btn btn-info" OnClick="btnCrearReserva_Click"/>
            </div>          
        </div>
    </div>
</asp:Content>
