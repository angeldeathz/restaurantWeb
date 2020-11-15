using Restaurant.Model.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Autoservicio
{
    public partial class PagoTarjetaFinalizado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            validarIngreso();
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            if (Session["montoTarjeta"] != null)
            {
                int montoTarjeta = (int)Session["montoTarjeta"];
                lblMontoPagado.Text = montoTarjeta.ToString();
            }
            else
            {
                lblMontoPagado.Text = pedido.Total.ToString();
            }
            lblFecha.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        }

        protected void divPagado_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Autoservicio/GestionAutoservicio.aspx");
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