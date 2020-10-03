using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Administrador
{
    public partial class GestionCocina : System.Web.UI.Page
    {
        private ArticuloService _articuloService;
        private PedidoService _pedidoService;
        private EstadoPedidoService _estadoPedidoService;
        private EstadoArticuloService _estadoArticuloService;
        private TipoConsumoService _tipoConsumoService;
        private MesaService _mesaService;

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

                List<Pedido> pedidos = _pedidoService.Obtener();
                if (pedidos != null && pedidos.Count > 0)
                {
                    listaPedidos.DataSource = pedidos;
                    listaPedidos.DataBind();
                }

                List<Articulo> articulos = _articuloService.Obtener();
                if (articulos != null && articulos.Count > 0)
                {
                    listaArticulos.DataSource = articulos;
                    listaArticulos.DataBind();

                    ddlArticuloPedido.DataSource = articulos;
                    ddlArticuloPedido.DataTextField = "Nombre";
                    ddlArticuloPedido.DataValueField = "Id";
                    ddlArticuloPedido.DataBind();
                    ddlArticuloPedido.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlArticuloPedido.SelectedIndex = 0;
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
                }
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
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

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('show');", true);
            upModalPedido.Update();

            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane fade active show");
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
                    txtFechaInicioPedido.Text = pedido.FechaInicio.ToShortTimeString();
                    txtFechaFinPedido.Text = pedido.FechaTermino.ToShortTimeString();
                    txtTotalPedido.Text = pedido.Total.ToString();
                    ddlEstadoPedido.SelectedValue = pedido.IdEstadoPedido.ToString();
                    ddlMesaPedido.SelectedValue = pedido.IdMesa.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('show');", true);
                    upModalPedido.Update();
                }
            }
            limpiarTabs();
            tabPedidos.Attributes.Add("class", "nav-link active");
            divPedidos.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnCrearPedido_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            Pedido pedido = new Pedido();
            pedido.FechaInicio = Convert.ToDateTime(txtFechaInicioPedido.Text);
            pedido.FechaTermino = Convert.ToDateTime(txtFechaFinPedido.Text);
            pedido.Total = int.Parse(txtTotalPedido.Text);
            pedido.IdEstadoPedido = int.Parse(ddlEstadoPedido.SelectedValue);
            pedido.IdMesa = int.Parse(ddlMesaPedido.SelectedValue);

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            int idPedido = _pedidoService.Guardar(pedido);
            if (idPedido != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "alert('Pedido creado');", true);
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
            pedido.FechaInicio = Convert.ToDateTime(txtFechaInicioPedido.Text);
            pedido.FechaInicio = Convert.ToDateTime(txtFechaFinPedido.Text);
            pedido.Total = int.Parse(txtTotalPedido.Text);
            pedido.IdEstadoPedido = int.Parse(ddlEstadoPedido.SelectedValue);
            pedido.IdMesa = int.Parse(ddlMesaPedido.SelectedValue);

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            bool editar = _pedidoService.Modificar(pedido);
            if (editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "$('#modalPedido').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "alert('Pedido editado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "alert('Error al editar pedido');", true);
            }
        }
        
        protected void btnModalCrearArticulo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalArticulo.Text = "Crear Artículo";
            btnCrearArticulo.Visible = true;
            btnEditarArticulo.Visible = false;
            txtIdArticulo.Text = "";
            txtNombreArticulo.Text = "";
            txtDescripcionArticulo.Text = "";
            txtPrecioArticulo.Text = "";
            ddlTipoConsumoArticulo.SelectedValue = "";
            ddlEstadoArticulo.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
            upModalArticulo.Update();

            limpiarTabs();
            tabArticulos.Attributes.Add("class", "nav-link active");
            divArticulos.Attributes.Add("class", "tab-pane fade active show");
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
                    tituloModalArticulo.Text = "Editar Articulo";
                    btnCrearArticulo.Visible = false;
                    btnEditarArticulo.Visible = true;
                    txtIdArticulo.Text = articulo.Id.ToString();
                    txtNombreArticulo.Text = articulo.Nombre;
                    txtDescripcionArticulo.Text = articulo.Descripcion;
                    txtPrecioArticulo.Text = articulo.Precio.ToString();
                    ddlTipoConsumoArticulo.SelectedValue = articulo.IdTipoConsumo.ToString();
                    ddlEstadoArticulo.SelectedValue = articulo.IdEstadoArticulo.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
                    upModalArticulo.Update();
                }
            }
            limpiarTabs();
            tabArticulos.Attributes.Add("class", "nav-link active");
            divArticulos.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnCrearArticulo_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            Articulo articulo = new Articulo();
            articulo.Id = int.Parse(txtIdArticulo.Text);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "alert('Articulo creado');", true);
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
            bool editar = _articuloService.Modificar(articulo);
            if (editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "alert('Articulo editado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "alert('Error al editar articulo');", true);
            }
        }

        protected void btnAgregarArticuloPedido_Click(object sender, EventArgs e)
        {

        }
        protected void btnEliminarArticuloPedido_Click(object sender, EventArgs e)
        {

        }        
    }
}
