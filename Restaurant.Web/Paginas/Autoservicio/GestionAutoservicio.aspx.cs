using Restaurant.Model.Clases;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Autoservicio
{
    public partial class GestionAutoservicio : System.Web.UI.Page
    {
        private ArticuloPedidoService _articuloPedidoService;
        protected void Page_Load(object sender, EventArgs e)
        {
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            _articuloPedidoService = new ArticuloPedidoService(token.access_token);
            List<ArticuloPedido> articulos = _articuloPedidoService.Obtener();
            List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)articulos.Where(x => x.IdPedido == pedido.Id);
            Session["articulosPedidos"] = articulosPedido;

            actualizarRepeater(listaArticulosPedido, articulosPedido, listaArticulosPedidoVacia);
            var totalPedido = articulosPedido.Sum(x => x.Total);
            lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();
            upModalPedido.Update();
        }

        protected void btnHacerPedido_Click(object sender, EventArgs e)
        {

        }

        protected void btnPagarCuenta_Click(object sender, EventArgs e)
        {

        }

        protected void btnEliminarArticulo_Click(object sender, RepeaterCommandEventArgs e)
        {
            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidos"];
            }
        }
        public void limpiarTabs()
        {
            tabMenu.Attributes.Add("class", "nav-link");
            tabMiOrden.Attributes.Add("class", "nav-link");
            divMenu.Attributes.Add("class", "tab-pane fade");
            divMiOrden.Attributes.Add("class", "tab-pane fade");
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
                    txtFechaInicioPedido.Text = pedido.FechaHoraInicio.ToShortTimeString();
                    txtFechaFinPedido.Text = pedido.FechaHoraFin.ToShortTimeString();
                    txtTotalPedido.Text = pedido.Total.ToString();
                    ddlEstadoPedido.SelectedValue = pedido.IdEstadoPedido.ToString();
                    ddlMesaPedido.SelectedValue = pedido.IdMesa.ToString();
                    ddlArticuloPedido.SelectedValue = "";
                    ddlPrecioArticuloPedido.SelectedValue = "";
                    txtCantidadArticuloPedido.Text = "";

                    //Buscar artículos del pedido
                    _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                    List<ArticuloPedido> articulos = _articuloPedidoService.Obtener();
                    List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)articulos.Where(x => x.IdPedido == pedido.Id);
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
                    articuloPedido.IdPedido = idPedido;
                    _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                    int idArticuloPedido = _articuloPedidoService.Guardar(articuloPedido);
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
            articuloPedido.IdEstadoArticuloPedido = 1;
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
    }
}