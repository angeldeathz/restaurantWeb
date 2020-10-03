﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="GestionRestaurante.aspx.cs" Inherits="Restaurant.Web.Paginas.Administrador.GestiónBodega" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row p-3">
      <div class="col-12 text-center">
        <h1 class="text-rosado">Gestión del Restaurante</h1>
      </div>
      <div class="col-12">
        <div class="nav nav-tabs" id="tabsGestionRestaurante" role="tablist" aria-orientation="vertical">
          <a class="nav-link active" id="tabReservas" data-toggle="pill" href="#divReservas" role="tab" aria-controls="divReservas" aria-selected="false" runat="server">Reservas</a>
          <a class="nav-link" id="tabClientes" data-toggle="pill" href="#divClientes" role="tab" aria-controls="divClientes" aria-selected="false" runat="server">Clientes</a>
          <a class="nav-link" id="tabMesas" data-toggle="pill" href="#divMesas" role="tab" aria-controls="divMesas" aria-selected="false" runat="server">Mesas</a>
        </div>
      </div>
      <div class="col-12 bg-blanco rounded contenedor-mantenedores">
        <div class="tab-content py-3 px-1">
          <div class="tab-pane show active" id="divReservas" role="tabpanel" aria-labelledby="tabReservas" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearReservas" runat="server" Text="Crear Reserva" OnClick="btnModalCrearReserva_Click" CssClass="btn btn-info float-right"/>
                <div class="table-responsive pt-3">
                    <asp:Repeater ID="listaReservas" runat="server" OnItemCommand="btnModalEditarReserva_Click">
                    <HeaderTemplate>
                        <table border="1" class="table">
                        <tr>
                            <td><b>Id</b></td>
                            <td><b>Fecha y Hora</b></td>
                            <td><b>Mesa</b></td>
                            <td><b>Comensales</b></td>
                            <td><b>Cliente</b></td>
                            <td><b>Acciones</b></td>
                        </tr>
                    </HeaderTemplate>          
                    <ItemTemplate>
                        <asp:HiddenField ID="idReserva" runat="server"  Value='<%# Eval("Id") %>'/>
                        <tr>
                        <td> <%# Eval("Id") %></td>
                        <td> <%# Eval("FechaHora") %> </td>
                        <td> <%# Eval("Mesa.Nombre") %> </td>
                        <td> <%# Eval("CantidadComensales") %> </td>
                        <td> <%# Eval("Cliente.Nombre") %> <%# Eval("Cliente.Apellido") %></td>
                        <td><asp:LinkButton ID="btnModalEditarReserva" CommandArgument='<%# Eval("Id") %>' runat="server" >
                                Editar</asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                  </asp:Repeater>
                </div>
          </div>
          <div class="tab-pane fade" id="divClientes" role="tabpanel" aria-labelledby="tabClientes" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearCliente" runat="server" Text="Crear Cliente" OnClick="btnModalCrearCliente_Click" CssClass="btn btn-info float-right"/>
                <div class="table-responsive pt-3">
                    <asp:Repeater ID="listaClientes" runat="server"  OnItemCommand="btnModalEditarCliente_Click">
                    <HeaderTemplate>
                        <table border="1" class="table">
                        <tr>
                            <td><b>Rut</b></td>
                            <td><b>Nombre</b></td>
                            <td><b>Apellido</b></td>
                            <td><b>Email</b></td>
                            <td><b>Teléfono</b></td>
                            <td><b>Acciones</b></td>
                        </tr>
                    </HeaderTemplate>          
                    <ItemTemplate>
                        <tr>
                        <td> <%# Eval("Persona.Rut") %>-<%# Eval("Persona.DigitoVerificador") %> </td>
                        <td> <%# Eval("Persona.Nombre") %> </td>
                        <td> <%# Eval("Persona.Apellido") %> </td>
                        <td> <%# Eval("Persona.Email") %> </td>
                        <td> <%# Eval("Persona.Telefono") %> </td>
                        <td> <%# Eval("Direccion") %> </td>
                        <td> <%# Eval("Persona.EsPersonaNatural") %> </td>
                         <td><asp:LinkButton ID="btnModalEditarCliente" CommandArgument='<%# Eval("Id") %>' runat="server" >
                                Editar</asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                  </asp:Repeater>
                </div>
          </div>
          <div class="tab-pane fade" id="divMesas" role="tabpanel" aria-labelledby="tabMesas" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearMesa" runat="server" Text="Crear Mesa" OnClick="btnModalCrearMesa_Click" CssClass="btn btn-info float-right"/>
              <asp:Repeater ID="listaMesas" runat="server">

              </asp:Repeater>
          </div>
        </div>
      </div>
    </div>
    <!-- Modal Reservas -->
    <div class="modal fade" id="modalReserva" tabindex="-1" role="dialog" aria-labelledby="tituloModalReserva" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
          <asp:UpdatePanel ID="upModalReserva" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalReserva" class="modal-title h5" runat="server" Text="Crear Reserva"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                      <asp:TextBox ID="txtIdReserva" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>                      
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblFechaHoraReserva" runat="server" Text="Fecha"></asp:Label>
                            <asp:TextBox ID="txtFechaHoraReserva" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>                           
                        </div>                      
                        <div class="col-12 col-md-6">
                           <asp:Label ID="lblCantidadComensalesReserva" runat="server" Text="Comensales"></asp:Label>
                            <asp:TextBox ID="txtCantidadComensalesReserva" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>              
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-sm-6">
                            <asp:Label ID="lblClienteReserva" runat="server" Text="Cliente"></asp:Label>
                            <asp:DropDownList ID="ddlClienteReserva" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-12 col-sm-6">
                            <asp:Label ID="lblMesaReserva" runat="server" Text="Mesa"></asp:Label>
                            <asp:DropDownList ID="ddlMesaReserva" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-sm-6">
                            <asp:Label ID="lblEstadoReserva" runat="server" Text="Estado"></asp:Label>
                            <asp:DropDownList ID="ddlEstadoReserva" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                      </div>                      
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearReserva" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearReserva_Click"/>
                    <asp:Button ID="btnEditarReserva" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarReserva_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div> <!-- Fin Modal Reservas-- >
    <!-- Modal Clientes -->
    <div class="modal fade" id="modalCliente" tabindex="-1" role="dialog" aria-labelledby="tituloModalCliente" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
          <asp:UpdatePanel ID="upModalCliente" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalCliente" class="modal-title h5" runat="server" Text="Crear Cliente"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                      <asp:TextBox ID="txtIdCliente" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblRutCliente" runat="server" Text="Rut" CssClass="d-block"></asp:Label>
                            <asp:TextBox ID="txtRutCliente" runat="server" CssClass="form-control w-75 d-inline-block" TextMode="Number"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="txtDigitoVerificadorCliente" runat="server" CssClass="form-control w-15 d-inline-block" MaxLength="1"></asp:TextBox>                           
                        </div>
                        <div class="col-12 col-md-6">
                          <br />
                          <asp:CheckBox ID="chkEsPersonaJuridica" runat="server" />
                          <asp:Label ID="lblEsPersonaJuridica" runat="server" Text="Es persona jurídica"></asp:Label>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblNombreCliente" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txtNombreCliente" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblApellidoCliente" runat="server" Text="Apellido"></asp:Label>
                            <asp:TextBox ID="txtApellidoCliente" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblEmailCliente" runat="server" Text="E-mail"></asp:Label>
                            <asp:TextBox ID="txtEmailCliente" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblTelefonoCliente" runat="server" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefonoCliente" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearCliente" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearCliente_Click"/>
                    <asp:Button ID="btnEditarCliente" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarCliente_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div><!-- Fin modal Clientes -->
    <!-- Modal Mesas -->
    <div class="modal fade" id="modalMesa" tabindex="-1" role="dialog" aria-labelledby="tituloModalMesa" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
          <asp:UpdatePanel ID="upModalMesa" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalMesa" class="modal-title h5" runat="server" Text="Crear Mesa"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                      <asp:TextBox ID="txtIdMesa" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>                      
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblNombreMesa" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txtNombreMesa" runat="server" CssClass="form-control"></asp:TextBox>                           
                        </div>                      
                        <div class="col-12 col-md-6">
                           <asp:Label ID="lblCantidadComensalesMesa" runat="server" Text="Comensales"></asp:Label>
                            <asp:TextBox ID="txtCantidadComensalesMesa" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>              
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-sm-6">
                            <asp:Label ID="lblEstadoMesa" runat="server" Text="Estado"></asp:Label>
                            <asp:DropDownList ID="ddlEstadoMesa" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                      </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearMesa" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearMesa_Click"/>
                    <asp:Button ID="btnEditarMesa" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarMesa_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div> <!-- Fin Modal Mesas-->
</asp:Content>
