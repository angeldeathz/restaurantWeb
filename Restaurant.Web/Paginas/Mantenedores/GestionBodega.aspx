﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="GestionBodega.aspx.cs" Inherits="Restaurant.Web.Paginas.Mantenedores.GestionInventario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <div class="row p-3">
      <div class="col-12 text-center">
        <h1 class="text-rosado">Gestión de Bodega</h1>
      </div>
      <div class="col-12">
        <div class="nav nav-tabs flex-row" id="tabs_gestion_inventario" role="tablist" aria-orientation="vertical">
          <a class="nav-link active" id="tabInsumos" data-toggle="pill" href="#divInsumos" role="tab" aria-controls="divInsumos" aria-selected="false" runat="server">Insumos</a>
          <a class="nav-link" id="tabProveedores" data-toggle="pill" href="#divProveedores" role="tab" aria-controls="divProveedores" aria-selected="false" runat="server">Proveedores</a>
          <a class="nav-link" id="tabOrdenes" data-toggle="pill" href="#divOrdenes" role="tab" aria-controls="divOrdenes" aria-selected="false" runat="server">Órdenes</a>
        </div>
      </div>
      <div class="col-12 bg-blanco rounded contenedor-mantenedores shadow">
        <div class="tab-content py-3 px-1" id="contenido_gestion_inventario">
          <div class="tab-pane fade" id="divInventario" role="tabpanel" aria-labelledby="tabInventario" runat="server" ClientIDMode="Static">
          </div>
          <div class="tab-pane show active" id="divInsumos" role="tabpanel" aria-labelledby="tabInsumos" runat="server" ClientIDMode="Static">
                <asp:Button ID="btnModalCrearInsumos" runat="server" Text="Crear Insumo" OnClick="btnModalCrearInsumo_Click" CssClass="btn btn-info float-right"/>
                <asp:UpdatePanel ID="upListaInsumos" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                   <ContentTemplate>
                      <div class="text-center">
                        <asp:Label ID="listaInsumosVacia" runat="server" 
                            Text="No existen Insumos para listar" CssClass="d-inline-block h5 my-5"></asp:Label>
                      </div>     
                      <div class="table-responsive pt-3">                
                        <asp:Repeater ID="listaInsumos" runat="server" OnItemCommand="btnModalEditarInsumo_Click">
                           <HeaderTemplate>
                             <table class="table styled-table">
                               <thead>
                                  <tr>
                                      <th><b>Id</b></th>
                                      <th><b>Nombre</b></th>
                                      <th><b>Unidad de medida</b></th>
                                      <th><b>Stock actual</b></th>
                                      <th><b>Stock mínimo</b></th>
                                      <th><b>Stock máximo</b></th>
                                      <th><b>Proveedor</b></th>
                                      <th><b>Acciones</b></th>
                                  </tr>
                                </thead>
                            </HeaderTemplate>          
                            <ItemTemplate>
                                <asp:HiddenField ID="idInsumo" runat="server"  Value='<%# Eval("Id") %>'/>
                                <tr>
                                <td> <%# Eval("Id") %></td>
                                <td> <%# Eval("Nombre") %> </td>
                                <td> <%# Eval("UnidadMedida.Nombre") %> </td>
                                <td> <%# Eval("StockActual") %> </td>
                                <td> <%# Eval("StockCritico") %> </td>
                                <td> <%# Eval("StockOptimo") %> </td>
                                <td> <%# Eval("Proveedor.Persona.Nombre") %>&nbsp; <%# Eval("Proveedor.Persona.Apellido") %></td>
                                <td><asp:LinkButton ID="btnModalEditarInsumo" CommandArgument='<%# Eval("Id") %>' runat="server" >
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

          <div class="tab-pane fade" id="divProveedores" role="tabpanel" aria-labelledby="tabProveedores" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearProveedor" runat="server" Text="Crear Proveedor" OnClick="btnModalCrearProveedor_Click" CssClass="btn btn-info float-right"/>
              <asp:UpdatePanel ID="upListaProveedores" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                   <ContentTemplate>
                      <div class="text-center">
                        <asp:Label ID="listaProveedoresVacia" runat="server" 
                            Text="No existen Proveedores para listar" CssClass="d-inline-block h5 my-5"></asp:Label>
                      </div>     
                      <div class="table-responsive pt-3">
                        <asp:Repeater ID="listaProveedores" runat="server"  OnItemCommand="btnModalEditarProveedor_Click">
                           <HeaderTemplate>
                             <table class="table styled-table">
                               <thead>
                                 <tr>
                                    <th><b>Rut</b></th>
                                    <th><b>Nombre</b></th>
                                    <th><b>Apellido</b></th>
                                    <th><b>Email</b></th>
                                    <th><b>Teléfono</b></th>
                                    <th><b>Dirección</b></th>
                                    <th><b>Es persona natural</b></th>
                                    <th><b>Acciones</b></th>
                                </tr>
                             </thead>
                            </HeaderTemplate>          
                            <ItemTemplate>
                              <tr>
                                <td class="text-center"> <%# Eval("Persona.Rut").ToString() != "0" ? Eval("Persona.Rut") + "-" + Eval("Persona.DigitoVerificador") : "-" %> </td>
                                <td> <%# Eval("Persona.Nombre") %> </td>
                                <td> <%# Eval("Persona.Apellido") %> </td>
                                <td> <%# Eval("Persona.Email") %> </td>
                                <td class="text-center"> <%# Eval("Persona.Telefono").ToString() != "0" ? Eval("Persona.Telefono") : "-" %> </td>
                                <td> <%# Eval("Direccion") %> </td>
                                <td> <%# Eval("Persona.EsPersonaNatural").ToString() != "0" ? "Sí" : "No" %> </td>
                                 <td><asp:LinkButton ID="btnModalEditarProveedor" CommandArgument='<%# Eval("Id") %>' runat="server" >
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
          <div class="tab-pane fade" id="divOrdenes" role="tabpanel" aria-labelledby="tabOrdenes" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearOrden" runat="server" Text="Crear Orden" OnClick="btnModalCrearOrden_Click" CssClass="btn btn-info float-right"/>
               <asp:UpdatePanel ID="upListaOrdenes" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                 <ContentTemplate>
                  <div class="text-center">
                    <asp:Label ID="listaOrdenesVacia" runat="server" 
                        Text="No existen Ordenes de proveedores para listar" CssClass="d-inline-block h5 my-5"></asp:Label>
                  </div>   
                   <div class="table-responsive pt-3">
                    <asp:Repeater ID="listaOrdenes" runat="server" OnItemCommand="btnModalEditarOrden_Click">
                      <HeaderTemplate>
                        <table class="table styled-table">
                          <thead>
                            <tr>
                                <th><b>Id</b></th>
                                <th><b>Fecha y Hora</b></th>
                                <th><b>Total</b></th>
                                <th><b>Estado</b></th>
                                <th><b>Proveedor</b></th>
                                <th><b>Acciones</b></th>
                            </tr>
                          </thead>
                        </HeaderTemplate>          
                        <ItemTemplate>
                            <asp:HiddenField ID="idOrden" runat="server"  Value='<%# Eval("Id") %>'/>
                            <tr>
                            <td> <%# Eval("Id") %></td>
                            <td> <%# Eval("FechaHora") %> </td>
                            <td> <%# Eval("Total", "${0:N0}") %> </td>
                            <td> <%# Eval("EstadoOrden.Nombre") %> </td>
                            <td> <%# Eval("Proveedor.Persona.Nombre") %>&nbsp; <%# Eval("Proveedor.Persona.Apellido") %></td>
                            <td><asp:LinkButton ID="btnModalEditarOrden" CommandArgument='<%# Eval("Id") %>' runat="server" >
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
                      <asp:RequiredFieldValidator ID="ValidacionNombreInsumo" runat="server" ControlToValidate="txtNombreInsumo" Display="Dynamic"
                      CssClass="text-danger" ErrorMessage="Debe seleccionar el nombre" ValidationGroup="ValidacionInsumo"></asp:RequiredFieldValidator>
                      <div class="row">
                        <div class="col-12 col-sm-4">
                          <asp:Label ID="lblStockActual" runat="server" Text="Stock actual"></asp:Label>
                          <asp:TextBox ID="txtStockActual" runat="server" CssClass="form-control" TextMode="Number" min="0"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="ValidacionStockActual" runat="server" ControlToValidate="txtStockActual" Display="Dynamic"
                          CssClass="text-danger" ErrorMessage="Debe ingresar el stock actual" ValidationGroup="ValidacionInsumo"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-4">
                          <asp:Label ID="lblStockCritico" runat="server" Text="Stock mínimo"></asp:Label>
                          <asp:TextBox ID="txtStockCritico" runat="server" CssClass="form-control" TextMode="Number" min="0"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="ValidacionStockCritico" runat="server" ControlToValidate="txtStockCritico" Display="Dynamic"
                          CssClass="text-danger" ErrorMessage="Ingrese el stock mínimo" ValidationGroup="ValidacionInsumo"></asp:RequiredFieldValidator>
                          <asp:CompareValidator ID="ValidacionStockCriticoOptimo" Operator="LessThan" type="Integer" runat="server" ValidationGroup="ValidacionInsumo" Display="Dynamic"
                          ControlToValidate="txtStockCritico" ControlToCompare="txtStockOptimo" ErrorMessage="Stock mínimo debe ser menor al máximo" CssClass="text-danger"></asp:CompareValidator>
                        </div>
                        <div class="col-12 col-sm-4">
                          <asp:Label ID="lblStockOptimo" runat="server" Text="Stock máximo"></asp:Label>
                          <asp:TextBox ID="txtStockOptimo" runat="server" CssClass="form-control" TextMode="Number" min="0"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="ValidacionStockOptimo" runat="server" ControlToValidate="txtStockOptimo" Display="Dynamic"
                          CssClass="text-danger" ErrorMessage="Ingrese el stock máximo" ValidationGroup="ValidacionInsumo"></asp:RequiredFieldValidator>
                          </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-sm-6">
                          <asp:Label ID="lblProveedorInsumo" runat="server" Text="Proveedor"></asp:Label>
                          <asp:DropDownList ID="ddlProveedorInsumo" runat="server" CssClass="form-control"></asp:DropDownList>
                          <asp:RequiredFieldValidator ID="ValidacionProveedorInsumo" runat="server" ControlToValidate="ddlProveedorInsumo" Display="Dynamic"
                          CssClass="text-danger" ErrorMessage="Debe seleccionar al proveedor" ValidationGroup="ValidacionInsumo"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-6">
                            <asp:Label ID="lblUnidadMedida" runat="server" Text="Unidad Medida"></asp:Label>
                            <asp:DropDownList ID="ddlUnidadMedida" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ValidacionUnidadMedida" runat="server" ControlToValidate="ddlUnidadMedida" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe seleccionar la unidad de medida" ValidationGroup="ValidacionInsumo"></asp:RequiredFieldValidator>
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
                            <asp:TextBox ID="txtRutProveedor" runat="server" CssClass="form-control w-75 d-inline-block" TextMode="Number" min="1"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="txtDigitoVerificadorProveedor" runat="server" CssClass="form-control w-15 d-inline-block" MaxLength="1"></asp:TextBox>         
                            <asp:RequiredFieldValidator ID="ValidacionRut" runat="server" ControlToValidate="txtRutProveedor" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar el RUT" ValidationGroup="ValidacionProveedor"></asp:RequiredFieldValidator><br />
                            <asp:RequiredFieldValidator ID="ValidacionDigitoVerificador" runat="server" ControlToValidate="txtDigitoVerificadorProveedor" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar el dígito verificador" ValidationGroup="ValidacionProveedor"></asp:RequiredFieldValidator>
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
                            <asp:RequiredFieldValidator ID="ValidacionNombreProveedor" runat="server" ControlToValidate="txtNombreProveedor" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar el nombre" ValidationGroup="ValidacionProveedor"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblApellidoProveedor" runat="server" Text="Apellido"></asp:Label>
                            <asp:TextBox ID="txtApellidoProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ValidacionApellidoProveedor" runat="server" ControlToValidate="txtApellidoProveedor" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar el apellido" ValidationGroup="ValidacionProveedor"></asp:RequiredFieldValidator>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblEmailProveedor" runat="server" Text="E-mail"></asp:Label>
                            <asp:TextBox ID="txtEmailProveedor" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ValidacionEmail" runat="server" ControlToValidate="txtEmailProveedor" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar el e-mail" ValidationGroup="ValidacionProveedor" ></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="ValidacionEmailValido" runat="server" CssClass="text-danger"
                            ErrorMessage="El e-mail ingresado es inválido" ControlToValidate="txtEmailProveedor" Display="Dynamic" ValidationGroup="ValidacionProveedor"
                            ValidationExpression="[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblTelefonoProveedor" runat="server" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefonoProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ValidacionTelefonoProveedor" runat="server" ControlToValidate="txtTelefonoProveedor" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar el telefono" ValidationGroup="ValidacionProveedor"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="ValidacionTelefonoFormato" runat="server" ControlToValidate="txtTelefonoProveedor"  CssClass="text-danger" 
                            ErrorMessage="El telefono debe tener 9 dígitos" ValidationExpression=".{9}.*" ValidationGroup="ValidacionProveedor" Display="Dynamic"/>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12">
                            <asp:Label ID="lblDireccionProveedor" runat="server" Text="Dirección"></asp:Label>
                            <asp:TextBox ID="txtDireccionProveedor" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="ValidacionDireccionProveedor" runat="server" ControlToValidate="txtDireccionProveedor" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar la dirección" ValidationGroup="ValidacionProveedor"></asp:RequiredFieldValidator>
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
        <!-- Modal Ordenes -->
    <div class="modal fade" id="modalOrden" tabindex="-1" role="dialog" aria-labelledby="tituloModalOrden" aria-hidden="true">
      <div class="modal-dialog modal-lg" role="document">
          <asp:UpdatePanel ID="upModalOrden" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalOrden" class="modal-title h5" runat="server" Text="Crear Orden"></asp:Label>
                    <button type="button" class="close" data-dismiss="modalValidacionInsumoOrden aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                    <asp:TextBox ID="txtIdOrden" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>                      
                    <div class="row">
                        <div class="col-12 col-sm-3">
                            <asp:Label ID="lblEstadoOrden" runat="server" Text="Estado"></asp:Label>
                            <asp:DropDownList ID="ddlEstadoOrden" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ValidacionEstadoOrden" runat="server" ControlToValidate="ddlEstadoOrden" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe seleccionar el estado de la orden" ValidationGroup="ValidacionOrden"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-sm-3">
                            <asp:Label ID="lblProveedorOrden" runat="server" Text="Proveedor"></asp:Label>
                            <asp:DropDownList ID="ddlProveedorOrden" runat="server" CssClass="form-control"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="ValidacionProveedorOrden" runat="server" ControlToValidate="ddlProveedorOrden" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe seleccionar al proveedor" ValidationGroup="ValidacionOrden"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-12 col-md-5">
                            <asp:Label ID="lblInsumoOrden" runat="server" Text="Insumo"></asp:Label>
                            <asp:DropDownList ID="ddlInsumoOrden" runat="server" CssClass="form-control"></asp:DropDownList>  
                            <asp:RequiredFieldValidator ID="ValidacionSelectInsumoOrden" runat="server" ControlToValidate="ddlInsumoOrden" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe seleccionar al insumi" ValidationGroup="ValidacionInsumoOrden"></asp:RequiredFieldValidator>
                        </div>                          
                        <div class="col-12 col-md-2">
                            <asp:Label ID="lblPrecioInsumoOrden" runat="server" Text="Precio"></asp:Label>
                            <asp:TextBox ID="txtPrecioInsumoOrden" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox> 
                            <asp:RequiredFieldValidator ID="ValidacionPrecioInsumoOrden" runat="server" ControlToValidate="txtPrecioInsumoOrden" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar al precio" ValidationGroup="ValidacionInsumoOrden"></asp:RequiredFieldValidator>
                        </div>
                        <div class="col-12 col-md-2">
                            <asp:Label ID="lblCantidadInsumoOrden" runat="server" Text="Cantidad"></asp:Label>
                            <asp:TextBox ID="txtCantidadInsumoOrden" runat="server" CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>          
                            <asp:RequiredFieldValidator ID="ValidacionCantidadInsumoOrden" runat="server" ControlToValidate="txtCantidadInsumoOrden" Display="Dynamic"
                            CssClass="text-danger" ErrorMessage="Debe ingresar la cantidad" ValidationGroup="ValidacionInsumoOrden"></asp:RequiredFieldValidator>
                        </div> 
                        <div class="col-12 col-md-3">
                            <br />
                            <asp:Button ID="btnAgregarInsumoOrden" runat="server" CssClass="btn btn-info btn-block" Text="Agregar" OnClick="btnAgregarInsumoOrden_Click"/>
                        </div>     
                    </div>   
                    <div class="row">
                        <div class="col-12">
                            <asp:UpdatePanel ID="upInsumosOrden" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="text-center">
                                        <asp:Label ID="listaInsumosOrdenVacia" runat="server" 
                                            Text="No han agregado artículos al Orden" CssClass="d-inline-block h5 my-5"></asp:Label>
                                    </div>
                                    <div class="table-responsive pt-3">
                                        <asp:Repeater ID="listaInsumosOrden" runat="server" OnItemCommand="btnEliminarInsumoOrden_Click">
                                        <HeaderTemplate>
                                            <table border="1" class="table">
                                            <tr>
                                                <td><b>Nombre</b></td>
                                                <td><b>Precio</b></td>
                                                <td><b>Cantidad</b></td>
                                                <td></td>
                                            </tr>
                                        </HeaderTemplate>          
                                        <ItemTemplate>
                                            <tr>
                                            <td> <%# Eval("Insumo.Nombre") %> </td>
                                            <td> <%# Eval("Precio", "${0:N0}") %> </td>
                                            <td> <%# Eval("Cantidad") %> </td>
                                                <td><asp:LinkButton ID="btnEliminarInsumoOrden" CommandArgument='<%# Eval("IdInsumo") %>' runat="server" >
                                                    Eliminar</asp:LinkButton></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="col-12 text-right">
                                        <asp:Label ID="lblTotalOrden" runat="server" Text=""></asp:Label>
                                        <asp:TextBox ID="txtTotalOrden" runat="server" CssClass="form-control" TextMode="Number" Visible="false"></asp:TextBox>              
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>  
                        </div> 
                    </div>   
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearOrden" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearOrden_Click"/>
                    <asp:Button ID="btnEditarOrden" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarOrden_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div> <!-- Fin Modal Ordenes-->
</asp:Content>
