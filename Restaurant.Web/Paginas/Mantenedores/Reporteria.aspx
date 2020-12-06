<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Admin.Master" AutoEventWireup="true" CodeBehind="Reporteria.aspx.cs" Inherits="Restaurant.Web.Paginas.Mantenedores.Reporteria" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	 <div class="row p-3">
      <div class="col-12 text-center">
        <h1 class="text-rosado">Reportería</h1>
      </div>
      <div class="col-12">
        <div class="nav nav-tabs flex-column flex-md-row" id="tabsGestionUsuario" role="tablist" aria-orientation="vertical">
          <a class="nav-link active" id="tabReportes" data-toggle="pill" href="#divReportes" role="tab" aria-controls="divReportes" aria-selected="false" runat="server">Reportes</a>
        </div>
      </div>
      <div class="col-12 bg-blanco rounded contenedor-mantenedores shadow">
        <div class="tab-content py-3 px-1">
          <div class="tab-pane show active" id="divReportes" role="tabpanel" aria-labelledby="tabReportes" runat="server" ClientIDMode="Static">
             <div class="col-12 col-md-8 col-lg-6 mx-auto pt-4">
                <div class="form-group">
                  <asp:label runat="server" CssClass="lead">Seleccione el tipo de reporte</asp:label>
                  <asp:DropDownList ID="ddlTipoReporte" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlTipoReporte_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                </div>
                <div class="form-group">
                  <asp:label ID="lblFecha" runat="server" CssClass="lead">Seleccione un rango de fechas</asp:label>
                  <div class="row">
                    <div class="col">
                      <asp:TextBox ID="txtFechaInicio" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="col">
                      <asp:TextBox ID="txtFechaFin" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                    </div>
                  </div>
                </div>
                <div class="my-5 text-center">
                  <asp:Button ID="btnObtenerReporte" runat="server" Text="Generar Reporte" OnClick="btnObtenerReporte_Click" CssClass="btn btn-info"/>
                </div>
               </div>
             </div>
          </div>
      </div>
    </div>
</asp:Content>
