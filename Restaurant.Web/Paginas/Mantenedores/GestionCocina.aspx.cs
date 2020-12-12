using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;

namespace Restaurant.Web.Paginas.Mantenedores
{
    public partial class GestionCocina : System.Web.UI.Page
    {
        private ArticuloService _articuloService;
        private PedidoService _pedidoService;
        private EstadoPedidoService _estadoPedidoService;
        private EstadoArticuloService _estadoArticuloService;
        private TipoConsumoService _tipoConsumoService;
        private MesaService _mesaService;
        private ArticuloPedidoService _articuloPedidoService;
        private InsumoService _insumoService;
        private PlatoService _platoService;
        private ArticuloConsumoDirectoService _articuloConsumoDirectoService;
        private IngredientePlatoService _ingredientePlatoService;
        private TipoPreparacionService _tipoPreparacionService;
        private ReservaService _reservaService;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ValidarSesion();

                    Token token = (Token)Session["token"];
                    _articuloService = new ArticuloService(token.access_token);
                    _pedidoService = new PedidoService(token.access_token);
                    _estadoPedidoService = new EstadoPedidoService(token.access_token);
                    _estadoArticuloService = new EstadoArticuloService(token.access_token);
                    _mesaService = new MesaService(token.access_token);
                    _tipoConsumoService = new TipoConsumoService(token.access_token);
                    _insumoService = new InsumoService(token.access_token);
                    _tipoPreparacionService = new TipoPreparacionService(token.access_token);
                    _reservaService = new ReservaService(token.access_token);

                    List<Pedido> pedidos = _pedidoService.Obtener();
                    if (pedidos != null && pedidos.Count > 0)
                    {
                        var pedidosOrdenados = pedidos.OrderByDescending(x => x.Id).ToList();
                        actualizarRepeater(listaPedidos, pedidosOrdenados, listaPedidosVacia);
                    }

                    List<Articulo> articulos = _articuloService.Obtener();
                    if (articulos != null && articulos.Count > 0)
                    {
                        actualizarRepeater(listaArticulos, articulos, listaArticulosVacia);
                        actualizarDdlArticulos(articulos);
                    }

                    List<Reserva> reservas = _reservaService.Obtener();
                    if (reservas != null && reservas.Count > 0)
                    {
                        var ds = from r in reservas
                                 select new
                                 {
                                     r.Id,
                                     r.Cliente.Persona.Nombre,
                                     r.Cliente.Persona.Apellido,
                                     NombreCliente = String.Format("{0} {1}", r.Cliente.Persona.Nombre, r.Cliente.Persona.Apellido)
                                 };
                        ddlReservaPedido.DataSource = ds;
                        ddlReservaPedido.DataTextField = "NombreCliente";
                        ddlReservaPedido.DataValueField = "Id";
                        ddlReservaPedido.DataBind();
                        ddlReservaPedido.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlReservaPedido.SelectedIndex = 0;
                    }

                    List<EstadoPedido> estadosPedido = _estadoPedidoService.Obtener();
                    if (estadosPedido != null && estadosPedido.Count > 0)
                    {
                        ddlEstadoPedido.DataSource = estadosPedido;
                        ddlEstadoPedido.DataTextField = "Nombre";
                        ddlEstadoPedido.DataValueField = "Id";
                        ddlEstadoPedido.DataBind();
                        ddlEstadoPedido.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlEstadoPedido.SelectedIndex = 0;
                    }

                    List<TipoConsumo> tiposConsumo = _tipoConsumoService.Obtener();
                    if (tiposConsumo != null && tiposConsumo.Count > 0)
                    {
                        ddlTipoConsumoArticulo.DataSource = tiposConsumo;
                        ddlTipoConsumoArticulo.DataTextField = "Nombre";
                        ddlTipoConsumoArticulo.DataValueField = "Id";
                        ddlTipoConsumoArticulo.DataBind();
                        ddlTipoConsumoArticulo.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlTipoConsumoArticulo.SelectedIndex = 0;

                        ddlTipoConsumoPlato.DataSource = tiposConsumo;
                        ddlTipoConsumoPlato.DataTextField = "Nombre";
                        ddlTipoConsumoPlato.DataValueField = "Id";
                        ddlTipoConsumoPlato.DataBind();
                        ddlTipoConsumoPlato.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlTipoConsumoPlato.SelectedIndex = 0;
                    }

                    List<TipoPreparacion> tiposPreparacion = _tipoPreparacionService.Obtener();
                    if (tiposPreparacion != null && tiposPreparacion.Count > 0)
                    {
                        ddlTipoPreparacion.DataSource = tiposPreparacion;
                        ddlTipoPreparacion.DataTextField = "Nombre";
                        ddlTipoPreparacion.DataValueField = "Id";
                        ddlTipoPreparacion.DataBind();
                        ddlTipoPreparacion.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlTipoPreparacion.SelectedIndex = 0;
                    }

