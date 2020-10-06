<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="GestionCocina.aspx.cs" Inherits="Restaurant.Web.Paginas.Administrador.GestionCocina" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row p-3">
      <div class="col-12 text-center">
        <h1 class="text-rosado">Gestión de la Cocina</h1>
      </div>
      <div class="col-12">
        <div class="nav nav-tabs" id="tabsGestionRestaurante" role="tablist" aria-orientation="vertical">
          <a class="nav-link active" id="tabPedidos" data-toggle="pill" href="#divPedidos" role="tab" aria-controls="divPedidos" aria-selected="false" runat="server">Pedidos</a>
          <a class="nav-link" id="tabArticulos" data-toggle="pill" href="#divArticulos" role="tab" aria-controls="divArticulos" aria-selected="false" runat="server">Artículos</a>
        </div>
      </div>
      <div class="col-12 bg-blanco rounded contenedor-mantenedores">
        <div class="tab-content py-3 px-1">
          <div class="tab-pane show active" id="divPedidos" role="tabpanel" aria-labelledby="tabPedidos" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearPedidos" runat="server" Text="Crear Pedido" OnClick="btnModalCrearPedido_Click" CssClass="btn btn-info float-right"/>
               <asp:UpdatePanel ID="upListaPedidos" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                 <ContentTemplate>
                  <div class="text-center">
                    <asp:Label ID="listaPedidosVacia" runat="server" 
                        Text="No existen Pedidos para listar" CssClass="d-inline-block h5 my-5"></asp:Label>
                  </div>  
                  <div class="table-responsive pt-3">
                     <asp:Repeater ID="listaPedidos" runat="server" OnItemCommand="btnModalEditarPedido_Click">
                        <HeaderTemplate>
                            <table border="1" class="table">
                            <tr>
                                <td><b>Id</b></td>
                                <td><b>Fecha Inicio</b></td>
                                <td><b>Fecha Fin</b></td>
                                <td><b>Total</b></td>
                                <td><b>Mesa</b></td>
                                <td><b>Estado</b></td>
                                <td><b>Acciones</b></td>
                            </tr>
                        </HeaderTemplate>          
                        <ItemTemplate>
                            <asp:HiddenField ID="idPedido" runat="server"  Value='<%# Eval("Id") %>'/>
                            <tr>
                            <td> <%# Eval("Id") %></td>
                            <td> <%# Eval("FechaHoraInicio") %> </td>
                            <td> <%# Eval("FechaHoraFin") %> </td>
                            <td> <%# Eval("Total") %> </td>
                            <td> <%# Eval("Mesa.Nombre") %> </td>
                            <td> <%# Eval("EstadoPedido.Nombre") %></td>
                            <td><asp:LinkButton ID="btnModalEditarPedido" CommandArgument='<%# Eval("Id") %>' runat="server" >
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
          <div class="tab-pane fade" id="divArticulos" role="tabpanel" aria-labelledby="tabArticulos" runat="server" ClientIDMode="Static">
              <asp:Button ID="btnModalCrearArticulo" runat="server" Text="Crear Articulo" OnClick="btnModalCrearArticulo_Click" CssClass="btn btn-info float-right"/>
               <asp:UpdatePanel ID="upListaArticulos" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                 <ContentTemplate>
                      <div class="text-center">
                        <asp:Label ID="listaArticulosVacia" runat="server" 
                            Text="No existen Artículos para listar" CssClass="d-inline-block h5 my-5"></asp:Label>
                      </div>   
                      <div class="table-responsive pt-3">
                        <asp:Repeater ID="listaArticulos" runat="server"  OnItemCommand="btnModalEditarArticulo_Click">
                        <HeaderTemplate>
                            <table border="1" class="table">
                            <tr>
                                <td><b>Nombre</b></td>
                                <td><b>Descripción</b></td>
                                <td><b>Precio</b></td>
                                <td><b>Tipo de Consumo</b></td>
                                <td><b>Estado</b></td>
                                <td><b>Acciones</b></td>
                            </tr>
                        </HeaderTemplate>          
                        <ItemTemplate>
                            <tr>
                            <td> <%# Eval("Nombre") %> </td>
                            <td> <%# Eval("Descripcion") %> </td>
                            <td> <%# Eval("Precio") %> </td>
                            <td> <%# Eval("TipoConsumo.Nombre") %> </td>
                            <td> <%# Eval("EstadoArticulo.Nombre") %> </td>
                            <td><asp:LinkButton ID="btnModalEditarArticulo" CommandArgument='<%# Eval("Id") %>' runat="server" >
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
    <!-- Modal Pedidos -->
    <div class="modal fade" id="modalPedido" tabindex="-1" role="dialog" aria-labelledby="tituloModalPedido" aria-hidden="true">
      <div class="modal-dialog modal-lg" role="document">
          <asp:UpdatePanel ID="upModalPedido" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalPedido" class="modal-title h5" runat="server" Text="Crear Pedido"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                    <asp:TextBox ID="txtIdPedido" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>                      
                    <div class="row">
                        <div class="col-12 col-md-3">
                            <asp:Label ID="lblFechaInicioPedido" runat="server" Text="Fecha Inicio"></asp:Label>
                            <asp:TextBox ID="txtFechaInicioPedido" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>                           
                        </div>  
                        <div class="col-12 col-md-3">
                            <asp:Label ID="lblFechaFinPedido" runat="server" Text="Fecha Fin"></asp:Label>
                            <asp:TextBox ID="txtFechaFinPedido" runat="server" CssClass="form-control" TextMode="DateTimeLocal"></asp:TextBox>                           
                        </div> 
                            <div class="col-12 col-sm-3">
                            <asp:Label ID="lblEstadoPedido" runat="server" Text="Estado"></asp:Label>
                            <asp:DropDownList ID="ddlEstadoPedido" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="col-12 col-sm-3">
                            <asp:Label ID="lblMesaPedido" runat="server" Text="Mesa"></asp:Label>
                            <asp:DropDownList ID="ddlMesaPedido" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-12 col-md-5">
                            <asp:Label ID="lblArticuloPedido" runat="server" Text="Artículo"></asp:Label>
                            <asp:DropDownList ID="ddlArticuloPedido" runat="server" CssClass="form-control" 
                                OnSelectedIndexChanged="ddlArticuloPedido_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>     
                        </div>                          
                        <div class="col-12 col-md-2">
                            <asp:UpdatePanel ID="upPrecio" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Label ID="lblPrecioArticuloPedido" runat="server" Text="Precio"></asp:Label>
                                    <asp:DropDownList ID="ddlPrecioArticuloPedido" runat="server" CssClass="form-control" Enabled="false"></asp:DropDownList>              
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-12 col-md-2">
                            <asp:Label ID="lblCantidadArticuloPedido" runat="server" Text="Cantidad"></asp:Label>
                            <asp:TextBox ID="txtCantidadArticuloPedido" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>                           
                        </div> 
                        <div class="col-12 col-md-3">
                            <br />
                            <asp:Button ID="btnAgregarArticuloPedido" runat="server" CssClass="btn btn-info btn-block" Text="Agregar" OnClick="btnAgregarArticuloPedido_Click"/>
                        </div>     
                    </div>   
                    <div class="row">
                        <div class="col-12">
                            <asp:UpdatePanel ID="upArticulosPedido" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="text-center">
                                        <asp:Label ID="listaArticulosPedidoVacia" runat="server" 
                                            Text="No han agregado artículos al pedido" CssClass="d-inline-block h5 my-5"></asp:Label>
                                    </div>
                                    <div class="table-responsive pt-3">
                                        <asp:Repeater ID="listaArticulosPedido" runat="server" OnItemCommand="btnEliminarArticuloPedido_Click">
                                        <HeaderTemplate>
                                            <table border="1" class="table">
                                            <tr>
                                                <td><b>Nombre</b></td>
                                                <td><b>Precio</b></td>
                                                <td><b>Cantidad</b></td>
                                                <td><b>Total</b></td>
                                                <td></td>
                                            </tr>
                                        </HeaderTemplate>          
                                        <ItemTemplate>
                                            <tr>
                                            <td> <%# Eval("Articulo.Nombre") %> </td>
                                            <td> $<%# Eval("Precio") %> </td>
                                            <td> <%# Eval("Cantidad") %> </td>
                                            <td> $<%# Eval("Total") %> </td>
                                                <td><asp:LinkButton ID="btnEliminarArticuloPedido" CommandArgument='<%# Eval("IdArticulo") %>' runat="server" >
                                                    Eliminar</asp:LinkButton></td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                    <div class="col-12 text-right">
                                        <asp:Label ID="lblTotalPedido" runat="server" Text=""></asp:Label>
                                        <asp:TextBox ID="txtTotalPedido" runat="server" CssClass="form-control" TextMode="Number" Visible="false"></asp:TextBox>              
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>  
                        </div> 
                    </div>   
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearPedido" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearPedido_Click"/>
                    <asp:Button ID="btnEditarPedido" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarPedido_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div> <!-- Fin Modal Pedidos-->
    <!-- Modal Articulos -->
    <div class="modal fade" id="modalArticulo" tabindex="-1" role="dialog" aria-labelledby="tituloModalArticulo" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
          <asp:UpdatePanel ID="upModalArticulo" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalArticulo" class="modal-title h5" runat="server" Text="Crear Articulo"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                      <asp:TextBox ID="txtIdArticulo" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblNombreArticulo" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txtNombreArticulo" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblPrecioArticulo" runat="server" Text="Precio"></asp:Label>
                            <asp:TextBox ID="txtPrecioArticulo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12">
                            <asp:Label ID="lblDescripcionArticulo" runat="server" Text="Descripción"></asp:Label>
                            <asp:TextBox ID="txtDescripcionArticulo" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblTipoConsumoArticulo" runat="server" Text="Tipo de Consumo"></asp:Label>
                            <asp:DropDownList ID="ddlTipoConsumoArticulo" runat="server" CssClass="form-control"></asp:DropDownList>     
                        </div>  
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblEstadoArticulo" runat="server" Text="Estado"></asp:Label>
                             <asp:DropDownList ID="ddlEstadoArticulo" runat="server" CssClass="form-control"></asp:DropDownList>     
                        </div> 
                        </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnCrearArticulo" runat="server" CssClass="btn btn-info" Text="Crear" OnClick="btnCrearArticulo_Click"/>
                    <asp:Button ID="btnEditarArticulo" runat="server" visible="false" CssClass="btn btn-info" Text="Editar" OnClick="btnEditarArticulo_Click" />
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div><!-- Fin modal Articulos -->
</asp:Content>
