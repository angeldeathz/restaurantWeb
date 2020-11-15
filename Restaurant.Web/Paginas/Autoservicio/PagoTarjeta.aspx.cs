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
    public partial class PagoTarjeta : System.Web.UI.Page
    {
        private PedidoService _pedidoService;

        protected void Page_Load(object sender, EventArgs e)
        {
            validarIngreso();
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            if(Session["montoTarjeta"] != null)
            {
                int montoTarjeta = (int)Session["montoTarjeta"];
                lblMontoPagar.Text = montoTarjeta.ToString();
            }
            else
            {
                lblMontoPagar.Text = pedido.Total.ToString();
            }
        }

        protected void divPagar_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            
            if (Session["montoTarjeta"] == null) //Pago solo con tarjeta, se completa el pago
            {
                pedido.IdEstadoPedido = EstadoPedido.pagado;
                pedido.Reserva = null;
                pedido.EstadoPedido = null;

                Token token = (Token)Session["token"];
                _pedidoService = new PedidoService(token.access_token);
                bool editar = _pedidoService.Modificar(pedido, pedido.Id);

                if (!editar)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarCuenta", "Swal.fire('Error al realizar el pago', '', 'error');", true);
                }
            }
           
            Session["pedidoCliente"] = pedido;
            Response.Redirect("/Paginas/Autoservicio/PagoTarjetaFinalizado.aspx");
        }
        protected void validarIngreso()
        {
            if (Session["reservaCliente"] == null || Session["token"] == null)
            {
                Response.Redirect("/Paginas/Publica/Autoservicio.aspx");
            }
        }
    }
}