                    List<EstadoArticulo> estadoArticulo = _estadoArticuloService.Obtener();
                    if (estadoArticulo != null && estadoArticulo.Count > 0)
                    {
                        ddlEstadoArticulo.DataSource = estadoArticulo;
                        ddlEstadoArticulo.DataTextField = "Nombre";
                        ddlEstadoArticulo.DataValueField = "Id";
                        ddlEstadoArticulo.DataBind();
                        ddlEstadoArticulo.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlEstadoArticulo.SelectedIndex = 0;

                        ddlEstadoPlato.DataSource = estadoArticulo;
                        ddlEstadoPlato.DataTextField = "Nombre";
                        ddlEstadoPlato.DataValueField = "Id";
                        ddlEstadoPlato.DataBind();
                        ddlEstadoPlato.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlEstadoPlato.SelectedIndex = 0;
                    }


                    List<Insumo> insumos = _insumoService.Obtener();
                    if (insumos != null && insumos.Count > 0)
                    {
                        insumos = insumos.OrderBy(x => x.Nombre).ToList();

                        ddlInsumoArticulo.DataSource = insumos;
                        ddlInsumoArticulo.DataTextField = "Nombre";
                        ddlInsumoArticulo.DataValueField = "Id";
                        ddlInsumoArticulo.DataBind();
                        ddlInsumoArticulo.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlInsumoArticulo.SelectedIndex = 0;

                        ddlIngredientePlato.DataSource = insumos;
                        ddlIngredientePlato.DataTextField = "Nombre";
                        ddlIngredientePlato.DataValueField = "Id";
                        ddlIngredientePlato.DataBind();
                        ddlIngredientePlato.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlIngredientePlato.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
            }
            Usuario usuario = (Usuario)Session["usuario"];

            if (!new int[] { TipoUsuario.administrador, TipoUsuario.cocina, TipoUsuario.garzon }.Contains(usuario.IdTipoUsuario))
            {
                Response.Redirect("../Mantenedores/Inicio.aspx");
            }
        }
        public void limpiarTabs()
        {
            tabPedidos.Attributes.Add("class", "nav-link");
            tabArticulos.Attributes.Add("class", "nav-link");
            divPedidos.Attributes.Add("class", "tab-pane fade");
            divArticulos.Attributes.Add("class", "tab-pane fade");
        }
        protected void btnModalCrearPedido_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalPedido.Text = "Crear Pedido";
            btnCrearPedido.Visible = true;
            btnEditarPedido.Visible = false;
            txtFechaInicioPedido.Text = "";
            txtFechaFinPedido.Text = "";
            txtTotalPedido.Text = "";
            ddlEstadoPedido.SelectedValue = "";
            ddlReservaPedido.SelectedValue = "";
            ddlArticuloPedido.SelectedValue = "";
            ddlPrecioArticuloPedido.SelectedValue = "";
            txtCantidadArticuloPedido.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('show');", true);
            Session["articulosPedidos"] = new List<ArticuloPedido>();
            upModalPedido.Update();
            actualizarRepeater(listaArticulosPedido, new List<ArticuloPedido>(), listaArticulosPedidoVacia);
            upArticulosPedido.Update();

            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnModalEditarPedido_Click(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                ValidarSesion();
                Session["articulosPedidos"] = new List<ArticuloPedido>();

                int idPedido;
                if (int.TryParse((string)e.CommandArgument, out idPedido))
                {
                    Token token = (Token)Session["token"];
                    _pedidoService = new PedidoService(token.access_token);
                    Pedido pedido = _pedidoService.Obtener(idPedido);
                    if (pedido != null)
                    {
                        tituloModalPedido.Text = "Editar Pedido";
                        btnCrearPedido.Visible = false;
                        btnEditarPedido.Visible = true;
                        txtIdPedido.Text = pedido.Id.ToString();
                        txtFechaInicioPedido.Text = pedido.FechaHoraInicio.ToString("yyyy-MM-ddTHH:mm");
                        txtFechaFinPedido.Text = pedido.FechaHoraFin.ToString("yyyy-MM-ddTHH:mm");
                        txtTotalPedido.Text = pedido.Total.ToString();
                        ddlEstadoPedido.SelectedValue = pedido.IdEstadoPedido.ToString();
                        ddlReservaPedido.SelectedValue = pedido.IdReserva.ToString();
                        ddlArticuloPedido.SelectedValue = "";
                        ddlPrecioArticuloPedido.SelectedValue = "";
                        txtCantidadArticuloPedido.Text = "";

                        //Buscar artículos del pedido
                        _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                        List<ArticuloPedido> articulos = _articuloPedidoService.Obtener();
                        if (articulos == null || articulos.Count == 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "Swal.fire('Error al editar pedido', '', 'error');", true);
                        }
                        List<ArticuloPedido> articulosPedido = articulos.Where(x => x.IdPedido == pedido.Id).ToList();
                        Session["articulosPedidos"] = articulosPedido;

                        actualizarRepeater(listaArticulosPedido, articulosPedido, listaArticulosPedidoVacia);
                        var totalPedido = articulosPedido.Sum(x => x.Total);
                        lblTotalPedido.Text = "Total: " + string.Format("${0:N0}", totalPedido) + "-";
                        txtTotalPedido.Text = totalPedido.ToString();
                        upArticulosPedido.Update();

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('show');", true);
                        upModalPedido.Update();
                    }
                }

