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
        private DocumentoPagoService _documentoPagoService;
        private MedioPagoDocumentoService _medioPagoDocumentoService;
        private ReservaService _reservaService;
        
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
        protected void btnPagoCredito_Click(object sender, EventArgs e)
        {
            int medioPago = MedioPago.credito;
            Pagar(medioPago);
        }

        protected void btnPagoDebito_Click(object sender, EventArgs e)
        {
            int medioPago = MedioPago.debito;
            Pagar(medioPago);
        }
        protected void Pagar(int medioPago)
        {
            validarIngreso();
            if(Session["tipoDocumentoPago"] == null) //No se guardó la info del tipo de pago
            {
                Response.Redirect("/Paginas/Autoservicio/GestionAutoservicio.aspx");
            }
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
            crearDocumentoPago(pedido.Id, pedido.Total, medioPago);
            Session["pedidoCliente"] = pedido;
            Response.Redirect("/Paginas/Autoservicio/PagoTarjetaFinalizado.aspx");
        }

        public void crearDocumentoPago(int idPedido, int total, int medioPago)
        {
            DocumentoPago documentoPago= new DocumentoPago();
            documentoPago.IdPedido = idPedido;
            documentoPago.Total = total;
            documentoPago.FechaHora = DateTime.Now;
            documentoPago.IdTipoDocumentoPago = (int)Session["tipoDocumentoPago"];
            Token token = (Token)Session["token"];
            _documentoPagoService = new DocumentoPagoService(token.access_token);
            int idDocumentoPago = _documentoPagoService.Guardar(documentoPago);
            if(idDocumentoPago == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearDocumento", "Swal.fire('Error al realizar el pago', '', 'error');", true);
            }
            MedioPagoDocumento medioPagoDocumento = new MedioPagoDocumento();
            medioPagoDocumento.IdDocumentoPago = idDocumentoPago;
            medioPagoDocumento.IdMedioPago = medioPago;
            medioPagoDocumento.Monto = Convert.ToInt32(lblMontoPagar.Text);
            _medioPagoDocumentoService = new MedioPagoDocumentoService(token.access_token);
            int idMedioPago = _medioPagoDocumentoService.Guardar(medioPagoDocumento);
            if (idMedioPago == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearMedioPago", "Swal.fire('Error al realizar el pago', '', 'error');", true);
            }

            Reserva reserva = (Reserva)Session["reservaCliente"];
            ReservaCambioEstado cambioEstado = new ReservaCambioEstado();
            cambioEstado.IdReserva = reserva.Id;
            cambioEstado.IdEstadoReserva = EstadoReserva.finalizada;
            _reservaService = new ReservaService(token.access_token);
            bool editar = _reservaService.ModificarEstado(cambioEstado);
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