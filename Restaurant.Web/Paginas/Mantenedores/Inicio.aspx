<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Restaurant.Web.Paginas.Mantenedores.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row py-3 my-5 bg-blanco rounded">
        <div class="col-12 col-md-6">
            <h4 class="text-rosado">Bienvenid@&nbsp;<asp:Label runat="server" id="lblNombres" CssClass="h5"></asp:Label></h4>
            <span class="text-rosado">Perfil: <asp:Label runat="server" id="lblPerfil"></asp:Label></span>
            <br/><br/>
            <h3>Estado restaurante</h3>
            <p class="lead"><i class="fa fa-check-circle"></i>  Mesas ocupadas: <asp:Label runat="server" ID="lblMesasOcupadas"></asp:Label></p>
            <p class="lead"><i class="fa fa-times-circle"></i>  Mesas disponibles: <asp:Label runat="server" ID="lblMesasDisponibles"></asp:Label></p>
            <p class="lead"><i class="fa fa-users"></i>  Cantidad de comensales: <asp:Label runat="server" ID="lblComensales"></asp:Label></p>
            <p class="lead"><i class="fa fa-clock-o"></i>  Próxima reserva: <asp:Label runat="server" ID="lblProximaReserva" CssClass="small"></asp:Label></p>
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
                        <li><a runat="server" href="/Paginas/Mantenedores/GestionRestaurante.aspx">Horario de Reservas</a></li>
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
                <li class="list-group-item" runat="server" id="linkReporteria"><a runat="server" href="/Paginas/Mantenedores/Reporteria.aspx" class="font-weight-bold">Reportería</a></li>
            </ul>
        </div>
    </div>
</asp:Content>
