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
      <div class="col-12 bg-blanco rounded contenedor-mantenedores shadow">
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
                              <asp:Repeater ID="listaEntradas" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
                                <ItemTemplate>
                                  <div class="row">
                                    <div class="col-12"><h4><%# Eval("Nombre") %></h4></div>
                                  </div>
                                  <div class="row mb-4">
                                    <div class="col-12 col-md-5 col-lg-3"><img class="img-fluid" src="<%# Eval("UrlImagen") %>"/></div>
                                    <div class="col-12 col-md-7 col-lg-9">
                                      <p class="text-justify"><%# Eval("Descripcion") %></p>
                                      <h5>$ <%# Eval("Precio") %></h5>
                                      <asp:LinkButton CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' 
                                        runat="server">Pedir</asp:LinkButton>
                                    </div>
                                  </div>
                                </ItemTemplate>
                              </asp:Repeater>
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
                        <asp:Repeater ID="listaPlatosFondo" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">                                      
                            <ItemTemplate>
                              <div class="row">
                                <div class="col-12"><h4><%# Eval("Nombre") %></h4></div>
                              </div>
                              <div class="row mb-4">
                                <div class="col-12 col-md-5 col-lg-3"><img class="img-fluid" src="<%# Eval("UrlImagen") %>"/></div>
                                <div class="col-12 col-md-7 col-lg-9">
                                  <p class="text-justify"><%# Eval("Descripcion") %></p>
                                  <h5>$ <%# Eval("Precio") %></h5>
                                  <asp:LinkButton CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' 
                                    runat="server">Pedir</asp:LinkButton>
                                </div>
                              </div>
                            </ItemTemplate>
                        </asp:Repeater>
                      </div>                  
                      <!-- Fin Tab Platos de Fondo -->
                      <!-- Tab Ensaldas -->
                      <div class="tab-pane fade" id="divEnsaladas" role="tabpanel" aria-labelledby="tabEnsaladas" runat="server" ClientIDMode="Static">
                        <div class="text-center">
                        <asp:Label ID="listaEnsaladasVacia" runat="server" 
                            Text="No hay ensaladas disponibles" CssClass="d-inline-block h5 my-5"></asp:Label>
                        </div>   
                        <asp:Repeater ID="listaEnsaladas" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
                          <ItemTemplate>
                            <div class="row">
                              <div class="col-12"><h4><%# Eval("Nombre") %></h4></div>
                            </div>
                            <div class="row mb-4">
                              <div class="col-12 col-md-5 col-lg-3"><img class="img-fluid" src="<%# Eval("UrlImagen") %>"/></div>
                              <div class="col-12 col-md-7 col-lg-9">
                                <p class="text-justify"><%# Eval("Descripcion") %></p>
                                <h5>$ <%# Eval("Precio") %></h5>
                                <asp:LinkButton CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' 
                                  runat="server">Pedir</asp:LinkButton>
                              </div>
                            </div>
                          </ItemTemplate>
                        </asp:Repeater>
                      </div>                  
                      <!-- Fin Tab Ensaladas -->
                      <!-- Tab Postres -->
                      <div class="tab-pane fade" id="divPostres" role="tabpanel" aria-labelledby="tabPostres" runat="server" ClientIDMode="Static">
                        <div class="text-center">
                            <asp:Label ID="listaPostresVacia" runat="server" 
                                Text="No hay postres disponibles" CssClass="d-inline-block h5 my-5"></asp:Label>
                        </div>   
                        <asp:Repeater ID="listaPostres" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
                          <ItemTemplate>
                            <div class="row">
                              <div class="col-12"><h4><%# Eval("Nombre") %></h4></div>
                            </div>
                            <div class="row mb-4">
                              <div class="col-12 col-md-5 col-lg-3"><img class="img-fluid" src="<%# Eval("UrlImagen") %>"/></div>
                              <div class="col-12 col-md-7 col-lg-9">
                                <p class="text-justify"><%# Eval("Descripcion") %></p>
                                <h5>$ <%# Eval("Precio") %></h5>
                                <asp:LinkButton CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' 
                                  runat="server">Pedir</asp:LinkButton>
                              </div>
                            </div>
                          </ItemTemplate>
                        </asp:Repeater>
                     </div>                  
                     <!-- Fin Tab Postres -->
                     <!-- Tab Bebestibles -->
                     <div class="tab-pane fade" id="divBebestibles" role="tabpanel" aria-labelledby="tabBebestibles" runat="server" ClientIDMode="Static">
                        <div class="text-center">
                            <asp:Label ID="listaBebestiblesVacia" runat="server" 
                                Text="No hay bebestibles disponibles" CssClass="d-inline-block h5 my-5"></asp:Label>
                        </div>   
                        <asp:Repeater ID="listaBebestibles" runat="server" OnItemCommand="btnModalAgregarArticulo_Click">
                            <ItemTemplate>
                              <div class="row">
                                <div class="col-12"><h4><%# Eval("Nombre") %></h4></div>
                              </div>
                              <div class="row mb-4">
                                <div class="col-12 col-md-5 col-lg-3"><img class="img-fluid" src="<%# Eval("UrlImagen") %>"/></div>
                                <div class="col-12 col-md-7 col-lg-9">
                                  <p class="text-justify"><%# Eval("Descripcion") %></p>
                                  <h5>$ <%# Eval("Precio") %></h5>
                                  <asp:LinkButton CssClass="btn btn-primary" CommandArgument='<%# Eval("Id") %>' 
                                    runat="server">Pedir</asp:LinkButton>
                                </div>
                              </div>
                            </ItemTemplate>
                          </asp:Repeater>
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
                              <td><b>Precio</b></td>
                              <td><b>Cantidad</b></td>
                              <td><b>Total</b></td>
                              <td><b>Estado</b></td>
                              <td><b>Acciones</b></td>
                          </tr>
                      </HeaderTemplate>          
                      <ItemTemplate>
                          <tr>
                          <td> <%# Eval("Articulo.Nombre") %> </td>
                          <td> <%# Eval("Precio") %> </td>
                          <td> <%# Eval("Cantidad") %> </td>
                          <td> <%# Eval("Total") %> </td>
                          <td> <%# Eval("EstadoArticuloPedido.Nombre") %> </td>
                          <td><asp:LinkButton ID="btnEliminarArticulo" CommandArgument='<%# Eval("Id") %>' runat="server" >
                                  Eliminar</asp:LinkButton></td>
                          </tr>
                      </ItemTemplate>
                      <FooterTemplate>
                          </table>
                      </FooterTemplate>
                      </asp:Repeater>
                    </div>
                    <div class="text-right font-weight-bold">
                      <asp:Label ID="lblTotalPedido" runat="server"></asp:Label>
                      <asp:TextBox ID="txtTotalPedido" runat="server" Visible="false"></asp:TextBox>
                    </div>
                    <div class="row">
                      <div class="col-12 col-md-4 col-lg-2 m-auto">
                        <asp:Button ID="btnHacerPedido" runat="server" Text="Hacer Pedido" Visible="false" OnClick="btnHacerPedido_Click" CssClass="btn btn-info"/>              
                        <asp:Button ID="btnCerrarCuenta" runat="server" Text="Cerrar Cuenta" Visible="false" OnClick="btnCerrarCuenta_Click" CssClass="btn btn-info"/>
                      </div>
                    </div>
                </ContentTemplate>
              </asp:UpdatePanel>
          </div>        
        </div>
      </div>
    </div>

    <!-- Modal Articulos -->
    <div class="modal fade" id="modalArticulo" tabindex="-1" role="dialog" aria-labelledby="tituloModalArticulo" aria-hidden="true">
      <div class="modal-dialog" role="document">
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
                            Precio unitario: <asp:Label ID="lblPrecioArticulo" runat="server" Text="Precio"></asp:Label>
                        </div>
                        <div class="col-12 col-md-6">
                          <asp:Label ID="lblCantidadArticulo" runat="server" Text="Cantidad"></asp:Label>
                          <asp:TextBox ID="txtCantidadArticulo" runat="server" CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="ValidacionCantidad" runat="server" ControlToValidate="txtCantidadArticulo" Display="Dynamic"
                          CssClass="text-danger" ErrorMessage="Debe ingresar la cantidad" ValidationGroup="ValidacionArticulo"></asp:RequiredFieldValidator>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12">
                            <asp:Label ID="lblComentarioArticulo" runat="server" Text="Comentarios"></asp:Label>
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

   <!-- Modal Cerrar cuenta -->
    <div class="modal fade" id="modalCerrarCuenta" tabindex="-1" role="dialog" aria-labelledby="lblCerrarCuenta" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <asp:UpdatePanel ID="upModalCerrarCuenta" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
          <ContentTemplate>  
            <div class="modal-content">                    
              <div class="modal-header">
                <asp:Label ID="lblCerrarCuenta" class="modal-title h5" runat="server" Text="Cerrar la cuenta"></asp:Label>
                <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
					    <div class="modal-body py-4">
                <div class="text-center">
                  <div class="alert alert-warning"><p class="lead font-weight-bold">¡Atención!</p>
				   	        <p> Al cerrar la cuenta no podrá pedir más productos</p>
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                  </div>
                  <br />
                  <h5 class="d-inline-block mt-3">Total a pagar: $</h5>
                  <asp:Label ID="lblTotalPagar" runat="server" CssClass="h5 d-inline-block"></asp:Label>
                </div> 
                <div class="row">
                  <div class="col-12 col-sm-6">
                      <asp:Label runat="server" Text="Tipo de documento de pago" CssClass="text-left"></asp:Label>                    
                  </div>
                  <div class="col-12 col-sm-6">
                    <asp:DropDownList ID="ddlTipoDocumentoPago" runat="server" CssClass="form-control"></asp:DropDownList>
                    <asp:RequiredFieldValidator ID="ValidadorTipoDocumento" runat="server" ControlToValidate="ddlTipoDocumentoPago" Display="Dynamic"
                    CssClass="text-danger" ErrorMessage="Debe seleccionar el tipo de documento" ValidationGroup="ValidacionTipoDocumento"></asp:RequiredFieldValidator>
                  </div>
                </div>
                <div class="row">
                  <div class="col-12">
                    <asp:Label runat="server" Text="Medio de pago"></asp:Label>
                    <br />
                    <div class="d-flex justify-content-between">
						          <asp:Button ID="btnPagarEfectivo" runat="server" CssClass="btn btn-success col-4" Text="Efectivo" OnClick="btnPagarEfectivo_Click" />
						          <asp:Button ID="btnPagarMixto" runat="server" CssClass="btn btn-warning col-3" Text="Mixto" OnClick="btnPagarMixto_Click" />
                      <asp:Button ID="btnPagarTarjeta" runat="server" CssClass="btn btn-primary col-4" Text="Tarjeta" OnClick="btnPagarTarjeta_Click" />
                    </div>
                  </div>
                </div> 
              </div>
            </div>
           </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div><!-- Fin modal Cerrar Cuenta -->

     <!-- Modal Pago mixto -->
    <div class="modal fade" id="modalPagoMixto" tabindex="-1" role="dialog" aria-labelledby="lblSeleccionarPago" aria-hidden="true">
      <div class="modal-dialog" role="document">
        <asp:UpdatePanel ID="upModalPagoMixto" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
          <ContentTemplate>  
            <div class="modal-content">                    
              <div class="modal-header">
                <asp:Label ID="lblSeleccionarPago" class="modal-title h5" runat="server" Text="Pago Mixto"></asp:Label>
                <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
					    <div class="modal-body py-4">
                <div class="text-center">
                  <h5 class="d-inline-block mt-3">Total a pagar: $</h5>
                  <asp:Label ID="lblTotalPagarCuenta" runat="server" CssClass="h5 d-inline-block"></asp:Label>
                  <br />
                  <p> Ingrese montos a pagar:</p>
                </div>
                <div class="row">
                  <div class="col-12 col-md-6 form-group">
                    <asp:Label ID="lblMontoEfectivo" runat="server" CssClass="h5 d-inline-block">En efectivo:</asp:Label>
						        <asp:TextBox ID="txtMontoEfectivo" runat="server" CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidacionMontoEfectivo" runat="server" ControlToValidate="txtMontoEfectivo" Display="Dynamic"
                    CssClass="text-danger" ErrorMessage="Debe ingresar el monto con efectivo" ValidationGroup="ValidacionMontoPago"></asp:RequiredFieldValidator>
                  </div>
                  <div class="col-12 col-md-6 form-group">
                    <asp:Label ID="lblMontoTarjeta" runat="server" CssClass="h5 d-inline-block">Con tarjeta:</asp:Label>
						        <asp:TextBox ID="txtMontoTarjeta" runat="server" CssClass="form-control" TextMode="Number" min="1"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="ValidacionMontoTarjeta" runat="server" ControlToValidate="txtMontoTarjeta" Display="Dynamic"
                    CssClass="text-danger" ErrorMessage="Debe ingresar el monto con tarjeta " ValidationGroup="ValidacionMontoPago"></asp:RequiredFieldValidator>
                  </div>
                  <div class="col-12 text-right">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnMontosPagoMixto" runat="server" CssClass="btn btn-info" Text="Pagar" OnClick="btnMontosPagoMixto_Click" />
                  </div>
					      </div>
              </div>
             </div>
          </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div><!-- Fin modal Pago mixto -->
</asp:Content>