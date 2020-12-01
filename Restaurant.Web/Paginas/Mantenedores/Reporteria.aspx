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
            <div class="row">
              <div class="col-12 col-md-6">
                <div class="col-12 mb-5">
                  <h4>Reporte de utilidad diaria</h4>
                  <asp:TextBox ID="txtFechaDiario" runat="server" TextMode="Date"></asp:TextBox>
                  <asp:Button ID="btnReporteDiario" runat="server" Text="Obtener" OnClick="btnReporteDiario_Click" CssClass="btn btn-info"/>
                </div>
                <div class="col-12">
                  <h4>Reporte de utilidad mensual</h4>
                  <asp:TextBox ID="txtFechaMensual" runat="server" TextMode="Date"></asp:TextBox>
                  <asp:Button ID="btnReporteMensual" runat="server" Text="Obtener" OnClick="btnReporteMensual_Click" CssClass="btn btn-warning"/>
                </div>
               </div>
             </div>
          </div>
        </div>
      </div>
    </div>
        <!-- Modal Reportes -->
    <div class="modal fade" id="modalUsuario" tabindex="-1" role="dialog" aria-labelledby="tituloModalUsuario" aria-hidden="true">
      <div class="modal-dialog modal-md" role="document">
          <asp:UpdatePanel ID="upModalUsuario" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-content">                    
                  <div class="modal-header">
                    <asp:Label ID="tituloModalUsuario" class="modal-title h5" runat="server" Text="Crear Usuario"></asp:Label>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Cerrar">
                      <span aria-hidden="true">&times;</span>
                    </button>
                  </div>
                  <div class="modal-body py-4">                      
                      <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control" Visible="false"></asp:TextBox>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblRutUsuario" runat="server" Text="Rut" CssClass="d-block"></asp:Label>
                            <asp:TextBox ID="txtRutUsuario" runat="server" CssClass="form-control w-75 d-inline-block" TextMode="Number"></asp:TextBox>
                            &nbsp;-&nbsp;
                            <asp:TextBox ID="txtDigitoVerificadorUsuario" runat="server" CssClass="form-control w-15 d-inline-block" MaxLength="1"></asp:TextBox>                           
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblTipoUsuario" runat="server" Text="Tipo de Usuario"></asp:Label>
                            <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-control"></asp:DropDownList>
                          </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblNombreUsuario" runat="server" Text="Nombre"></asp:Label>
                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblApellidoUsuario" runat="server" Text="Apellido"></asp:Label>
                            <asp:TextBox ID="txtApellidoUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblEmailUsuario" runat="server" Text="E-mail"></asp:Label>
                            <asp:TextBox ID="txtEmailUsuario" runat="server" CssClass="form-control" ></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblTelefonoUsuario" runat="server" Text="Teléfono"></asp:Label>
                            <asp:TextBox ID="txtTelefonoUsuario" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                      </div>
                      <div class="row">
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblContrasena" runat="server" Text="Contraseña"></asp:Label>
                            <asp:TextBox ID="txtContrasena" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                        <div class="col-12 col-md-6">
                            <asp:Label ID="lblContrasenaRepetir" runat="server" Text="Repetir Contraseña"></asp:Label>
                            <asp:TextBox ID="txtContrasenaRepetir" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>
                      </div>
                  </div>
                  <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancelar</button>
                  </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
      </div>
    </div><!-- Fin modal Reportes -->
</asp:Content>
