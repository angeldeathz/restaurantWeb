using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;

namespace Restaurant.Web.Paginas.Autoservicio
{
    public partial class GestionAutoservicio : System.Web.UI.Page
    {
        private PedidoService _pedidoService;
        private ArticuloPedidoService _articuloPedidoService;
        private ArticuloService _articuloService;
        private TipoDocumentoPagoService _tipoDocumentoPagoService;
        private DocumentoPagoService _documentoPagoService;
        private MedioPagoDocumentoService _medioPagoDocumentoService;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                validarIngreso();
                Token token = (Token)Session["token"];
                Reserva reserva = (Reserva)Session["reservaCliente"];
                _pedidoService = new PedidoService(token.access_token);

                List<Pedido> pedidos = _pedidoService.Obtener();

                Pedido pedidoCliente = null;
                if (pedidos != null && pedidos.Count > 0)
                {
                    pedidoCliente = pedidos.FirstOrDefault(x => x.IdEstadoPedido != EstadoPedido.cancelado
                                                             && x.Reserva.Id == reserva.Id
                                                             && x.FechaHoraInicio.Date == DateTime.Now.Date);
                    if (pedidoCliente != null)
                    {

                        if (pedidoCliente.IdEstadoPedido == EstadoPedido.pagado)
                        {
                            Response.Redirect("/Paginas/Autoservicio/PedidoPagado.aspx");
                            return;
                        }

                        cargarPedido(token, pedidoCliente);
                    }
                }

                List<ArticuloPedido> articulosPedido = new List<ArticuloPedido>();
                if (Session["articulosPedidoCliente"] == null)
                {
                    Session["articulosPedidoCliente"] = articulosPedido;
                }
                else
                {
                    articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                }

                if (articulosPedido != null && articulosPedido.Count > 0)
                {
                    recargarArticulosPedido(articulosPedido);
                    if (pedidoCliente != null)
                    {
                        btnHacerPedido.Visible = false;
                        if (pedidoCliente.IdEstadoPedido == EstadoPedido.enCurso
                            || pedidoCliente.IdEstadoPedido == EstadoPedido.cerradoConTarjeta
                            || (pedidoCliente.IdEstadoPedido == EstadoPedido.cerradoMixto && !PagoTarjetaListo(pedidoCliente.Id)))
                        {
                            btnCerrarCuenta.Visible = true;

                            if (pedidoCliente.IdEstadoPedido == EstadoPedido.cerradoConTarjeta
                               || pedidoCliente.IdEstadoPedido == EstadoPedido.cerradoMixto)
                            {
                                btnCerrarCuenta.Text = "Ir a pagar";
                            }
                        }

                        if (pedidoCliente.IdEstadoPedido != EstadoPedido.enCurso)
                        {
                            tabMenu.Attributes.Add("class", "nav-link d-none");
                            divMenu.Attributes.Add("class", "tab-pane fade d-none");
                            tabMiOrden.Attributes.Add("class", "nav-link active");
                            divMiOrden.Attributes.Add("class", "tab-pane active show");
                        }
                    }
                    else
                    {
                        btnHacerPedido.Visible = true;
                        btnCerrarCuenta.Visible = false;
                    }
                }
                upArticulosPedido.Update();
            }
            catch (Exception ex)
            {
                //Nada 
            }

            if (!IsPostBack)
            {
                try
                {
                    Token token = (Token)Session["token"];
                    _articuloService = new ArticuloService(token.access_token);
                    List<Articulo> articulos = _articuloService.Obtener();
                    List<Articulo> articulosDisponibles = articulos.Where(x => x.IdEstadoArticulo == EstadoArticulo.disponible).ToList();
                    articulosDisponibles = articulosDisponibles.OrderBy(x => x.Nombre).ToList();
                    Session["articulosDisponibles"] = articulosDisponibles;

                    List<Articulo> entradas = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.entradas).ToList();
                    listaEntradas.DataSource = entradas;
                    listaEntradas.DataBind();
                    actualizarRepeater(listaEntradas, entradas, listaEntradasVacia);

                    List<Articulo> platosFondo = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.platosFondo).ToList();
                    listaPlatosFondo.DataSource = platosFondo;
                    listaPlatosFondo.DataBind();
                    actualizarRepeater(listaPlatosFondo, platosFondo, listaPlatosFondoVacia);

