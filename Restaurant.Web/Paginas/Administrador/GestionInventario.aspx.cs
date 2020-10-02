using Restaurant.Services.Servicios;
using Restaurant.Model.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Restaurant.Model.Dto;

namespace Restaurant.Web.Paginas.Administrador
{
    public partial class GestionInventario : System.Web.UI.Page
    {
        private ProveedorService _proveedorService;
        private InsumoService _insumoService;
        private UnidadDeMedidaService _unidadDeMedidaService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                
                Token token = (Token) Session["token"];
                _proveedorService = new ProveedorService(token.access_token);
                _insumoService = new InsumoService(token.access_token);
                _unidadDeMedidaService = new UnidadDeMedidaService(token.access_token);

                List<Insumo> insumos = _insumoService.Obtener();
                if (insumos != null && insumos.Count > 0)
                {
                    listaInsumos.DataSource = insumos;
                    listaInsumos.DataBind();
                }

                List<Proveedor> proveedores = _proveedorService.Obtener();
                if (proveedores != null)
                {
                    if (proveedores.Count > 0 && proveedores.Count > 0)
                    {
                        listaProveedores.DataSource = proveedores;
                        listaProveedores.DataBind();

                        ddlProveedorInsumo.DataSource = proveedores;
                        ddlProveedorInsumo.DataTextField = "NombreProveedor";
                        ddlProveedorInsumo.DataValueField = "Id";
                        ddlProveedorInsumo.DataBind();
                        ddlProveedorInsumo.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlProveedorInsumo.SelectedIndex = 0;
                    }
                }

                List<UnidadMedida> unidades = _unidadDeMedidaService.Obtener();
                if (unidades != null)
                {
                    if (unidades.Count > 0)
                    {
                        ddlUnidadMedida.DataSource = unidades;
                        ddlUnidadMedida.DataTextField = "Nombre";
                        ddlUnidadMedida.DataValueField = "Id";
                        ddlUnidadMedida.DataBind();
                        ddlUnidadMedida.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlUnidadMedida.SelectedIndex = 0;
                    }
                }
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
            ValidarSesion();
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
        protected void btnModalEditarInsumos_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idInsumo;
            if (int.TryParse((string)e.CommandArgument, out idInsumo))
            {
                Token token = (Token)Session["token"];
                _insumoService = new InsumoService(token.access_token);
                Insumo insumo = _insumoService.Obtener(idInsumo);
                if (insumo != null)
                {
                    tituloModalInsumo.Text = "Editar Insumo";
                    btnCrearInsumo.Visible = false;
                    btnEditarInsumo.Visible = true;
                    txtIdInsumo.Text = insumo.Id.ToString();
                    txtNombreInsumo.Text = insumo.Nombre;
                    txtStockActual.Text = insumo.StockActual.ToString();
                    txtStockCritico.Text = insumo.StockCritico.ToString();
                    txtStockOptimo.Text = insumo.StockOptimo.ToString();
                    ddlProveedorInsumo.SelectedValue = insumo.IdProveedor.ToString();
                    ddlUnidadMedida.SelectedValue = insumo.IdUnidadDeMedida.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal();", true);
                    upModalInsumo.Update();
                }
            }
            System.Web.UI.HtmlControls.HtmlControl tab = tabInsumos;
            limpiarTabs();
            tabInsumos.Attributes.Add("class", "nav-link active");
            divInsumos.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnCrearInsumo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Insumo insumo = new Insumo();
            insumo.Nombre = txtNombreInsumo.Text;
            insumo.StockActual = int.Parse(txtStockActual.Text);
            insumo.StockCritico = int.Parse(txtStockCritico.Text);
            insumo.StockOptimo = int.Parse(txtStockOptimo.Text);
            insumo.IdProveedor = int.Parse(ddlProveedorInsumo.SelectedValue);
            insumo.IdUnidadDeMedida = int.Parse(ddlUnidadMedida.SelectedValue);

            Token token = (Token)Session["token"];
            _insumoService = new InsumoService(token.access_token);
            int idInsumo = _insumoService.Guardar(insumo);
            if (idInsumo != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('close');", true);
            }
        }

        protected void btnEditarInsumo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Insumo insumo = new Insumo();
            insumo.Id = int.Parse(txtIdInsumo.Text);
            insumo.Nombre = txtNombreInsumo.Text;
            insumo.StockActual = int.Parse(txtStockActual.Text);
            insumo.StockCritico = int.Parse(txtStockCritico.Text);
            insumo.StockOptimo = int.Parse(txtStockOptimo.Text);
            insumo.IdProveedor = int.Parse(ddlProveedorInsumo.SelectedValue);
            insumo.IdUnidadDeMedida = int.Parse(ddlUnidadMedida.SelectedValue);

            Token token = (Token)Session["token"];
            _insumoService = new InsumoService(token.access_token);
            bool creado = _insumoService.Modificar(insumo);
            if (creado)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('close');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('close');", true);
            }
        }
    }
}