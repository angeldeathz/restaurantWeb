<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Restaurant.Web.Paginas.Mantenedores.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row py-3 my-5 bg-blanco rounded">
        <div class="col-12 col-md-6">
            <h4 class="text-rosado">Bienvenid@&nbsp;<asp:Label runat="server" id="lblNombres" CssClass="h5"></asp:Label></h4>
            <span class="text-rosado">Perfil: <asp:Label runat="server" id="lblPerfil"></asp:Label></span>
            <br/><br/>
            <h3>Estado restaurante</h3>
            <p class="lead">Mesas ocupadas: 7</p>
            <p class="lead">Mesas disponibles: 5</p>
            <p class="lead">Cantidad de comensales: 20</p>
            <p class="lead">Garzones trabajando: 4</p>
        </div>
        <div class="col-12 col-md-6">
            <ul class="list-group">
                <li class="list-group-item active">Mantenedores</li>
                <li class="list-group-item" runat="server" id="linkBodega">
                    <a runat="server" href="/Paginas/Mantenedores/GestionBodega.aspx" class="font-weight-bold">Gestión de Bodega</a>
                    <ul>
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionBodega#divInsumos">Insumos</a></li>
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionBodega#divProveedores">Proveedores</a></li>
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionBodega#divOrdenes">Órdenes proveedores</a></li>
                    </ul>
                </li>
                <li class="list-group-item" runat="server" id="linkRestaurante">
                    <a runat="server" href="/Paginas/Mantenedores/GestionRestaurante.aspx" class="font-weight-bold">Gestión de Restaurante</a>
                    <ul>
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionRestaurante.aspx">Reservas</a></li>
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionRestaurante.aspx">Clientes</a></li>
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionRestaurante.aspx">Mesas</a></li>
                    </ul>
                </li>
                <li class="list-group-item" runat="server" id="linkCocina">
                    <a runat="server" href="/Paginas/Mantenedores/GestionCocina.aspx" class="font-weight-bold">Gestión de Cocina</a>
                    <ul>
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionCocina.aspx">Artículos</a></li>
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionCocina.aspx">Pedidos</a></li>
                    </ul>
                </li>
                <li class="list-group-item" runat="server" id="linkUsuarios"><a runat="server" href="/Paginas/Mantenedores/GestionUsuarios.aspx" class="font-weight-bold">Gestión de Usuarios</a></li>
                <li class="list-group-item" runat="server" id="linkReporteria"><a runat="server" href="/Paginas/Mantenedores/Reporteria.aspx" class="font-weight-bold">Informes y Reportería</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
