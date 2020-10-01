﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="GestionInventario.aspx.cs" Inherits="Restaurant.Web.Paginas.Administrador.GestionInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row my-5 p-3 bg-blanco rounded">
      <div class="col-12 text-center">
        <h1 class="text-rosado">Gestión de Inventario</h1>
      </div>
      <div class="col-12 col-md-3">
        <div class="nav flex-column nav-pills" id="tabs_gestion_inventario" role="tablist" aria-orientation="vertical">
          <a class="nav-link active" id="tabInventario" data-toggle="pill" href="#divInventario" role="tab" aria-controls="divInventario" aria-selected="true" runat="server">Inventario</a>
          <a class="nav-link" id="tabInsumos" data-toggle="pill" href="#divInsumos" role="tab" aria-controls="divInsumos" aria-selected="false" runat="server">Insumos</a>
          <a class="nav-link" id="tabProveedores" data-toggle="pill" href="#divProveedores" role="tab" aria-controls="divProveedores" aria-selected="false" runat="server">Proveedores</a>
          <a class="nav-link" id="tabOrdenes" data-toggle="pill" href="#divOrdenes" role="tab" aria-controls="divOrdenes" aria-selected="false" runat="server">Órdenes</a>
        </div>
      </div>
      <div class="col-12 col-md-9">
        <div class="tab-content" id="contenido_gestion_inventario">
          <div class="tab-pane fade show active" id="divInventario" role="tabpanel" aria-labelledby="tabInventario" runat="server" ClientIDMode="Static">
          </div>
          <div class="tab-pane fade" id="divInsumos" role="tabpanel" aria-labelledby="tabInsumos" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearInsumos" runat="server" Text="Crear Insumo" OnClick="btnModalCrearInsumos_Click" CssClass="btn btn-info float-right"/>
              <asp:Repeater ID="listaInsumos" runat="server">

              </asp:Repeater>
          </div>
          <div class="tab-pane fade" id="divProveedores" role="tabpanel" aria-labelledby="tabProveedores" runat="server" ClientIDMode="Static">
              <asp:Repeater ID="listaProveedores" runat="server">

              </asp:Repeater>
          </div>
          <div class="tab-pane fade" id="divOrdenes" role="tabpanel" aria-labelledby="tabOrdenes" runat="server" ClientIDMode="Static">
              <asp:Repeater ID="listaOrdenes" runat="server">

              </asp:Repeater>
          </div>
        </div>
      </div>
    </div>

    <div class="modal fade" id="modalInsumo" tabindex="-1" role="dialog" aria-labelledby="tituloModalInsumo" aria-hidden="true">
      <div class="modal-dialog" role="document">
          <asp:UpdatePanel ID="upModalInsumo" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalInsumo" class="modal-title h5" runat="server" Text="Crear Insumo"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">
                      <asp:Label ID="lblNombreInsumo" runat="server" Text="Nombre"></asp:Label>
                      <asp:TextBox ID="txtNombreInsumo" runat="server" CssClass="form-control"></asp:TextBox>
                      <div class="row">
                        <div class="col-12 col-sm-4">
                          <asp:Label ID="lblStockActual" runat="server" Text="Stock actual"></asp:Label>
                          <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-4">
                            <asp:Label ID="lblStockCritico" runat="server" Text="Stock crítico"></asp:Label>
                            <asp:TextBox ID="txtStockCritico" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                        <div class="col-12 col-sm-4">
                            <asp:Label ID="lblStockOptimo" runat="server" Text="Stock óptimo"></asp:Label>
                            <asp:TextBox ID="txtStockOptimo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-sm-6">
                            <asp:Label ID="lblProveedorInsumo" runat="server" Text="Proveedor"></asp:Label>
                            <asp:DropDownList ID="ddlProveedorInsumo" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-12 col-sm-6">
                            <asp:Label ID="lblUnidadMedida" runat="server" Text="Unidad Medida"></asp:Label>
                            <asp:DropDownList ID="ddlUnidadMedida" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                      </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearInsumo" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearInsumo_Click"/>
                    <asp:Button ID="btnEditarInsumo" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarInsumo_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
</asp:Content>