                limpiarTabs();
                tabPedidos.Attributes.Add("class", "nav-link active");
                divPedidos.Attributes.Add("class", "tab-pane active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnCrearPedido_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("ValidacionPedido");
            if(!Page.IsValid)
            {
                upModalPedido.Update();
                return;
            }
            try
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidos"];
                if (listaArticulos == null || listaArticulos.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "Swal.fire('Debe agregar al menos un artículo', '', 'error');", true);
                    return;
                }
                Pedido pedido = new Pedido();
                pedido.FechaHoraInicio = Convert.ToDateTime(txtFechaInicioPedido.Text);
                pedido.FechaHoraFin = Convert.ToDateTime(txtFechaFinPedido.Text);
                pedido.Total = int.Parse(txtTotalPedido.Text);
                pedido.IdEstadoPedido = int.Parse(ddlEstadoPedido.SelectedValue);
                pedido.IdReserva = int.Parse(ddlReservaPedido.SelectedValue);

                Token token = (Token)Session["token"];
                _pedidoService = new PedidoService(token.access_token);
                int idPedido = _pedidoService.Guardar(pedido);
                if (idPedido != 0)
                {
                    foreach (ArticuloPedido articuloPedido in listaArticulos)
                    {
                        ArticuloPedido articuloPedidoInsert = new ArticuloPedido();
                        articuloPedidoInsert.Cantidad = articuloPedido.Cantidad;
                        articuloPedidoInsert.Precio = articuloPedido.Precio;
                        articuloPedidoInsert.Total = articuloPedido.Total;
                        articuloPedidoInsert.IdArticulo = articuloPedido.IdArticulo;
                        articuloPedidoInsert.IdEstadoArticuloPedido = articuloPedido.IdEstadoArticuloPedido;
                        articuloPedidoInsert.IdPedido = idPedido;
                        _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                        int idArticuloPedido = _articuloPedidoService.Guardar(articuloPedidoInsert);
                    }
                    List<Pedido> pedidos = _pedidoService.Obtener();
                    if (pedidos != null && pedidos.Count > 0)
                    {
                        actualizarRepeater(listaPedidos, pedidos, listaPedidosVacia);
                        upListaPedidos.Update();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPedido", "Swal.fire('Pedido creado', '', 'success');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "Swal.fire('Error al crear pedido', '', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnEditarPedido_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("ValidacionPedido");
            if (!Page.IsValid)
            {
                upModalPedido.Update();
                return;
            }
            try
            {
                Pedido pedido = new Pedido();
                pedido.Id = int.Parse(txtIdPedido.Text);
                pedido.FechaHoraInicio = Convert.ToDateTime(txtFechaInicioPedido.Text);
                pedido.FechaHoraFin = Convert.ToDateTime(txtFechaFinPedido.Text);
                pedido.Total = int.Parse(txtTotalPedido.Text);
                pedido.IdEstadoPedido = int.Parse(ddlEstadoPedido.SelectedValue);
                pedido.IdReserva = int.Parse(ddlReservaPedido.SelectedValue);

                Token token = (Token)Session["token"];
                _pedidoService = new PedidoService(token.access_token);
                bool editar = _pedidoService.Modificar(pedido, pedido.Id);
                if (editar)
                {
                    _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                    //ELIMINAR LOS articulosPedido que ya existen
                    List<ArticuloPedido> articulosActuales = _articuloPedidoService.Obtener();
                    if (articulosActuales != null && articulosActuales.Count > 0)
                    {
                        articulosActuales = articulosActuales.Where(x => x.IdPedido == pedido.Id).ToList();
                        foreach (ArticuloPedido articuloPedido in articulosActuales)
                        {
                            _articuloPedidoService.Eliminar(articuloPedido.Id);
                        }
                    }

                    List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidos"];
                    foreach (ArticuloPedido articuloPedido in listaArticulos)
                    {
                        ArticuloPedido nuevoArticulo = new ArticuloPedido();
                        nuevoArticulo.IdPedido = pedido.Id;
                        nuevoArticulo.Cantidad = articuloPedido.Cantidad;
                        nuevoArticulo.Comentarios = articuloPedido.Comentarios;
                        nuevoArticulo.IdArticulo = articuloPedido.IdArticulo;
                        nuevoArticulo.IdEstadoArticuloPedido = articuloPedido.IdEstadoArticuloPedido != 0 ? articuloPedido.IdEstadoArticuloPedido : (articuloPedido.EstadoArticuloPedido != null ? articuloPedido.EstadoArticuloPedido.Id : EstadoArticuloPedido.pendiente );
                        nuevoArticulo.Precio = articuloPedido.Precio;
                        nuevoArticulo.Total = articuloPedido.Total;
                        int idArticuloPedido = _articuloPedidoService.Guardar(nuevoArticulo);
                    }
                    List<Pedido> pedidos = _pedidoService.Obtener();
                    if (pedidos != null && pedidos.Count > 0)
                    {
                        actualizarRepeater(listaPedidos, pedidos, listaPedidosVacia);
                        upListaPedidos.Update();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarPedido", "Swal.fire('Pedido editado', '', 'success');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "Swal.fire('Error al editar pedido', '', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true); ;
                return;
            }
        }
        protected void ddlArticuloPedido_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idArticulo = ddlArticuloPedido.SelectedValue;
            ddlPrecioArticuloPedido.SelectedValue = idArticulo;
            upPrecio.Update();
            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnAgregarArticuloPedido_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("ValidacionArticuloPedido");
            if (!Page.IsValid)
            {
                upModalPedido.Update();
                return;
            }
            ArticuloPedido articuloPedido = new ArticuloPedido();
            Articulo articulo = new Articulo();
            articulo.Nombre = ddlArticuloPedido.SelectedItem.Text;
            articuloPedido.IdArticulo = int.Parse(ddlArticuloPedido.SelectedValue);
            articuloPedido.Precio = int.Parse(ddlPrecioArticuloPedido.SelectedItem.Text);
            articuloPedido.Cantidad = int.Parse(txtCantidadArticuloPedido.Text);
            articuloPedido.Total = articuloPedido.Precio * articuloPedido.Cantidad;
            articuloPedido.IdEstadoArticuloPedido = EstadoArticuloPedido.pendiente;
            articuloPedido.Articulo = articulo;

            List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidos"];
            listaArticulos.Add(articuloPedido);
            Session["articulosPedidos"] = listaArticulos;

            actualizarRepeater(listaArticulosPedido, listaArticulos, listaArticulosPedidoVacia);
            var totalPedido = listaArticulos.Sum(x => x.Total);
            lblTotalPedido.Text = "Total: " + string.Format("${0:N0}", totalPedido) + "-";
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();

            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnEliminarArticuloPedido_Click(object sender, RepeaterCommandEventArgs e)
        {
            try
            {
                ValidarSesion();
                int idArticulo;
                if (int.TryParse((string)e.CommandArgument, out idArticulo))
                {
                    List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidos"];
                    var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                    if (articuloEliminar != null)
                    {
                        listaArticulos.Remove(articuloEliminar);
                    }
                    Session["articulosPedidos"] = listaArticulos;
                    actualizarRepeater(listaArticulosPedido, listaArticulos, listaArticulosPedidoVacia);
                    var totalPedido = listaArticulos.Sum(x => x.Total);
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
                limpiarTabs();
                tabPedidos.Attributes.Add("class", "nav-link active");
                divPedidos.Attributes.Add("class", "tab-pane active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnModalCrearArticulo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalArticulo.Text = "Crear Artículo de consumo directo";
            btnCrearArticulo.Visible = true;
            btnEditarArticulo.Visible = false;
            txtIdArticulo.Text = "";
            txtNombreArticulo.Text = "";
            txtDescripcionArticulo.Text = "";
            txtPrecioArticulo.Text = "";
            ddlTipoConsumoArticulo.SelectedValue = "";
            ddlEstadoArticulo.SelectedValue = "";
            ddlInsumoArticulo.SelectedValue = "";
            ddlInsumoArticulo.Enabled = true;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
            upModalArticulo.Update();

            limpiarTabs();
            tabArticulos.Attributes.Add("class", "nav-link active");
            divArticulos.Attributes.Add("class", "tab-pane active show");
        }
        protected void btnModalEditarArticulo_Click(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                ValidarSesion();
                int idArticulo;
                if (int.TryParse((string)e.CommandArgument, out idArticulo))
                {
                    Token token = (Token)Session["token"];
                    _articuloService = new ArticuloService(token.access_token);
                    Articulo articulo = _articuloService.Obtener(idArticulo);
                    if (articulo != null)
                    {
                        _articuloConsumoDirectoService = new ArticuloConsumoDirectoService(token.access_token);
                        _platoService = new PlatoService(token.access_token);

                        List<ArticuloConsumoDirecto> articulosConsumoDirecto = _articuloConsumoDirectoService.Obtener();
                        List<Plato> platos = _platoService.Obtener();

                        if (articulosConsumoDirecto != null)
                        {
                            ArticuloConsumoDirecto articuloConsumoDirecto = articulosConsumoDirecto.FirstOrDefault(x => x.IdArticulo == articulo.Id);
                            if (articuloConsumoDirecto != null)
                            {
                                modalEditarArticuloConsumoDirecto(articulo, articuloConsumoDirecto);
                            }
                            else if (platos != null)
                            {
                                Plato plato = platos.FirstOrDefault(x => x.IdArticulo == articulo.Id);
                                if (plato != null)
                                {
                                    modalEditarPlato(articulo, plato);
                                }
                            }
                        }
                        else if (platos != null)
                        {
                            Plato plato = platos.FirstOrDefault(x => x.IdArticulo == articulo.Id);
                            if (plato != null)
                            {
                                modalEditarPlato(articulo, plato);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }
        protected void modalEditarArticuloConsumoDirecto(Articulo articulo, ArticuloConsumoDirecto articuloConsumoDirecto)
        {
            tituloModalArticulo.Text = "Editar Artículo de consumo directo";
            btnCrearArticulo.Visible = false;
            btnEditarArticulo.Visible = true;
            txtIdArticulo.Text = articulo.Id.ToString();
            txtNombreArticulo.Text = articulo.Nombre;
            txtDescripcionArticulo.Text = articulo.Descripcion;
            txtPrecioArticulo.Text = articulo.Precio.ToString();
            ddlTipoConsumoArticulo.SelectedValue = articulo.IdTipoConsumo.ToString();
            ddlEstadoArticulo.SelectedValue = articulo.IdEstadoArticulo.ToString();
            ddlInsumoArticulo.SelectedValue = articuloConsumoDirecto.IdInsumo.ToString();
            ddlInsumoArticulo.Enabled = false;
            hdnUrlImagen.Value = articulo.UrlImagen;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
            upModalArticulo.Update();

            limpiarTabs();
            tabArticulos.Attributes.Add("class", "nav-link active");
            divArticulos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnCrearArticulo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("ValidacionArticulo");
            if (!Page.IsValid)
            {
                upModalArticulo.Update();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
                return;
            }
            try
            {
                Articulo articulo = new Articulo();
                articulo.Nombre = txtNombreArticulo.Text;
                articulo.Descripcion = txtDescripcionArticulo.Text;
                articulo.Precio = int.Parse(txtPrecioArticulo.Text);
                articulo.IdTipoConsumo = int.Parse(ddlTipoConsumoArticulo.SelectedValue);
                articulo.IdEstadoArticulo = int.Parse(ddlEstadoArticulo.SelectedValue);
                articulo.UrlImagen = !string.IsNullOrEmpty(fileImagenArticulo.PostedFile.FileName)
                    ? UploadFileToStorage(fileImagenArticulo)
                    : string.Empty;

                Token token = (Token)Session["token"];
                _articuloService = new ArticuloService(token.access_token);
                int idArticulo = _articuloService.Guardar(articulo);

                if (idArticulo != 0)
                {
                    ArticuloConsumoDirecto articuloConsumoDirecto = new ArticuloConsumoDirecto();
                    articuloConsumoDirecto.IdInsumo = int.Parse(ddlInsumoArticulo.SelectedValue);
                    articuloConsumoDirecto.IdArticulo = idArticulo;

                    _articuloConsumoDirectoService = new ArticuloConsumoDirectoService(token.access_token);
                    int idArticuloConsumoDirecto = _articuloConsumoDirectoService.Guardar(articuloConsumoDirecto);

                    if (idArticuloConsumoDirecto != 0)
                    {
                        List<Articulo> articulos = _articuloService.Obtener();
                        if (articulos != null && articulos.Count > 0)
                        {
                            actualizarRepeater(listaArticulos, articulos, listaArticulosVacia);
                            upListaArticulos.Update();
                            actualizarDdlArticulos(articulos);
                        }

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearArticulo", "Swal.fire('Articulo creado', '', 'success');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "Swal.fire('Error al crear articulo', '', 'error');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "Swal.fire('Error al crear articulo', '', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnEditarArticulo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("ValidacionArticulo");
            if (!Page.IsValid)
            {
                upModalArticulo.Update();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
                return;
            }
            try
            {
                Articulo articulo = new Articulo();
                articulo.Id = int.Parse(txtIdArticulo.Text);
                articulo.Nombre = txtNombreArticulo.Text;
                articulo.Descripcion = txtDescripcionArticulo.Text;
                articulo.Precio = int.Parse(txtPrecioArticulo.Text);
                articulo.IdTipoConsumo = int.Parse(ddlTipoConsumoArticulo.Text);
                articulo.IdEstadoArticulo = int.Parse(ddlEstadoArticulo.Text);
                articulo.UrlImagen = !string.IsNullOrEmpty(fileImagenArticulo.PostedFile.FileName) ? UploadFileToStorage(fileImagenArticulo) : hdnUrlImagen.Value;

                Token token = (Token)Session["token"];
                _articuloService = new ArticuloService(token.access_token);
                bool editar = _articuloService.Modificar(articulo, articulo.Id);
                if (editar)
                {
                    List<Articulo> articulos = _articuloService.Obtener();
                    if (articulos != null && articulos.Count > 0)
                    {
                        actualizarRepeater(listaArticulos, articulos, listaArticulosVacia);
                        upListaArticulos.Update();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarArticulo", "Swal.fire('Articulo editado', '', 'success');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "Swal.fire('Error al editar articulo', '', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }
        protected void btnModalCrearPlato_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalPlato.Text = "Crear Plato";
            btnCrearPlato.Visible = true;
            btnEditarPlato.Visible = false;
            txtIdPlato.Text = "";
            txtNombrePlato.Text = "";
            txtDescripcionPlato.Text = "";
            txtPrecioPlato.Text = "";
            ddlTipoConsumoPlato.SelectedValue = "";
            ddlEstadoPlato.SelectedValue = "";
            txtMinutosPreparacion.Text = "";
            ddlTipoPreparacion.SelectedValue = "";
            ddlIngredientePlato.SelectedValue = "";
            txtCantidadIngredientePlato.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "$('#modalPlato').modal('show');", true);
            upModalPlato.Update();
            Session["ingredientesPlato"] = new List<IngredientePlato>();
            actualizarRepeater(listaIngredientesPlato, new List<IngredientePlato>(), listaIngredientesPlatoVacia);
            upIngredientesPlato.Update();
            limpiarTabs();
            tabArticulos.Attributes.Add("class", "nav-link active");
            divArticulos.Attributes.Add("class", "tab-pane active show");
        }

        protected void modalEditarPlato(Articulo articulo, Plato plato)
        {
            try
            {
                tituloModalPlato.Text = "Editar Plato";
                btnCrearPlato.Visible = false;
                btnEditarPlato.Visible = true;
                txtIdPlato.Text = plato.Id.ToString();
                txtIdArticuloPlato.Text = articulo.Id.ToString();
                txtNombrePlato.Text = articulo.Nombre;
                txtDescripcionPlato.Text = articulo.Descripcion;
                txtPrecioPlato.Text = articulo.Precio.ToString();
                ddlTipoConsumoPlato.SelectedValue = articulo.IdTipoConsumo.ToString();
                ddlEstadoPlato.SelectedValue = articulo.IdEstadoArticulo.ToString();
                txtMinutosPreparacion.Text = Convert.ToInt32(plato.MinutosPreparacion).ToString();
                ddlTipoPreparacion.SelectedValue = plato.TipoPreparacion.Id.ToString();
                hdnUrlImagenPlato.Value = plato.Articulo.UrlImagen;
                
                ddlIngredientePlato.SelectedValue = "";
                txtCantidadIngredientePlato.Text = "";

                //Buscar ingredientes del plato
                Token token = (Token)Session["token"];
                _ingredientePlatoService = new IngredientePlatoService(token.access_token);
                List<IngredientePlato> ingredientes = _ingredientePlatoService.Obtener();
                if (ingredientes == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "Swal.fire('Error al cargar información del articulo', '', 'error');", true);
                    return;
                }
                List<IngredientePlato> ingredientesPlato = ingredientes.Where(x => x.IdPlato == plato.Id).ToList();
                Session["ingredientesPlato"] = ingredientesPlato;

                actualizarRepeater(listaIngredientesPlato, ingredientesPlato, listaIngredientesPlatoVacia);
                upIngredientesPlato.Update();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "$('#modalPlato').modal('show');", true);
                upModalPlato.Update();

                Session["ingredientesPlato"] = new List<IngredientePlato>();

                limpiarTabs();
                tabArticulos.Attributes.Add("class", "nav-link active");
                divArticulos.Attributes.Add("class", "tab-pane active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnCrearPlato_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("ValidacionPlato");
            if (!Page.IsValid)
            {
                upModalPlato.Update();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "$('#modalPlato').modal('show');", true);
                return;
            }
            try
            {
                Articulo articulo = new Articulo();
                articulo.Nombre = txtNombrePlato.Text;
                articulo.Descripcion = txtDescripcionPlato.Text;
                articulo.Precio = int.Parse(txtPrecioPlato.Text);
                articulo.IdTipoConsumo = int.Parse(ddlTipoConsumoPlato.SelectedValue);
                articulo.IdEstadoArticulo = int.Parse(ddlEstadoPlato.SelectedValue);
                articulo.UrlImagen = !string.IsNullOrEmpty(fileImagenPlato.PostedFile.FileName)
                    ? UploadFileToStorage(fileImagenPlato)
                    : string.Empty;

                Token token = (Token)Session["token"];
                _articuloService = new ArticuloService(token.access_token);
                int idArticulo = _articuloService.Guardar(articulo);

                if (idArticulo != 0)
                {
                    Plato plato = new Plato();
                    plato.Nombre = articulo.Nombre;
                    plato.MinutosPreparacion = int.Parse(txtMinutosPreparacion.Text);
                    plato.IdTipoPreparacion = int.Parse(ddlTipoPreparacion.SelectedValue);
                    plato.IdArticulo = idArticulo;

                    _platoService = new PlatoService(token.access_token);
                    int idPlato = _platoService.Guardar(plato);
                    if (idPlato != 0)
                    {
                        List<IngredientePlato> listaIngredientes = (List<IngredientePlato>)Session["ingredientesPlato"];
                        foreach (IngredientePlato ingredientePlato in listaIngredientes)
                        {
                            ingredientePlato.Insumo = null;
                            ingredientePlato.IdPlato = idPlato;

                            _ingredientePlatoService = new IngredientePlatoService(token.access_token);
                            int idIngredientePlato = _ingredientePlatoService.Guardar(ingredientePlato);
                        }
                        //Actualizar lista de artículos
                        List<Articulo> articulos = _articuloService.Obtener();
                        if (articulos != null && articulos.Count > 0)
                        {
                            actualizarRepeater(listaArticulos, articulos, listaArticulosVacia);
                            upListaArticulos.Update();
                        }
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPlato", "Swal.fire('Plato creado', '', 'success');", true);
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "$('#modalPlato').modal('hide');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "Swal.fire('Error al crear pedido', '', 'error');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }
        protected void btnEditarPlato_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("ValidacionPlato");
            if (!Page.IsValid)
            {
                upModalPlato.Update();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "$('#modalPlato').modal('show');", true);
                return;
            }
            try
            {
                Articulo articulo = new Articulo();
                articulo.Id = int.Parse(txtIdArticuloPlato.Text);
                articulo.Nombre = txtNombrePlato.Text;
                articulo.Descripcion = txtDescripcionPlato.Text;
                articulo.Precio = int.Parse(txtPrecioPlato.Text);
                articulo.IdTipoConsumo = int.Parse(ddlTipoConsumoPlato.SelectedValue);
                articulo.IdEstadoArticulo = int.Parse(ddlEstadoPlato.SelectedValue);
                articulo.UrlImagen = !string.IsNullOrEmpty(fileImagenPlato.PostedFile.FileName)
                    ? UploadFileToStorage(fileImagenPlato)
                    : hdnUrlImagenPlato.Value;

                Token token = (Token)Session["token"];
                _articuloService = new ArticuloService(token.access_token);
                bool editar = _articuloService.Modificar(articulo, articulo.Id);

                if (editar)
                {
                    Plato plato = new Plato();
                    plato.Id = int.Parse(txtIdPlato.Text);
                    plato.Nombre = txtNombrePlato.Text;
                    plato.MinutosPreparacion = int.Parse(txtMinutosPreparacion.Text);
                    plato.IdTipoPreparacion = int.Parse(ddlTipoPreparacion.SelectedValue);
                    plato.IdArticulo = articulo.Id;

                    _platoService = new PlatoService(token.access_token);
                    bool editarPlato = _platoService.Modificar(plato, plato.Id);

                    List<IngredientePlato> listaInsumos = (List<IngredientePlato>)Session["ingredientesPlato"];
                    //SE DEBERÍAN ELIMINAR LOS insumosPlato que ya existen, asociados?
                    foreach (IngredientePlato ingredientePlato in listaInsumos)
                    {
                        ingredientePlato.IdPlato = plato.Id;
                        _ingredientePlatoService = new IngredientePlatoService(token.access_token);
                        int idIngredientePlato = _ingredientePlatoService.Guardar(ingredientePlato);
                    }

                    //Actualizar lista de articulos
                    _articuloService = new ArticuloService(token.access_token);
                    List<Articulo> articulos = _articuloService.Obtener();
                    if (articulos != null && articulos.Count > 0)
                    {
                        actualizarRepeater(listaArticulos, articulos, listaArticulosVacia);
                        upListaArticulos.Update();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarPlato", "Swal.fire('Plato editado', '', 'success');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "$('#modalPlato').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "Swal.fire('Error al editar pedido', '', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }
        protected void btnAgregarIngredientePlato_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("ValidacionIngrediente");
            if (!Page.IsValid)
            {
                upModalPlato.Update();
                return;
            }
            try
            {
                IngredientePlato ingredientePlato = new IngredientePlato();
                Insumo insumo = new Insumo();
                insumo.Nombre = ddlIngredientePlato.SelectedItem.Text;
                ingredientePlato.IdInsumo = int.Parse(ddlIngredientePlato.SelectedValue);
                string cantidadInsumo = txtCantidadIngredientePlato.Text.Replace(".", ",");
                ingredientePlato.CantidadInsumo = Convert.ToDouble(cantidadInsumo);
                ingredientePlato.Insumo = insumo;

                List<IngredientePlato> listaInsumos = (List<IngredientePlato>)Session["ingredientesPlato"];
                var insumoExiste = listaInsumos.FirstOrDefault(a => a.IdInsumo == ingredientePlato.IdInsumo);
                if (insumoExiste != null)
                {
                    insumoExiste.CantidadInsumo = insumoExiste.CantidadInsumo + ingredientePlato.CantidadInsumo;
                }
                else
                {
                    listaInsumos.Add(ingredientePlato);
                }
                Session["ingredientesPlato"] = listaInsumos;
                actualizarRepeater(listaIngredientesPlato, listaInsumos, listaIngredientesPlatoVacia);
                upIngredientesPlato.Update();

                limpiarTabs();
                tabArticulos.Attributes.Add("class", "nav-link active");
                divArticulos.Attributes.Add("class", "tab-pane active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnEliminarIngredientePlato_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idInsumo;
            if (int.TryParse((string)e.CommandArgument, out idInsumo))
            {
                List<IngredientePlato> listaInsumos = (List<IngredientePlato>)Session["ingredientesPlato"];
                var insumoEliminar = listaInsumos.FirstOrDefault(a => a.IdInsumo == idInsumo);
                if (insumoEliminar != null)
                {
                    listaInsumos.Remove(insumoEliminar);
                }
                Session["ingredientesPlato"] = listaInsumos;
                actualizarRepeater(listaIngredientesPlato, listaInsumos, listaIngredientesPlatoVacia);
                upIngredientesPlato.Update();
            }
            limpiarTabs();
            tabArticulos.Attributes.Add("class", "nav-link active");
            divArticulos.Attributes.Add("class", "tab-pane active show");
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

        public void actualizarDdlArticulos(List<Articulo> articulos)
        {
            articulos = articulos.OrderBy(x => x.Nombre).ToList();

            ddlArticuloPedido.DataSource = articulos;
            ddlArticuloPedido.DataTextField = "Nombre";
            ddlArticuloPedido.DataValueField = "Id";
            ddlArticuloPedido.DataBind();
            ddlArticuloPedido.Items.Insert(0, new ListItem("Seleccionar", ""));
            ddlArticuloPedido.SelectedIndex = 0;

            ddlPrecioArticuloPedido.DataSource = articulos;
            ddlPrecioArticuloPedido.DataTextField = "Precio";
            ddlPrecioArticuloPedido.DataValueField = "Id";
            ddlPrecioArticuloPedido.DataBind();
            ddlPrecioArticuloPedido.Items.Insert(0, new ListItem("", ""));
            ddlPrecioArticuloPedido.SelectedIndex = 0;
        }

        private string UploadFileToStorage(FileUpload file)
        {
            var path = "C:\\Storage\\Images";
            var exists = Directory.Exists(path);

            if (!exists)
                Directory.CreateDirectory(path);

            var pathFile = $"{path}\\{file.FileName}";
            file.PostedFile.SaveAs(pathFile);

             return pathFile;
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
    }
}
