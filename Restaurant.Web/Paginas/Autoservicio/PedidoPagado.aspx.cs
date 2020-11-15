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
    public partial class PedidoPagado : System.Web.UI.Page
    {
        private PedidoService _pedidoService;
        private ArticuloPedidoService _articuloPedidoService;
        private ArticuloService _articuloService;
        protected void Page_Load(object sender, EventArgs e)
        {
            validarIngreso();
            Token token = (Token)Session["token"];
            Reserva reserva = (Reserva)Session["reservaCliente"];
            _pedidoService = new PedidoService(token.access_token);
            List<Pedido> pedidos = _pedidoService.Obtener();

            Pedido pedidoCliente = null;
            if (pedidos != null && pedidos.Count > 0)
            {
                pedidoCliente = pedidos.FirstOrDefault(x => x.IdEstadoPedido == EstadoPedido.pagado
                                                         && x.Reserva.Id == reserva.Id
                                                         && x.FechaHoraInicio.Date == DateTime.Now.Date);

                if (pedidoCliente != null)
                {
                    cargarPedido(token, pedidoCliente);
                }
            }
            upArticulosPedido.Update();
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
            if (articulos == null || articulos.Count == 0)
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
                lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
            }
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();
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