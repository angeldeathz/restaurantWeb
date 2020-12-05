using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Mantenedores
{
    public partial class Reporteria : System.Web.UI.Page
    {
        private ReporteService _reporteService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                DateTime fecha = DateTime.Now;
                txtFechaInicio.Text = fecha.ToString("yyyy-MM-dd");
                txtFechaFin.Text = fecha.ToString("yyyy-MM-dd");
                txtFechaInicio.Attributes["max"] = fecha.ToString("yyyy-MM-dd");
                txtFechaFin.Attributes["max"] = fecha.ToString("yyyy-MM-dd");

                var tipoReportes = Reporte.GetTiposReporte();
                ddlTipoReporte.DataSource = tipoReportes;
                ddlTipoReporte.DataValueField = "Key";
                ddlTipoReporte.DataTextField = "Value";
                ddlTipoReporte.DataBind();
                ddlTipoReporte.Items.Insert(0, new ListItem("Seleccionar", "0"));
                ddlTipoReporte.SelectedIndex = 0;
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
            }
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario.IdTipoUsuario != TipoUsuario.administrador)
            {
                Response.Redirect("../Mantenedores/Inicio.aspx");
            }
        }
        protected void ddlTipoReporte_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idTipoReporte = Convert.ToInt32(ddlTipoReporte.SelectedValue);
            if (idTipoReporte == 0)
            {
                return;
            }
            
            switch (idTipoReporte)
            {
                case Reporte.reporteDiario:
                case Reporte.reporteMensual:
                    lblFecha.Text = "Seleccione una fecha";
                    txtFechaFin.Enabled = false;
                    break;
                default:
                    lblFecha.Text = "Seleccione un rango de fechas";
                    txtFechaFin.Enabled = true;
                    break;
            }
        }
        protected void btnObtenerReporte_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            int idTipoReporte = Convert.ToInt32(ddlTipoReporte.SelectedValue);
            if (idTipoReporte == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "tipoReporte", "Swal.fire('Debe seleccionar el tipo de reporte', '', 'error');", true);
                return;
            }

            DateTime fechaInicio;
            DateTime fechaFin;
            try
            {
                fechaInicio = DateTime.Parse(txtFechaInicio.Text);
                fechaFin = DateTime.Parse(txtFechaFin.Text);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fecha", "Swal.fire('Debe seleccionar una fecha', '', 'error');", true);
                return;
            }
            if(idTipoReporte != Reporte.reporteDiario && idTipoReporte != Reporte.reporteMensual && fechaInicio > fechaFin)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "fecha", "Swal.fire('La fecha de inicio debe ser anterior a la fecha de término', '', 'error');", true);
                return;
            }

            Usuario usuario = (Usuario)Session["usuario"];
            Reporte reporte = new Reporte();
            reporte.IdReporte = idTipoReporte;
            reporte.IdUsuario = usuario.Id;
            switch(idTipoReporte)
            {
                case Reporte.reporteDiario:
                    reporte.FechaDesde = fechaInicio;
                    reporte.FechaHasta = fechaInicio;
                    break;
                case Reporte.reporteMensual:
                    reporte.FechaDesde = fechaInicio.Date.AddDays(1 - fechaInicio.Day);
                    reporte.FechaHasta = reporte.FechaDesde.AddMonths(1).AddDays(-1);
                    break;
                default:
                    reporte.FechaDesde = fechaInicio;
                    reporte.FechaHasta = fechaFin;
                    break;
            }

            DescargarReporte(reporte);
        }
        private void DescargarReporte(Reporte reporte)
        {
            try
            {
                Token token = (Token)Session["token"];
                _reporteService = new ReporteService(token.access_token);

                string base64 = _reporteService.Obtener(reporte);
                byte[] pdfBytes = Convert.FromBase64String(base64);

                string pdflocation = "C:\\Storage\\";
                string nombrePDF = $"reporte-{DateTime.Now:yyyy-MM-dd}.pdf";
                string ruta = Path.Combine(pdflocation, nombrePDF);

                FileStream file = File.Create(ruta);
                file.Write(pdfBytes, 0, pdfBytes.Length);
                file.Close();

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notificacion", "window.open('VerReporte.aspx?nombre=" + nombrePDF + "','_blank');", true);
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + ex.Message + "', 'error');", true);
            }
        }
    }
}