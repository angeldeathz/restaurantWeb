using Restaurant.Model.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Reservas
{
    public partial class DetalleReserva : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Reserva reserva = (Reserva)Session["reservaCreada"];
            if(reserva == null)
            {
                Response.Redirect("/Paginas/Reservas/MensajeCrearReservaError.aspx");
            }
            lblIdReserva.Text = reserva.Id.ToString();
            lblFecha.Text = reserva.FechaReserva.ToShortDateString();
            lblHora.Text = reserva.FechaReserva.ToShortTimeString();
            lblComensales.Text = reserva.CantidadComensales.ToString();
        }
    }
}