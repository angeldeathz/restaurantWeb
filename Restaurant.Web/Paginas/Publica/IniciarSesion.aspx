<%@ Page Title="Iniciar Sesión" Language="C#" MasterPageFile="~/Master/Site.Master" CodeBehind="IniciarSesion.aspx.cs" Inherits="Restaurant.Web.Paginas.IniciarSesion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-12 col-md-6 col-lg-5 mx-auto my-5">
            <div class="p-5 bg-blanco rounded">
                <h3 class="">¡Bienvenido!</h3>
                <asp:Label runat="server" for="txtRut" CssClass="">Rut</asp:Label>
                <asp:TextBox runat="server" ID="txtRut" CssClass="form-control"></asp:TextBox><br />
                <asp:Label runat="server" for="txtContrasena" CssClass="" >Contraseña</asp:Label>
                <asp:TextBox runat="server" ID="txtContrasena" CssClass="form-control" TextMode="Password"></asp:TextBox><br />
                <div class="text-center">
                    <asp:Button runat="server" ID="btnIniciarSesion" Text="Iniciar Sesión" OnClick="btnIniciarSesion_OnClick" 
                        CssClass="btn btn-info"/>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