                    List<Articulo> ensaladas = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.ensaladas).ToList();
                    listaEnsaladas.DataSource = ensaladas;
                    listaEnsaladas.DataBind();
                    actualizarRepeater(listaEnsaladas, ensaladas, listaEnsaladasVacia);

                    List<Articulo> postres = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.postres).ToList();
                    listaPostres.DataSource = postres;
                    listaPostres.DataBind();
                    actualizarRepeater(listaPostres, postres, listaPostresVacia);

                    List<Articulo> bebestibles = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.bebestibles).ToList();
                    listaBebestibles.DataSource = bebestibles;
                    listaBebestibles.DataBind();
                    actualizarRepeater(listaBebestibles, bebestibles, listaBebestiblesVacia);

                    _tipoDocumentoPagoService = new TipoDocumentoPagoService(token.access_token);
                    List<TipoDocumentoPago> tiposDocumento = _tipoDocumentoPagoService.Obtener();
                    if (tiposDocumento != null && tiposDocumento.Count > 0)
                    {
                        ddlTipoDocumentoPago.DataSource = tiposDocumento;
                        ddlTipoDocumentoPago.DataTextField = "Nombre";
                        ddlTipoDocumentoPago.DataValueField = "Id";
                        ddlTipoDocumentoPago.DataBind();
                        ddlTipoDocumentoPago.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlTipoDocumentoPago.SelectedIndex = 0;
                    }
                }
                catch (Exception ex)
                {
                    //Nada
                }
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Token token = (Token)Session["token"];
            Reserva reserva = (Reserva)Session["reservaCliente"];
            _pedidoService = new PedidoService(token.access_token);

            List<Pedido> pedidos = _pedidoService.Obtener();

            if (pedidos != null && pedidos.Count > 0)
            {
                var pedidoCliente = pedidos.FirstOrDefault(x => x.IdEstadoPedido != EstadoPedido.cancelado
                                                                && x.Reserva.Id == reserva.Id
                                                                && x.FechaHoraInicio.Date == DateTime.Now.Date);
                if (pedidoCliente != null)
                {

                    if (pedidoCliente.IdEstadoPedido == EstadoPedido.pagado)
                    {
                        Response.Redirect("/Paginas/Autoservicio/PedidoPagado.aspx");
                        return;
                    }

                    cargarPedido(token, pedidoCliente);
                }
            }
        }

        protected void validarIngreso()
        {
            if (Session["reservaCliente"] == null || Session["token"] == null)
            {
                Response.Redirect("/Paginas/Publica/Autoservicio.aspx");
            }
        }
        protected void cargarPedido(Token token, Pedido pedido)
        {
            Session["pedidoCliente"] = pedido;
            _articuloPedidoService = new ArticuloPedidoService(token.access_token);
            List<ArticuloPedido> articulos = _articuloPedidoService.Obtener();
            if(articulos == null || articulos.Count == 0)
            {
                return;
            }
            List<ArticuloPedido> articulosPedido = articulos.Where(x => x.IdPedido == pedido.Id).ToList();
            recargarArticulosPedido(articulosPedido);
        }

        protected void recargarArticulosPedido(List<ArticuloPedido> articulosPedido)
        {
            Session["articulosPedidoCliente"] = articulosPedido;
            actualizarRepeater(listaArticulosPedido, articulosPedido, listaArticulosPedidoVacia);

            var totalPedido = articulosPedido.Sum(x => x.Total);
            if (totalPedido == 0)
            {
                lblTotalPedido.Text = "";
            }
            else
            {
                lblTotalPedido.Text = "Total: " + string.Format("${0:N0}", totalPedido) + "-";
            }
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();
        }
        protected void btnEliminarArticulo_Click(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                int idArticulo;
                if (int.TryParse((string)e.CommandArgument, out idArticulo))
                {
                    List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                    var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                    if (articuloEliminar != null)
                    {
                        listaArticulos.Remove(articuloEliminar);

                        Token token = (Token)Session["token"];
                        _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                        _articuloPedidoService.Eliminar(articuloEliminar.Id);
                    }

                    Session["articulosPedidoCliente"] = listaArticulos;
                    actualizarRepeater(listaArticulosPedido, listaArticulos, listaArticulosPedidoVacia);
                    var totalPedido = listaArticulos.Sum(x => x.Total);
                    if (totalPedido == 0)
                    {
                        lblTotalPedido.Text = "";
                        btnHacerPedido.Visible = false;
                    }
                    else
                    {
                        lblTotalPedido.Text = "Total: " + string.Format("${0:N0}", totalPedido) + "-";
                    }
                    txtTotalPedido.Text = totalPedido.ToString();
                    upArticulosPedido.Update();
                }
                limpiarTabs();
                tabMiOrden.Attributes.Add("class", "nav-link active");
                divMiOrden.Attributes.Add("class", "tab-pane active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }
        protected void btnModalAgregarArticulo_Click(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                validarIngreso();
                int idArticulo;
                if (int.TryParse((string)e.CommandArgument, out idArticulo))
                {
                    List<Articulo> articulosDisponibles = (List<Articulo>)Session["articulosDisponibles"];
                    Articulo articulo = articulosDisponibles.FirstOrDefault(a => a.Id == idArticulo);
                    txtIdArticulo.Text = idArticulo.ToString();
                    txtCantidadArticulo.Text = "";
                    txtComentarioArticulo.Text = "";
                    lblTituloModalArticulo.Text = "Pedir " + articulo.Nombre;
                    lblPrecioArticulo.Text = string.Format("${0:N0}", articulo.Precio);
                    upModalArticulo.Update();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
                    limpiarTabsMenu(articulo.IdTipoConsumo);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "Swal.fire('Error al agregar artículo', '', 'error');", true);
                }
                limpiarTabs();
                
                tabMenu.Attributes.Add("class", "nav-link active");
                divMenu.Attributes.Add("class", "tab-pane active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnAgregarArticuloPedido_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Page.Validate("ValidacionArticulo");
            if (!Page.IsValid)
            {
                upModalArticulo.Update();
                return;
            }
            try
            {
                int cantidad = Convert.ToInt32(txtCantidadArticulo.Text);
                string comentarios = txtComentarioArticulo.Text;
                int idArticulo = Convert.ToInt32(txtIdArticulo.Text);

                if(cantidad > 10)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "erroArticulo", "Swal.fire('La cantidad máxima por pedido es de 10 artículos', '', 'error');", true);
                    return;
                }
                // Info de artículos disponibles para pedir
                List<Articulo> articulosDisponibles = (List<Articulo>)Session["articulosDisponibles"];
                Articulo articulo = articulosDisponibles.FirstOrDefault(a => a.Id == idArticulo);
                if (articulo == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "erroArticulo", "Swal.fire('Error al agregar artículo', '', 'error');", true);
                    return;
                }

                List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                Pedido pedidoCliente = (Pedido)Session["pedidoCliente"];

                ArticuloPedido nuevoArticuloPedido = crearArticuloPedido(articulo, cantidad, comentarios);
                articulosPedido.Add(nuevoArticuloPedido);
                if (pedidoCliente != null)
                {
                    guardarArticuloPedido(nuevoArticuloPedido, pedidoCliente.Id);
                }
                else
                {
                    btnHacerPedido.Visible = true;
                }

                recargarArticulosPedido(articulosPedido);

                if (pedidoCliente != null)
                {
                    pedidoCliente.Total = articulosPedido.Sum(x => x.Total);
                    Token token = (Token)Session["token"];
                    _pedidoService = new PedidoService(token.access_token);
                    pedidoCliente.EstadoPedido = null;
                    pedidoCliente.Reserva = null;
                    bool editar = _pedidoService.Modificar(pedidoCliente, pedidoCliente.Id);
                }
                limpiarTabsMenu(articulo.IdTipoConsumo);
                Session["pedidoCliente"] = pedidoCliente;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "creacionArticulo", "Swal.fire('Artículo agregado al pedido', '', 'success');", true);
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnEliminarArticuloPedido_Click(object sender, RepeaterCommandEventArgs e)
        {
            validarIngreso();
            Pedido pedidoCliente = (Pedido)Session["pedidoCliente"];
            if(pedidoCliente != null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "eliminarArticulo", "Swal.fire('No se pueden eliminar artículos enviados a la cocina', '', 'warning');", true);
                return;
            }

            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                if (articuloEliminar != null)
                {
                    listaArticulos.Remove(articuloEliminar);
                }
                recargarArticulosPedido(listaArticulos);
            }
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }
        protected void btnHacerPedido_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Pedido pedidoCliente = (Pedido)Session["pedidoCliente"];
            if (pedidoCliente != null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPedido", "Swal.fire('Ya se creó un pedido', '', 'warning');", true);
                return;
            }

            int idPedido = guardarPedido();
            if (idPedido != 0)
            {
                List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                foreach (ArticuloPedido articuloPedido in articulosPedido)
                {
                    guardarArticuloPedido(articuloPedido, idPedido);
                }

                btnHacerPedido.Visible = false;
                btnCerrarCuenta.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPedido", "Swal.fire('Pedido enviado a la cocina', '', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "Swal.fire('Error al hacer pedido', '', 'error');", true);
            }
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }
        protected ArticuloPedido crearArticuloPedido(Articulo articulo, int cantidad, string comentarios)
        {
            ArticuloPedido nuevoArticuloPedido = new ArticuloPedido();
            nuevoArticuloPedido.Articulo = articulo;
            nuevoArticuloPedido.IdArticulo = articulo.Id;
            nuevoArticuloPedido.Precio = articulo.Precio;
            nuevoArticuloPedido.Cantidad = cantidad;
            nuevoArticuloPedido.Total = nuevoArticuloPedido.Precio * cantidad;
            nuevoArticuloPedido.Comentarios = comentarios;

            EstadoArticuloPedido estadoInicialArticuloPedido = new EstadoArticuloPedido();
            estadoInicialArticuloPedido.Id = EstadoArticuloPedido.pendiente;
            estadoInicialArticuloPedido.Nombre = "Pendiente";
            nuevoArticuloPedido.EstadosArticuloPedido = new List<EstadoArticuloPedido>();
            nuevoArticuloPedido.EstadosArticuloPedido.Add(estadoInicialArticuloPedido);
            nuevoArticuloPedido.IdEstadoArticuloPedido = estadoInicialArticuloPedido.Id;

            return nuevoArticuloPedido;
        }

        protected bool guardarArticuloPedido(ArticuloPedido articuloPedido, int idPedido)
        {
            articuloPedido.IdPedido = idPedido;

            Articulo articuloAux = articuloPedido.Articulo;
            EstadoArticuloPedido estadoArticuloAux = articuloPedido.EstadoArticuloPedido;
            List<EstadoArticuloPedido> estadosAux = articuloPedido.EstadosArticuloPedido;
            articuloPedido.Articulo = null;
            articuloPedido.EstadosArticuloPedido = null;

            Token token = (Token)Session["token"];
            _articuloPedidoService = new ArticuloPedidoService(token.access_token);
            int idArticuloPedido = _articuloPedidoService.Guardar(articuloPedido);
            if (idArticuloPedido == 0)
            {
                return false;
            }

            articuloPedido.Id = idArticuloPedido;
            articuloPedido.Articulo = articuloAux;
            articuloPedido.EstadosArticuloPedido = estadosAux;
            return true;
        }
        
        protected int guardarPedido()
        {
            Reserva reserva = (Reserva)Session["reservaCliente"];

            Pedido pedido = new Pedido();
            pedido.FechaHoraInicio = DateTime.Now;
            pedido.FechaHoraFin = DateTime.Now;
            pedido.Total = int.Parse(txtTotalPedido.Text);
            pedido.IdEstadoPedido = EstadoPedido.enCurso;
            pedido.IdReserva = reserva.Id;

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            int idPedido = _pedidoService.Guardar(pedido);
            if (idPedido != 0)
            {
                pedido.Id = idPedido;
                Session["pedidoCliente"] = pedido;
            }
            return idPedido;
        }
        protected void btnCerrarCuenta_Click(object sender, EventArgs e)
        {
            try
            {
                validarIngreso();
                Pedido pedido = (Pedido)Session["pedidoCliente"];
                List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                var totalPedido = articulosPedido.Sum(x => x.Total);
                lblTotalPagar.Text = string.Format("${0:N0}", totalPedido);
                upModalCerrarCuenta.Update();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCerrarCuenta", "$('#modalCerrarCuenta').modal('show');", true);
                limpiarTabs();
                tabMiOrden.Attributes.Add("class", "nav-link active");
                divMiOrden.Attributes.Add("class", "tab-pane active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnPagarEfectivo_Click(object sender, EventArgs e)
        {
            int estadoPedido = EstadoPedido.cerradoConEfectivo;
            generarPago(estadoPedido);
        }

        protected void btnPagarTarjeta_Click(object sender, EventArgs e)
        {
            int estadoPedido = EstadoPedido.cerradoConTarjeta;
            generarPago(estadoPedido);
        }

        protected void btnPagarMixto_Click(object sender, EventArgs e)
        {
            int estadoPedido = EstadoPedido.cerradoMixto;
            generarPago(estadoPedido);
        }

        protected void generarPago(int estadoPedido)
        {
            tabMenu.Attributes.Add("class", "nav-link d-none");
            divMenu.Attributes.Add("class", "tab-pane fade d-none");
            validarIngreso();
            Page.Validate("ValidacionTipoDocumento");
            if (!Page.IsValid)
            {
                upModalCerrarCuenta.Update();
                return;
            }
            if (ddlTipoDocumentoPago.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "documentoPago", "Swal.fire('Debe seleccionar el tipo de documento de pago', '', 'warning');", true);
                return;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCerrarCuenta", "$('#modalCerrarCuenta').modal('hide');", true);
            try
            {
                Session["tipoDocumentoPago"] = Convert.ToInt32(ddlTipoDocumentoPago.SelectedValue);

                Pedido pedido = (Pedido)Session["pedidoCliente"];
                List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                if (estadoPedido != EstadoPedido.cerradoMixto)
                {
                    pedido.IdEstadoPedido = estadoPedido;
                }
                var totalPedido = articulosPedido.Sum(x => x.Total);
                pedido.Total = totalPedido;
                pedido.Reserva = null;
                pedido.EstadoPedido = null;

                Token token = (Token)Session["token"];
                _pedidoService = new PedidoService(token.access_token);
                bool editar = _pedidoService.Modificar(pedido, pedido.Id);

                if (!editar)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarCuenta", "Swal.fire('Error al cerrar la cuenta', '', 'error');", true);
                }
                Session["pedidoCliente"] = pedido;
                btnHacerPedido.Visible = false;

                switch (estadoPedido)
                {
                    case EstadoPedido.cerradoConEfectivo:
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "Swal.fire('Por favor espere. Un garzón acudirá para realizar el pago', '', 'warning');", true);
                        btnCerrarCuenta.Visible = false;
                        break;
                    case EstadoPedido.cerradoConTarjeta:
                        Response.Redirect("/Paginas/Autoservicio/PagoTarjeta.aspx");
                        btnCerrarCuenta.Visible = false;
                        break;
                    case EstadoPedido.cerradoMixto:
                        lblTotalPagarCuenta.Text = string.Format("${0:N0}", pedido.Total);
                        upModalPagoMixto.Update();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPagoMixto", "$('#modalPagoMixto').modal('show');", true);
                        break;
                }

                limpiarTabs();
                tabMiOrden.Attributes.Add("class", "nav-link active");
                divMiOrden.Attributes.Add("class", "tab-pane active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnMontosPagoMixto_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Page.Validate("ValidacionMontoPago");
            if(!Page.IsValid)
            {
                upModalPagoMixto.Update();
                return;
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCerrarCuenta", "$('#modalPagoMixto').modal('hide');", true);

            try
            {
                Pedido pedido = (Pedido)Session["pedidoCliente"];
                int montoEfectivo = Convert.ToInt32(txtMontoEfectivo.Text);
                int montoTarjeta = Convert.ToInt32(txtMontoTarjeta.Text);
                if ((montoEfectivo + montoTarjeta) != pedido.Total)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "Swal.fire('La suma de los montos ingresados debe ser igual al total a pagar', '', 'error').then(function(){$('#modalPagoMixto').modal('show');});", true);
                    return;
                }

                pedido.IdEstadoPedido = EstadoPedido.cerradoMixto;
                pedido.Reserva = null;
                pedido.EstadoPedido = null;

                Token token = (Token)Session["token"];
                _pedidoService = new PedidoService(token.access_token);
                bool editar = _pedidoService.Modificar(pedido, pedido.Id);

                if (!editar)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarCuenta", "Swal.fire('Error al cerrar la cuenta', '', 'error');", true);
                }
                Session["pedidoCliente"] = pedido;
                Session["montoTarjeta"] = montoTarjeta;
                btnHacerPedido.Visible = false;
                btnCerrarCuenta.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "Swal.fire('Realice el pago con tarjeta mientras un garzón acude a su mesa para hacer el pago en efectivo', '', 'warning').then(function(){location.replace('/Paginas/Autoservicio/PagoTarjeta.aspx');});", true);
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        public bool PagoTarjetaListo(int idPedido)
        {
            Token token = (Token)Session["token"];
            _documentoPagoService = new DocumentoPagoService(token.access_token);
            List<DocumentoPago> listaDocumentoPago = _documentoPagoService.Obtener();
            if(listaDocumentoPago == null)
            {
                return false;
            }
            DocumentoPago documentoPago = listaDocumentoPago.FirstOrDefault(x => x.IdPedido == idPedido);
            if(documentoPago == null)
            {
                return false;
            }
            _medioPagoDocumentoService = new MedioPagoDocumentoService(token.access_token);
            List<MedioPagoDocumento> listaMedioPagoDocumentos = _medioPagoDocumentoService.Obtener();
            MedioPagoDocumento medioPagoDocumento = listaMedioPagoDocumentos.FirstOrDefault(x => x.IdDocumentoPago == documentoPago.Id
                                                                                              && x.IdMedioPago != MedioPago.efectivo);
            if (medioPagoDocumento == null)
            {
                return false;
            }
            return true;
        }
        public void actualizarRepeater<T>(Repeater repeater, List<T> listaData, Label mensajeListaVacia)
        {
            repeater.DataSource = listaData;
            repeater.DataBind();
            if (listaData.Count() == 0)
            {
                repeater.Visible = false;
                mensajeListaVacia.Visible = true;
            }
            else
            {
                repeater.Visible = true;
                mensajeListaVacia.Visible = false;
            }
        }
        public void limpiarTabs()
        {
            tabMenu.Attributes.Add("class", "nav-link");
            tabMiOrden.Attributes.Add("class", "nav-link");
            divMenu.Attributes.Add("class", "tab-pane fade");
            divMiOrden.Attributes.Add("class", "tab-pane fade");
        }

        public void limpiarTabsMenu(int idTipoConsumo)
        {
            tabEntradas.Attributes.Add("class", "nav-link");
            tabPlatosFondo.Attributes.Add("class", "nav-link");
            tabEnsaladas.Attributes.Add("class", "nav-link");
            tabPostres.Attributes.Add("class", "nav-link");
            tabBebestibles.Attributes.Add("class", "nav-link");
            divEntradas.Attributes.Add("class", "tab-pane fade");
            divPlatosFondo.Attributes.Add("class", "tab-pane fade");
            divEnsaladas.Attributes.Add("class", "tab-pane fade");
            divPostres.Attributes.Add("class", "tab-pane fade");
            divBebestibles.Attributes.Add("class", "tab-pane fade");

            switch(idTipoConsumo)
            {
                case TipoConsumo.bebestibles:
                    tabBebestibles.Attributes.Add("class", "nav-link active");
                    divBebestibles.Attributes.Add("class", "tab-pane active show");
                    break;
                case TipoConsumo.platosFondo:
                    tabPlatosFondo.Attributes.Add("class", "nav-link active");
                    divPlatosFondo.Attributes.Add("class", "tab-pane active show");
                    break;
                case TipoConsumo.ensaladas:
                    tabEnsaladas.Attributes.Add("class", "nav-link active");
                    divEnsaladas.Attributes.Add("class", "tab-pane active show");
                    break;
                case TipoConsumo.postres:
                    tabPostres.Attributes.Add("class", "nav-link active");
                    divPostres.Attributes.Add("class", "tab-pane active show");
                    break;
                case TipoConsumo.entradas:
                default:
                    tabEntradas.Attributes.Add("class", "nav-link active");
                    divEntradas.Attributes.Add("class", "tab-pane active show");
                    break;
            }
        }

        public string GetImage(string file)
        {
            if (string.IsNullOrEmpty(file))
               file = "C:\\Storage\\Images\\sin_imagen.jpg";

            var path = Server.MapPath("~/Images/LocalStorage/");
            var exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            var fileInfo = new FileInfo(file);
            
            if (!File.Exists(fileInfo.FullName)) return null;

            var pathToCreateFile = Server.MapPath($"~/Images/LocalStorage/{fileInfo.Name}");

            File.Copy(file, pathToCreateFile, true);
            return $"../../Images/LocalStorage/{fileInfo.Name}";
        }

        protected void listaArticulosPedido_OnItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || 
                e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var hdnIdEstado = (HiddenField)e.Item.FindControl("hdnIdEstado");

                if (btnCerrarCuenta.Visible)
                {
                    if (!hdnIdEstado.Value.Equals("1"))
                    {
                        var btnEliminarArticulo = (LinkButton)e.Item.FindControl("btnEliminarArticulo");
                        btnEliminarArticulo.Visible = false;
                    }
                }

                if (!btnCerrarCuenta.Visible && !btnHacerPedido.Visible)
                {
                    var btnEliminarArticulo = (LinkButton)e.Item.FindControl("btnEliminarArticulo");
                    btnEliminarArticulo.Visible = false;
                }
            }
        }

        protected void txtMontoTarjeta_TextChanged(object sender, EventArgs e)
        {
            
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            int total = pedido.Total;
            int montoTarjeta = Convert.ToInt32(txtMontoTarjeta.Text);
            if(montoTarjeta > total)
            {
                return;
            }
            int montoEfectivo = 0;
            if (txtMontoEfectivo.Text != string.Empty)
            {
                montoEfectivo = Convert.ToInt32(txtMontoEfectivo.Text);
            }
            if ((montoTarjeta + montoEfectivo) == total)
            {
                return;
            }
            montoEfectivo = total - montoTarjeta;
            txtMontoEfectivo.Text = montoEfectivo.ToString();
            upModalPagoMixto.Update();
        }

        protected void txtMontoEfectivo_TextChanged(object sender, EventArgs e)
        {
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            int total = pedido.Total;
            int montoEfectivo = Convert.ToInt32(txtMontoEfectivo.Text);
            if (montoEfectivo > total)
            {
                return;
            }
            int montoTarjeta = 0;
            if (txtMontoTarjeta.Text != string.Empty)
            {
                montoTarjeta = Convert.ToInt32(txtMontoTarjeta.Text);
            }
            if ((montoTarjeta + montoEfectivo) == total)
            {
                return;
            }
            montoTarjeta = total - montoEfectivo;
            txtMontoTarjeta.Text = montoTarjeta.ToString();
            upModalPagoMixto.Update();
        }
    }
}