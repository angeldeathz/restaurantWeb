<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="GestionBodega.aspx.cs" Inherits="Restaurant.Web.Paginas.Administrador.GestionInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row p-3">
      <div class="col-12 text-center">
        <h1 class="text-rosado">Gestión de Bodega</h1>
      </div>
      <div class="col-12">
        <div class="nav nav-tabs" id="tabs_gestion_inventario" role="tablist" aria-orientation="vertical">
          <a class="nav-link active" id="tabInventario" data-toggle="pill" href="#divInventario" role="tab" aria-controls="divInventario" aria-selected="true" runat="server">Inventario</a>
          <a class="nav-link" id="tabInsumos" data-toggle="pill" href="#divInsumos" role="tab" aria-controls="divInsumos" aria-selected="false" runat="server">Insumos</a>
          <a class="nav-link" id="tabProveedores" data-toggle="pill" href="#divProveedores" role="tab" aria-controls="divProveedores" aria-selected="false" runat="server">Proveedores</a>
          <a class="nav-link" id="tabOrdenes" data-toggle="pill" href="#divOrdenes" role="tab" aria-controls="divOrdenes" aria-selected="false" runat="server">Órdenes</a>
        </div>
      </div>
      <div class="col-12 bg-blanco rounded contenedor-mantenedores">
        <div class="tab-content py-3 px-1" id="contenido_gestion_inventario">
          <div class="tab-pane show active" id="divInventario" role="tabpanel" aria-labelledby="tabInventario" runat="server" ClientIDMode="Static">
          </div>
          <div class="tab-pane fade" id="divInsumos" role="tabpanel" aria-labelledby="tabInsumos" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearInsumos" runat="server" Text="Crear Insumo" OnClick="btnModalCrearInsumo_Click" CssClass="btn btn-info float-right"/>
                <div class="table-responsive pt-3">
                    <asp:Repeater ID="listaInsumos" runat="server" OnItemCommand="btnModalEditarInsumo_Click">
                    <HeaderTemplate>
                        <table border="1" class="table">
                        <tr>
                            <td><b>Id</b></td>
                            <td><b>Nombre</b></td>
                            <td><b>Stock actual</b></td>
                            <td><b>Stock crítico</b></td>
                            <td><b>Stock óptimo</b></td>
                            <td><b>Acciones</b></td>
                        </tr>
                    </HeaderTemplate>          
                    <ItemTemplate>
                        <asp:HiddenField ID="idInsumo" runat="server"  Value='<%# Eval("Id") %>'/>
                        <tr>
                        <td> <%# Eval("Id") %></td>
                        <td> <%# Eval("Nombre") %> </td>
                        <td> <%# Eval("StockActual") %> </td>
                        <td> <%# Eval("StockCritico") %> </td>
                        <td> <%# Eval("StockOptimo") %> </td>
                        <td><asp:LinkButton ID="btnModalEditarInsumo" CommandArgument='<%# Eval("Id") %>' runat="server" >
                                Editar</asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                  </asp:Repeater>
                </div>
          </div>
          <div class="tab-pane fade" id="divProveedores" role="tabpanel" aria-labelledby="tabProveedores" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearProveedor" runat="server" Text="Crear Proveedor" OnClick="btnModalCrearProveedor_Click" CssClass="btn btn-info float-right"/>
                <div class="table-responsive pt-3">
                    <asp:Repeater ID="listaProveedores" runat="server"  OnItemCommand="btnModalEditarProveedor_Click">
                    <HeaderTemplate>
                        <table border="1" class="table">
                        <tr>
                            <td><b>Rut</b></td>
                            <td><b>Nombre</b></td>
                            <td><b>Apellido</b></td>
                            <td><b>Email</b></td>
                            <td><b>Teléfono</b></td>
                            <td><b>Dirección</b></td>
                            <td><b>Es persona natural</b></td>
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
                         <td><asp:LinkButton ID="btnModalEditarProveedor" CommandArgument='<%# Eval("Id") %>' runat="server" >
                                Editar</asp:LinkButton></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                  </asp:Repeater>
                </div>
          </div>
          <div class="tab-pane fade" id="divOrdenes" role="tabpanel" aria-labelledby="tabOrdenes" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearOrden" runat="server" Text="Crear Orden" OnClick="btnModalCrearOrden_Click" CssClass="btn btn-info float-right"/>
              <asp:Repeater ID="listaOrdenes" runat="server">

              </asp:Repeater>
          </div>
        </div>
      </div>
    </div>
    <!-- Modal Insumos -->
    <div class="modal fade" id="modalInsumo" tabindex="-1" role="dialog" aria-labelledby="tituloModalInsumo" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
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
                      <asp:TextBox ID="txtIdInsumo" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
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
    <!-- Fin Modal Insumos-- >
    <!-- Modal Proveedores -->
    <div class="modal fade" id="modalProveedor" tabindex="-1" role="dialog" aria-labelledby="tituloModalProveedor" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
          <asp:UpdatePanel ID="upModalProveedor" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalProveedor" class="modal-title h5" runat="server" Text="Crear Proveedor"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                      <asp:TextBox ID="txtIdProveedor" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblRutProveedor" runat="server" Text="Rut" CssClass="d-block"></asp:Label>
                            <asp:TextBox ID="txtRutProveedor" runat="server" CssClass="form-control w-75 d-inline-block" TextMode="Number"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="txtDigitoVerificadorProveedor" runat="server" CssClass="form-control w-15 d-inline-block" MaxLength="1"></asp:TextBox>                           
                        </div>                      
                        <div class="col-12 col-md-6">
                          <br />
                          <asp:CheckBox ID="chkEsPersonaJuridica" runat="server" />
                          <asp:Label ID="lblEsPersonaJuridica" runat="server" Text="Es persona jurídica"></asp:Label>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblNombreProveedor" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txtNombreProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblApellidoProveedor" runat="server" Text="Apellido"></asp:Label>
                            <asp:TextBox ID="txtApellidoProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblEmailProveedor" runat="server" Text="E-mail"></asp:Label>
                            <asp:TextBox ID="txtEmailProveedor" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblTelefonoProveedor" runat="server" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefonoProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12">
                            <asp:Label ID="lblDireccionProveedor" runat="server" Text="Dirección"></asp:Label>
                            <asp:TextBox ID="txtDireccionProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearProveedor" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearProveedor_Click"/>
                    <asp:Button ID="btnEditarProveedor" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarProveedor_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div>
    <!-- Fin modal proveedores -->
</asp:Content>
