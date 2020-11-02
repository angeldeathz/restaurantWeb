using System;
using System.Collections.Generic;
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

        protected void Page_Load(object sender, EventArgs e)
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

                List<Pedido> pedidos = _pedidoService.Obtener();
                if (pedidos != null && pedidos.Count > 0)
                {
                    actualizarRepeater(listaPedidos, pedidos, listaPedidosVacia);
                }

                List<Articulo> articulos = _articuloService.Obtener();
                if (articulos != null && articulos.Count > 0)
                {
                    actualizarRepeater(listaArticulos, articulos, listaArticulosVacia);
                    actualizarDdlArticulos(articulos);
                }

                List<Mesa> mesas = _mesaService.Obtener();
                if (mesas != null && mesas.Count > 0)
                {
                    ddlMesaPedido.DataSource = mesas;
                    ddlMesaPedido.DataTextField = "Nombre";
                    ddlMesaPedido.DataValueField = "Id";
                    ddlMesaPedido.DataBind();
                    ddlMesaPedido.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlMesaPedido.SelectedIndex = 0;
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
            ddlMesaPedido.SelectedValue = "";
            ddlArticuloPedido.SelectedValue = "";
            ddlPrecioArticuloPedido.SelectedValue = "";
            txtCantidadArticuloPedido.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('show');", true);
            upModalPedido.Update();
            Session["articulosPedidos"] = new List<ArticuloPedido>();

            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnModalEditarPedido_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
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
                    ddlMesaPedido.SelectedValue = pedido.IdMesa.ToString();
                    ddlArticuloPedido.SelectedValue = "";
                    ddlPrecioArticuloPedido.SelectedValue = "";
                    txtCantidadArticuloPedido.Text = "";

                    //Buscar artículos del pedido
                    _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                    List<ArticuloPedido> articulos = _articuloPedidoService.Obtener();
                    List<ArticuloPedido> articulosPedido = articulos.Where(x => x.IdPedido == pedido.Id).ToList();
                    Session["articulosPedidos"] = articulosPedido;

                    actualizarRepeater(listaArticulosPedido, articulosPedido, listaArticulosPedidoVacia);
                    var totalPedido = articulosPedido.Sum(x => x.Total);
                    lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
                    txtTotalPedido.Text = totalPedido.ToString();
                    upArticulosPedido.Update();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('show');", true);
                    upModalPedido.Update();
                }
            }
            Session["articulosPedidos"] = new List<ArticuloPedido>();

            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnCrearPedido_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            Pedido pedido = new Pedido();
            pedido.FechaHoraInicio = Convert.ToDateTime(txtFechaInicioPedido.Text);
            pedido.FechaHoraFin = Convert.ToDateTime(txtFechaFinPedido.Text);
            pedido.Total = int.Parse(txtTotalPedido.Text);
            pedido.IdEstadoPedido = int.Parse(ddlEstadoPedido.SelectedValue);
            pedido.IdMesa = int.Parse(ddlMesaPedido.SelectedValue);

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            int idPedido = _pedidoService.Guardar(pedido);
            if (idPedido != 0)
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidos"];
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPedido", "alert('Pedido creado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "alert('Error al crear pedido');", true);
            }
        }

        protected void btnEditarPedido_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Pedido pedido = new Pedido();
            pedido.Id = int.Parse(txtIdPedido.Text);
            pedido.FechaHoraInicio = Convert.ToDateTime(txtFechaInicioPedido.Text);
            pedido.FechaHoraFin = Convert.ToDateTime(txtFechaFinPedido.Text);
            pedido.Total = int.Parse(txtTotalPedido.Text);
            pedido.IdEstadoPedido = int.Parse(ddlEstadoPedido.SelectedValue);
            pedido.IdMesa = int.Parse(ddlMesaPedido.SelectedValue);

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            bool editar = _pedidoService.Modificar(pedido, pedido.Id);
            if (editar)
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidos"];
                //SE DEBERÍAN ELIMINAR LOS articulosPedido que ya existen, asociados?
                foreach (ArticuloPedido articuloPedido in listaArticulos)
                {
                    articuloPedido.IdPedido = pedido.Id;
                    _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                    int idArticuloPedido = _articuloPedidoService.Guardar(articuloPedido);
                }
                List<Pedido> pedidos = _pedidoService.Obtener();
                if (pedidos != null && pedidos.Count > 0)
                {
                    actualizarRepeater(listaPedidos, pedidos, listaPedidosVacia);
                    upListaPedidos.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarPedido", "alert('Pedido editado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "alert('Error al editar pedido');", true);
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

            ArticuloPedido articuloPedido = new ArticuloPedido();
            Articulo articulo = new Articulo();
            articulo.Nombre = ddlArticuloPedido.SelectedItem.Text;
            articuloPedido.IdArticulo = int.Parse(ddlArticuloPedido.SelectedValue);
            articuloPedido.Precio = int.Parse(ddlPrecioArticuloPedido.SelectedItem.Text);
            articuloPedido.Cantidad = int.Parse(txtCantidadArticuloPedido.Text);
            articuloPedido.Total = articuloPedido.Precio * articuloPedido.Cantidad;
            articuloPedido.IdEstadoArticuloPedido = EstadoArticuloPedido.recibido;
            articuloPedido.Articulo = articulo;

            List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidos"];
            var articuloExiste = listaArticulos.FirstOrDefault(a => a.IdArticulo == articuloPedido.IdArticulo);
            if (articuloExiste != null)
            {
                articuloExiste.Cantidad = articuloExiste.Cantidad + articuloPedido.Cantidad;
                articuloExiste.Total = articuloExiste.Precio * articuloExiste.Cantidad;
            }
            else
            {
                listaArticulos.Add(articuloPedido);
            }
            Session["articulosPedidos"] = listaArticulos;
            actualizarRepeater(listaArticulosPedido, listaArticulos, listaArticulosPedidoVacia);
            var totalPedido = listaArticulos.Sum(x => x.Total);
            lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();

            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnEliminarArticuloPedido_Click(object sender, RepeaterCommandEventArgs e)
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
                    lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
                }
                txtTotalPedido.Text = totalPedido.ToString();
                upArticulosPedido.Update();
            }
            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane active show");
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
                        ArticuloConsumoDirecto articuloConsumoDirecto = articulosConsumoDirecto.First(x => x.IdArticulo == articulo.Id);
                        if (articuloConsumoDirecto != null)
                        {
                            modalEditarArticuloConsumoDirecto(articulo, articuloConsumoDirecto);
                        }
                        else if (platos != null)
                        {
                            Plato plato = platos.First(x => x.IdArticulo == articulo.Id);
                            if (plato != null)
                            {
                                modalEditarPlato(articulo, plato);
                            }
                        }
                    }
                    else if (platos != null)
                    {
                        Plato plato = platos.First(x => x.IdArticulo == articulo.Id);
                        if (plato != null)
                        {
                            modalEditarPlato(articulo, plato);
                        }
                    }
                }
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
            ddlInsumoArticulo.SelectedValue = articuloConsumoDirecto.Id.ToString();
            ddlInsumoArticulo.Enabled = false;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
            upModalArticulo.Update();

            limpiarTabs();
            tabArticulos.Attributes.Add("class", "nav-link active");
            divArticulos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnCrearArticulo_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            Articulo articulo = new Articulo();
            articulo.Nombre = txtNombreArticulo.Text;
            articulo.Descripcion = txtDescripcionArticulo.Text;
            articulo.Precio = int.Parse(txtPrecioArticulo.Text);
            articulo.IdTipoConsumo = int.Parse(ddlTipoConsumoArticulo.SelectedValue);
            articulo.IdEstadoArticulo = int.Parse(ddlEstadoArticulo.SelectedValue);

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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearArticulo", "alert('Articulo creado');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "alert('Error al crear articulo');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "alert('Error al crear articulo');", true);
            }
        }

        protected void btnEditarArticulo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Articulo articulo = new Articulo();
            articulo.Id = int.Parse(txtIdArticulo.Text);
            articulo.Nombre = txtNombreArticulo.Text;
            articulo.Descripcion = txtDescripcionArticulo.Text;
            articulo.Precio = int.Parse(txtPrecioArticulo.Text);
            articulo.IdTipoConsumo = int.Parse(ddlTipoConsumoArticulo.Text);
            articulo.IdEstadoArticulo = int.Parse(ddlEstadoArticulo.Text);

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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarArticulo", "alert('Articulo editado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "alert('Error al editar articulo');", true);
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

            limpiarTabs();
            tabArticulos.Attributes.Add("class", "nav-link active");
            divArticulos.Attributes.Add("class", "tab-pane active show");
        }

        protected void modalEditarPlato(Articulo articulo, Plato plato)
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
            txtMinutosPreparacion.Text = plato.MinutosPreparacion.ToString();
            ddlTipoPreparacion.SelectedValue = plato.TipoPreparacion.Nombre;

            ddlIngredientePlato.SelectedValue = "";
            txtCantidadIngredientePlato.Text = "";

            //Buscar ingredientes del plato
            Token token = (Token)Session["token"];
            _ingredientePlatoService = new IngredientePlatoService(token.access_token);
            List<IngredientePlato> ingredientes = _ingredientePlatoService.Obtener();
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

        protected void btnCrearPlato_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Articulo articulo = new Articulo();
            articulo.Nombre = txtNombrePlato.Text;
            articulo.Descripcion = txtDescripcionPlato.Text;
            articulo.Precio = int.Parse(txtPrecioPlato.Text);
            articulo.IdTipoConsumo = int.Parse(ddlTipoConsumoPlato.SelectedValue);
            articulo.IdEstadoArticulo = int.Parse(ddlEstadoPlato.SelectedValue);

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
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPlato", "alert('Plato creado');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "$('#modalPlato').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "alert('Error al crear pedido');", true);
                }
            }
        }
        protected void btnEditarPlato_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Articulo articulo = new Articulo();
            articulo.Id = int.Parse(txtIdArticuloPlato.Text);
            articulo.Nombre = txtNombrePlato.Text;
            articulo.Descripcion = txtDescripcionPlato.Text;
            articulo.Precio = int.Parse(txtPrecioPlato.Text);
            articulo.IdTipoConsumo = int.Parse(ddlTipoConsumoPlato.SelectedValue);
            articulo.IdEstadoArticulo = int.Parse(ddlEstadoPlato.SelectedValue);

            Token token = (Token)Session["token"];
            _articuloService = new ArticuloService(token.access_token);
            bool editar = _articuloService.Modificar(articulo, articulo.Id);

            if (editar)
            {
                Plato plato = new Plato();
                plato.Id = int.Parse(txtIdPlato.Text);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarPlato", "alert('Plato editado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "$('#modalPlato').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPlato", "alert('Error al editar pedido');", true);
            }
        }
        protected void btnAgregarIngredientePlato_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            IngredientePlato ingredientePlato = new IngredientePlato();
            Insumo insumo = new Insumo();
            insumo.Nombre = ddlIngredientePlato.SelectedItem.Text;
            ingredientePlato.IdInsumo = int.Parse(ddlIngredientePlato.SelectedValue);
            ingredientePlato.CantidadInsumo = int.Parse(txtCantidadIngredientePlato.Text);
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
    }
}
