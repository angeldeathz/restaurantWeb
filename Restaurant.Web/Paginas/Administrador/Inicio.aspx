<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Restaurant.Web.Paginas.Administrador.Inicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row py-3 my-5 bg-blanco rounded">
        <div class="col-12 col-md-6">
             <h1>Estado actual</h1>
             <p class="lead">Mesas ocupadas: 7</p>
             <p class="lead">Mesas disponibles: 5</p>
             <p class="lead">Cantidad de comensales: 20</p>
             <p class="lead">Garzones trabajando: 4</p>
        </div>
         <div class="col-12 col-md-6">
             <ul class="list-group">
              <li class="list-group-item active">Mantenedores</li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionReservas.aspx">Gestión de reservas</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionMesas.aspx">Gestión de mesas</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionProveedores.aspx">Gestión de proveedores</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionOrdenes.aspx">Gestión de órdenes proveedores</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionInsumos.aspx">Gestión de insumos</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionInventario.aspx">Gestión de inventario</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionProductos.aspx">Gestión de productos</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionUsuarios.aspx">Gestión de usuarios</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionClientes.aspx">Gestión de clientes</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/GestionPedidos.aspx">Gestión de pedidos</a></li>
              <li class="list-group-item"><a runat="server" href="/Paginas/Administrador/Reporteria.aspx">Informes y Reportería</a></li>
            </ul>
         </div>
    </div>
</asp:Content>
