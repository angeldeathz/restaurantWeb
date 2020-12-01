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
                txtFechaDiario.Text = fecha.ToString("yyyy-MM-DD");
                txtFechaMensual.Text = fecha.ToString("yyyy-MM-DD");
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

        private void DescargarReporte(Reporte reporte)
        {
            Token token = (Token) Session["token"];
            _reporteService = new ReporteService(token.access_token);
            string base64 = _reporteService.Obtener(reporte);
            byte[] pdfBytes = Convert.FromBase64String(base64);

            string pdflocation = "C:\\Storage\\";
            string rutaBase = AppDomain.CurrentDomain.BaseDirectory;
            string nombrePDF = $"reporte-{DateTime.Now:yyyy-MM-dd}.pdf";
            string ruta = Path.Combine(pdflocation, nombrePDF);

            FileStream file = File.Create(ruta);
            file.Write(pdfBytes, 0, pdfBytes.Length);
            file.Close();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notificacion", "window.open('VerReporte.aspx?nombre=" + nombrePDF + "','_blank');", true);
        }

        protected void btnReporteDiario_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            DateTime fecha = DateTime.Parse(txtFechaDiario.Text);
            Usuario usuario = (Usuario)Session["usuario"];
            Reporte reporte = new Reporte();
            reporte.IdReporte = Reporte.reporteDiario;
            reporte.IdUsuario = usuario.Id;
            reporte.FechaDesde = fecha;
            reporte.FechaHasta = fecha;
            DescargarReporte(reporte);
        }

        protected void btnReporteMensual_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            DateTime fecha = DateTime.Parse(txtFechaMensual.Text);
            Usuario usuario = (Usuario)Session["usuario"];
            Reporte reporte = new Reporte();
            reporte.IdReporte = Reporte.reporteMensual;
            reporte.IdUsuario = usuario.Id;
            reporte.FechaDesde = fecha.Date.AddDays(1 - fecha.Day);
            reporte.FechaHasta = reporte.FechaDesde.AddMonths(1).AddDays(-1);

            DescargarReporte(reporte);
        }
    }
}