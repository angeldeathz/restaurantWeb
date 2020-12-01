using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Mantenedores
{
    public partial class VerPdf : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["nombre"] == null)
            {
                Response.Redirect("Paginas/Mantenedores/Reporteria.aspx");
            }
            string pdflocation = "C:\\Storage\\";
            string nombrePDF = Request.QueryString["nombre"];
            string ruta = Path.Combine(pdflocation, nombrePDF);

            Response.Clear();
            Response.ContentType = "application/pdf";
            /*
             *  muestra el pdf como archivo descargable para ver o guardar en el equipo
                Response.AddHeader("Content-disposition", "attachment; filename=" + rutaPdf);
            */
            Response.WriteFile(ruta);
            Response.Flush();
            Response.End();
        }
    }
}