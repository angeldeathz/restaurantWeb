using Restaurant.Model.Clases;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Publica
{
    public partial class Autoservicio : System.Web.UI.Page
    {
        private PedidoService _pedidoService;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAutoservicio_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            _pedidoService = new PedidoService(token.access_token);
            List<Pedido> pedidos = _pedidoService.Obtener();
            if (pedidos == null || pedidos.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorPedido", "alert('Debe registrarse en la entrada');", true);
                return;
            }

            Pedido pedidoCliente = pedidos.FirstOrDefault(x => x.IdEstadoPedido == EstadoPedido.enCurso
                                                          && x.Cliente.Persona.Email == email);
            if (pedidoCliente == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorPedido", "alert('Debe registrarse en la entrada');", true);
                return;
            }
            Session["pedidoCliente"] = pedidoCliente;
            Response.Redirect("/Paginas/Autoservicio/GestionAutoservicio.aspx");
        }
    }
}