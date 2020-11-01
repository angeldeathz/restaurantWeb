<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="GestionAutoservicio.aspx.cs" Inherits="Restaurant.Web.Paginas.Autoservicio.GestionAutoservicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row p-3">
      <div class="col-12 text-center">
        <h1 class="text-rosado">Autoservicio</h1>
      </div>
      <div class="col-12">
        <div class="nav nav-tabs flex-column flex-md-row" id="tabsAutoservicio" role="tablist" aria-orientation="vertical">
          <a class="nav-link active" id="tabMenu" data-toggle="pill" href="#divMenu" role="tab" aria-controls="divMenu" aria-selected="false" runat="server">Menú</a>
          <a class="nav-link" id="tabMiOrden" data-toggle="pill" href="#divMiOrden" role="tab" aria-controls="divMiOrden" aria-selected="false" runat="server">Mi Orden</a>
        </div>
      </div>
      <div class="col-12 bg-blanco rounded contenedor-mantenedores">
        <div class="tab-content py-3 px-1">
          <div class="tab-pane show active" id="divMenu" role="tabpanel" aria-labelledby="tabMenu" runat="server" ClientIDMode="Static">
              <div class="row">
                  <div class="col-4 col-md-2">
                    <div class="nav nav-pills d-flex flex-column" id="tabsTiposPlato" role="tablist" aria-orientation="horizontal">
                      <a class="nav-link active" id="tabEntradas" data-toggle="pill" href="#divEntradas" role="tab" aria-controls="divEntradas" aria-selected="false" runat="server">Entradas</a>
                      <a class="nav-link" id="tabPlatosFondo" data-toggle="pill" href="#divPlatosFondo" role="tab" aria-controls="divPlatosFondo" aria-selected="false" runat="server">Platos de Fondo</a>
                      <a class="nav-link" id="tabEnsaladas" data-toggle="pill" href="#divEnsaladas" role="tab" aria-controls="divEnsaladas" aria-selected="false" runat="server">Ensaladas</a>
                      <a class="nav-link" id="tabPostres" data-toggle="pill" href="#divPostres" role="tab" aria-controls="divPostres" aria-selected="false" runat="server">Postres</a>
                      <a class="nav-link" id="tabBebestibles" data-toggle="pill" href="#divBebestibles" role="tab" aria-controls="divBebestibles" aria-selected="false" runat="server">Bebestibles</a>
                    </div>
                  </div> 

                  <!-- Tabs menu-->
                  <div class="col-8 col-md-10">
                    <!-- Tab Entradas -->
                    <div class="tab-content py-3 px-1">
                      <div class="tab-pane show active" id="divEntradas" role="tabpanel" aria-labelledby="tabEntradas" runat="server" ClientIDMode="Static">
                          <asp:UpdatePanel ID="upMenu" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                            <ContentTemplate>
                              <div class="text-center">
                                <asp:Label ID="listaEntradasVacia" runat="server" 
                                    Text="No hay entradas disponibles" CssClass="d-inline-block h5 my-5"></asp:Label>
                              </div>   
                              <div class="table-responsive pt-3">
                                <asp:Repeater ID="listaEntradas" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
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
                                                Pedir</asp:LinkButton></td>
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
                      <!-- Fin Tab Entradas -->
                      <!-- Tab Platos de Fondo -->
                      <div class="tab-pane fade" id="divPlatosFondo" role="tabpanel" aria-labelledby="tabPlatosFondo" runat="server" ClientIDMode="Static">
                        <div class="text-center">
                            <asp:Label ID="listaPlatosFondoVacia" runat="server" 
                                Text="No hay platos de fondo disponibles" CssClass="d-inline-block h5 my-5"></asp:Label>
                        </div>   
                        <div class="table-responsive pt-3">
                            <asp:Repeater ID="listaPlatosFondo" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
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
                                            Pedir</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                      </div>                  
                      <!-- Fin Tab Platos de Fondo -->
                      <!-- Tab Ensaldas -->
                      <div class="tab-pane fade" id="divEnsaladas" role="tabpanel" aria-labelledby="tabEnsaladas" runat="server" ClientIDMode="Static">
                        <div class="text-center">
                        <asp:Label ID="listaEnsaladasVacia" runat="server" 
                            Text="No hay ensaladas disponibles" CssClass="d-inline-block h5 my-5"></asp:Label>
                        </div>   
                        <div class="table-responsive pt-3">
                            <asp:Repeater ID="listaEnsaladas" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
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
                                            Pedir</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                      </div>                  
                      <!-- Fin Tab Ensaladas -->
                      <!-- Tab Postres -->
                      <div class="tab-pane fade" id="divPostres" role="tabpanel" aria-labelledby="tabPostres" runat="server" ClientIDMode="Static">
                        <div class="text-center">
                            <asp:Label ID="listaPostresVacia" runat="server" 
                                Text="No hay postres disponibles" CssClass="d-inline-block h5 my-5"></asp:Label>
                        </div>   
                        <div class="table-responsive pt-3">
                        <asp:Repeater ID="listaPostres" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
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
                                        Pedir</asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                      </div>
                     </div>                  
                     <!-- Fin Tab Postres -->
                     <!-- Tab Bebestibles -->
                     <div class="tab-pane fade" id="divBebestibles" role="tabpanel" aria-labelledby="tabBebestibles" runat="server" ClientIDMode="Static">
                        <div class="text-center">
                            <asp:Label ID="listaBebestiblesVacia" runat="server" 
                                Text="No hay bebestibles disponibles" CssClass="d-inline-block h5 my-5"></asp:Label>
                        </div>   
                        <div class="table-responsive pt-3">
                            <asp:Repeater ID="listaBebestibles" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
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
                                            Pedir</asp:LinkButton></td>
                                    </tr>
                                </ItemTemplate>
                                <FooterTemplate>
                                    </table>
                                </FooterTemplate>
                            </asp:Repeater>
                          </div>
                      </div>                  
                      <!-- Fin Tab Bebestibles -->
                </div>
                  </div>
             </div>
          </div>
          <!-- Fin tabs menu-->
          <div class="tab-pane fade" id="divMiOrden" role="tabpanel" aria-labelledby="tabMiOrden" runat="server" ClientIDMode="Static">             
              <asp:UpdatePanel ID="upArticulosPedido" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                 <ContentTemplate>
                    <div class="text-center">
                      <asp:Label ID="listaArticulosPedidoVacia" runat="server" 
                          Text="No ha agregado ningún artículo al pedido" CssClass="d-inline-block h5 my-5"></asp:Label>
                    </div>   
                    <div class="table-responsive pt-3">
                      <asp:Repeater ID="listaArticulosPedido" runat="server" OnItemCommand="btnEliminarArticulo_Click">
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
                          <td><asp:LinkButton ID="btnEliminarArticulo" CommandArgument='<%# Eval("Id") %>' runat="server" >
                                  Eliminar</asp:LinkButton></td>
                          </tr>
                      </ItemTemplate>
                      <FooterTemplate>
                          </table>
                      </FooterTemplate>
                      </asp:Repeater>
                    </div>
                    <div>
                      <asp:Label ID="lblTotalPedido" runat="server"></asp:Label>
                      <asp:TextBox ID="txtTotalPedido" runat="server" Visible="false"></asp:TextBox>
                    </div>
                </ContentTemplate>
              </asp:UpdatePanel>
              <div class="col-12">
                <div class="text-center">
                  <asp:Button ID="btnVerMenu" runat="server" Text="Ver Menú" Visible="true" OnClick="btnVerMenu_Click" CssClass="btn btn-info float-right"/>              
                  <asp:Button ID="btnHacerPedido" runat="server" Text="Hacer Pedido" Visible="false" OnClick="btnHacerPedido_Click" CssClass="btn btn-info float-right"/>              
                  <asp:Button ID="btnPagarCuenta" runat="server" Text="Pagar Cuenta" Visible="false" OnClick="btnPagarCuenta_Click" CssClass="btn btn-info float-right"/>
                </div>
              </div>
          </div>        
        </div>
      </div>
    </div>

    <!-- Modal Articulos -->
    <div class="modal fade" id="modalArticulo" tabindex="-1" role="dialog" aria-labelledby="tituloModalArticulo" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
          <asp:UpdatePanel ID="upModalArticulo" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="lblTituloModalArticulo" class="modal-title h5" runat="server" Text="Pedir"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                      <asp:TextBox ID="txtIdArticulo" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                      <div class="row">                        
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblPrecioArticulo" runat="server" Text="Precio"></asp:Label>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblCantidadArticulo" runat="server" Text="Cantidad"></asp:Label>
                            <asp:TextBox ID="txtCantidadArticulo" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12">
                            <asp:Label ID="lblComentarioArticulo" runat="server" Text="Descripción"></asp:Label>
                            <asp:TextBox ID="txtComentarioArticulo" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                        </div>
                      </div>
                  </div>
                  <div class="modal-footer">
                    <asp:Button ID="btnAgregarPedido" runat="server" CssClass="btn btn-info" Text="Agregar al pedido" OnClick="btnAgregarArticuloPedido_Click"/>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div><!-- Fin modal Articulos -->

</asp:Content>