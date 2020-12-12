<%@ Page Title="Contacto" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Contacto.aspx.cs" Inherits="Restaurant.Web.Paginas.Publica.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  <div class="row">
    <div class="col-12 col-lg-6 my-5 bg-light rounded py-4 shadow">
      <h2>Contáctanos</h2>
      <hr />
      <div class="form-group">
        <asp:Label runat="server">Nombre completo</asp:Label>
        <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ValidacionNombre" runat="server" ControlToValidate="txtNombre" Display="Dynamic"
         CssClass="text-danger" ErrorMessage="Debe ingresar su nombre" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
      </div>
      <div class="form-group">
        <asp:Label runat="server">E-mail</asp:Label>
        <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ValidacionEmail" runat="server" ControlToValidate="txtEmail" Display="Dynamic"
         CssClass="text-danger" ErrorMessage="Debe ingresar su e-mail" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
      </div>
      <div class="form-group">
        <asp:Label runat="server">Asunto</asp:Label>
        <asp:TextBox runat="server" ID="txtAsunto" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ValidacionAsunto" runat="server" ControlToValidate="txtAsunto" Display="Dynamic"
         CssClass="text-danger" ErrorMessage="Debe ingresar el asunto" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
      </div>
      <div class="form-group">
        <asp:Label runat="server">Mensaje</asp:Label>
        <asp:TextBox runat="server" ID="txtMensaje" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
        <asp:RequiredFieldValidator ID="ValidacionMensaje" runat="server" ControlToValidate="txtMensaje" Display="Dynamic"
         CssClass="text-danger" ErrorMessage="Debe ingresar el mensaje" ValidationGroup="Validacion"></asp:RequiredFieldValidator>
      </div>
      <div class="text-center">
        <asp:Button runat="server" ID="btnEnviarMensaje" Text="Enviar mensaje" OnClick="btnEnviarMensaje_Click" CssClass="btn btn-primary"/>
      </div>
    </div>
    <div class="col-12 col-lg-6 p-5">
      <h2>Ubicación</h2>
      <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d3330.082072065792!2d-70.60809511616809!3d-33.42110458078833!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1sen!2scl!4v1607785729713!5m2!1sen!2scl" height="500" frameborder="0" style="border:0;" allowfullscreen="" aria-hidden="false" tabindex="0"></iframe>
    </div>
  </div>
</asp:Content>
