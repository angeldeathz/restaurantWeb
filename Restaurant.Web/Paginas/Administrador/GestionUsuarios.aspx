﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="GestionUsuarios.aspx.cs" Inherits="Restaurant.Web.Paginas.Administrador.GestionUsuarios" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row p-3">
      <div class="col-12 text-center">
        <h1 class="text-rosado">Gestión de Usuarios</h1>
      </div>
      <div class="col-12">
        <div class="nav nav-tabs flex-column flex-md-row" id="tabsGestionUsuario" role="tablist" aria-orientation="vertical">
          <a class="nav-link active" id="tabUsuarios" data-toggle="pill" href="#divUsuarios" role="tab" aria-controls="divUsuarios" aria-selected="false" runat="server">Usuarios</a>
        </div>
      </div>
      <div class="col-12 bg-blanco rounded contenedor-mantenedores">
        <div class="tab-content py-3 px-1">
            <div class="tab-pane show active" id="divUsuarios" role="tabpanel" aria-labelledby="tabUsuarios" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearUsuario" runat="server" Text="Crear Usuario" OnClick="btnModalCrearUsuario_Click" CssClass="btn btn-info float-right"/>
              <asp:UpdatePanel ID="upListaUsuarios" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                 <ContentTemplate> 
                    <div class="text-center">
                        <asp:Label ID="listaUsuariosVacia" runat="server" 
                            Text="No existen Usuarios para listar" CssClass="d-inline-block h5 my-5"></asp:Label>
                    </div>  
                    <div class="table-responsive pt-3">
                        <asp:Repeater ID="listaUsuarios" runat="server"  OnItemCommand="btnModalEditarUsuario_Click">
                        <HeaderTemplate>
                            <table border="1" class="table">
                            <tr>
                                <td><b>Rut</b></td>
                                <td><b>Nombre</b></td>
                                <td><b>Apellido</b></td>
                                <td><b>Email</b></td>
                                <td><b>Teléfono</b></td>
                                <td><b>Tipo de usuario</b></td>
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
                            <td> <%# Eval("TipoUsuario.Nombre") %> </td>
                             <td><asp:LinkButton ID="btnModalEditarUsuario" CommandArgument='<%# Eval("Id") %>' runat="server" >
                                    Editar</asp:LinkButton></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                      </asp:Repeater>
                    </div>
                   </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
      </div>
    </div>
        <!-- Modal Usuarios -->
    <div class="modal fade" id="modalUsuario" tabindex="-1" role="dialog" aria-labelledby="tituloModalUsuario" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
          <asp:UpdatePanel ID="upModalUsuario" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalUsuario" class="modal-title h5" runat="server" Text="Crear Usuario"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                      <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblRutUsuario" runat="server" Text="Rut" CssClass="d-block"></asp:Label>
                            <asp:TextBox ID="txtRutUsuario" runat="server" CssClass="form-control w-75 d-inline-block" TextMode="Number"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="txtDigitoVerificadorUsuario" runat="server" CssClass="form-control w-15 d-inline-block" MaxLength="1"></asp:TextBox>                           
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de Usuario"></asp:Label>
                            <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control"></asp:DropDownList>
                          </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblApellidoUsuario" runat="server" Text="Apellido"></asp:Label>
                            <asp:TextBox ID="txtApellidoUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblEmailUsuario" runat="server" Text="E-mail"></asp:Label>
                            <asp:TextBox ID="txtEmailUsuario" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblTelefonoUsuario" runat="server" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefonoUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblContrasena" runat="server" Text="Contraseña"></asp:Label>
                            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblContrasenaRepetir" runat="server" Text="Repetir Contraseña"></asp:Label>
                            <asp:TextBox ID="txtContrasenaRepetir" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                      </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearUsuario" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearUsuario_Click"/>
                    <asp:Button ID="btnEditarUsuario" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarUsuario_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div><!-- Fin modal Usuarios -->
</asp:Content>
