using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
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
        private PedidoService _pedidoService;
        private ArticuloPedidoService _articuloPedidoService;
        private ArticuloService _articuloService;
        protected void Page_Load(object sender, EventArgs e)
        {
            validarIngreso();
            Token token = (Token) Session["token"];
            Reserva reserva = (Reserva) Session["reservaCliente"];
            Pedido pedido = (Pedido) Session["pedidoCliente"];

            Session["articulosPedido"] = new List<ArticuloPedido>();
            _pedidoService = new PedidoService(token.access_token);

            btnHacerPedido.Visible = true;
            btnPagarCuenta.Visible = false;

            if (pedido == null)
            {
                List<Pedido> pedidos = _pedidoService.Obtener();
                if(pedidos != null && pedidos.Count > 0)
                {
                    Pedido pedidoCliente = pedidos.FirstOrDefault(x => x.IdEstadoPedido == EstadoPedido.enCurso
                                                                  && x.IdMesa == reserva.IdMesa
                                                                  && x.FechaHoraInicio.Date == DateTime.Now.Date);
                    if (pedidoCliente != null)
                    {
                        cargarPedido(token, pedidoCliente);
                    }
                }
            }

            _articuloService = new ArticuloService(token.access_token);
            List<Articulo> articulos = _articuloService.Obtener();
            List<Articulo> articulosDisponibles = articulos.Where(x => x.IdEstadoArticulo == EstadoArticulo.disponible).ToList();

            listaEntradas.DataSource = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.entradas);
            listaEntradas.DataBind();
            //listaPlatosFondo.DataSource = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.platosFondo);
            //listaPlatosFondo.DataBind();
            //listaEnsaladas.DataSource = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.ensaladas);
            //listaEnsaladas.DataBind();
            //listaPostres.DataSource = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.postres);
            //listaPostres.DataBind();
            //listaBebestibles.DataSource = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.bebestibles);
            //listaBebestibles.DataBind();
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
            Session["articulosPedido"] = articulosPedido;

            actualizarRepeater(listaArticulosPedido, articulosPedido, listaArticulosPedidoVacia);

            var totalPedido = articulosPedido.Sum(x => x.Total);
            lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();
            upModalPedido.Update();

            if(articulosPedido.Count > 0)
            {
                btnHacerPedido.Visible = false;
                btnPagarCuenta.Visible = true;
            }
        }
        protected void btnHacerPedido_Click(object sender, EventArgs e)
        {
            validarIngreso();

            Pedido pedido = new Pedido();
            pedido.FechaHoraInicio = DateTime.Now;
            pedido.FechaHoraFin = DateTime.Now;
            pedido.Total = int.Parse(txtTotalPedido.Text);
            pedido.IdEstadoPedido = EstadoPedido.enCurso;
            pedido.IdMesa = int.Parse(ddlMesaPedido.SelectedValue);

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            int idPedido = _pedidoService.Guardar(pedido);
            if (idPedido != 0)
            {
                Session["pedidoCliente"] = pedido;
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedido"];
                foreach (ArticuloPedido articuloPedido in listaArticulos)
                {
                    articuloPedido.IdPedido = idPedido;
                    _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                    int idArticuloPedido = _articuloPedidoService.Guardar(articuloPedido);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPedido", "alert('Pedido enviado a la cocina');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "alert('Error al hacer pedido');", true);
            }
        }

        protected void btnPagarCuenta_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedido"];
            pedido.IdEstadoPedido = EstadoPedido.pagado;
            var totalPedido = articulosPedido.Sum(x => x.Total);
            pedido.Total = totalPedido;

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            bool editar = _pedidoService.Modificar(pedido, pedido.Id);

            //PENDIENTE: GESTIÓN DEL PAGO, CREACIÓN DE BOLETA, FACTURA
            if(editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "alert('Pedido pagado exitosamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "alert('Error al realizar el pago');", true);
            }
        }

        protected void btnEliminarArticulo_Click(object sender, RepeaterCommandEventArgs e)
        {
            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedido"];
                var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                if (articuloEliminar != null)
                {
                    listaArticulos.Remove(articuloEliminar);
                }
                Session["articulosPedido"] = listaArticulos;
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
        }
        public void limpiarTabs()
        {
            tabMenu.Attributes.Add("class", "nav-link");
            tabMiOrden.Attributes.Add("class", "nav-link");
            divMenu.Attributes.Add("class", "tab-pane fade");
            divMiOrden.Attributes.Add("class", "tab-pane fade");
        }

        protected void btnEditarPedido_Click(object sender, EventArgs e)
        {
            validarIngreso();
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
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedido"];
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
                    //actualizarRepeater(listaPedidos, pedidos, listaPedidosVacia);
                    //upListaPedidos.Update();
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
            validarIngreso();

            ArticuloPedido articuloPedido = new ArticuloPedido();
            Articulo articulo = new Articulo();
            //articulo.Nombre = ddlArticuloPedido.SelectedItem.Text;
            //articuloPedido.IdArticulo = int.Parse(ddlArticuloPedido.SelectedValue);
            //articuloPedido.Precio = int.Parse(ddlPrecioArticuloPedido.SelectedItem.Text);
            articuloPedido.Cantidad = int.Parse(txtCantidadArticuloPedido.Text);
            articuloPedido.Total = articuloPedido.Precio * articuloPedido.Cantidad;
            articuloPedido.IdEstadoArticuloPedido = 1;
            articuloPedido.Articulo = articulo;

            List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedido"];
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
            Session["articulosPedido"] = listaArticulos;
            actualizarRepeater(listaArticulosPedido, listaArticulos, listaArticulosPedidoVacia);
            var totalPedido = listaArticulos.Sum(x => x.Total);
            lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();

            limpiarTabs();
            //tabPedidos.Attributes.Add("class", "nav-link active");
            //divPedidos.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnEliminarArticuloPedido_Click(object sender, RepeaterCommandEventArgs e)
        {
            validarIngreso();
            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedido"];
                var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                if (articuloEliminar != null)
                {
                    listaArticulos.Remove(articuloEliminar);
                }
                Session["articulosPedido"] = listaArticulos;
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
            //tabPedidos.Attributes.Add("class", "nav-link active");
            //divPedidos.Attributes.Add("class", "tab-pane active show");
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