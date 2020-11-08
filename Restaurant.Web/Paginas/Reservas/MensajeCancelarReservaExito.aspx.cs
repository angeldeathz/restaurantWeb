using Restaurant.Model.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Reservas
{
    public partial class MensajeReservaCancelada : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Reserva reserva = (Reserva)Session["reservaCancelada"];
            if (reserva == null)
            {
                Response.Redirect("/Paginas/Reservas/MensajeCancelarReservaError.aspx");
            }
            lblIdReserva.Text = reserva.Id.ToString();
        }
    }
}