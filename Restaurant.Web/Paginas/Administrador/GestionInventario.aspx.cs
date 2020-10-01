using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Administrador
{
    public partial class GestionInventario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                //Llenar proveedores
                //Llenar unidades de medida
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
            }
        }
        protected void btnModalCrearInsumos_Click(object sender, EventArgs e)
        {
            tituloModalInsumo.Text = "Crear Insumo";
            btnCrearInsumo.Visible = true;
            btnEditarInsumo.Visible = false;
            txtNombreInsumo.Text = "";
            txtStockActual.Text = "";
            txtStockCritico.Text = "";
            txtStockOptimo.Text = "";
            ddlProveedorInsumo.SelectedValue = "";
            ddlUnidadMedida.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal();", true);
            upModalInsumo.Update();
            System.Web.UI.HtmlControls.HtmlControl tab = tabInsumos;
            limpiarTabs();
            tabInsumos.Attributes.Add("class", "nav-link active");
            divInsumos.Attributes.Add("class", "tab-pane fade active show");
        }

        public void limpiarTabs()
        {
            tabInventario.Attributes.Add("class", "nav-link");
            tabInsumos.Attributes.Add("class", "nav-link");
            tabProveedores.Attributes.Add("class", "nav-link");
            tabOrdenes.Attributes.Add("class", "nav-link");
            divInventario.Attributes.Add("class", "tab-pane fade");
            divInsumos.Attributes.Add("class", "tab-pane fade");
            divProveedores.Attributes.Add("class", "tab-pane fade");
            divOrdenes.Attributes.Add("class", "tab-pane fade");
        }
        protected void btnModalEditarInsumos_Click(object sender, EventArgs e)
        {
            // buscar insumo
            tituloModalInsumo.Text = "Editar Insumo";
            btnCrearInsumo.Visible = false;
            btnEditarInsumo.Visible = true;
            txtNombreInsumo.Text = "";
            txtStockActual.Text = "";
            txtStockCritico.Text = "";
            txtStockOptimo.Text = "";
            ddlProveedorInsumo.SelectedValue = "";
            ddlUnidadMedida.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal();", true);
            upModalInsumo.Update();
        }

        protected void btnCrearInsumo_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditarInsumo_Click(object sender, EventArgs e)
        {

        }
    }
}