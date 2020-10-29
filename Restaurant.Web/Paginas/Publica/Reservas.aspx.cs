using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Publica
{
    public partial class Reservas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnReservarMesa_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Reservas/CrearReserva.aspx");
        }

        protected void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Paginas/Reservas/CancelarReserva.aspx");
        }
    }
}