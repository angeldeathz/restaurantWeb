<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="../Master/Site.Master" CodeBehind="IniciarSesion.aspx.cs" Inherits="Restaurant.Web.Paginas.IniciarSesion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <br />
    <br />

    Rut:
    <asp:TextBox runat="server" ID="txtRut"></asp:TextBox><br />
    Contrasena:
    <asp:TextBox runat="server" ID="txtContrasena"></asp:TextBox><br />
    <br />

    <asp:Button runat="server" ID="btnIniciarSesion" Text="Iniciar Sesión" OnClick="btnIniciarSesion_OnClick" />

</asp:Content>
