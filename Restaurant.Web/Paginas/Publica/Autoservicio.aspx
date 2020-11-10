<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Autoservicio.aspx.cs" Inherits="Restaurant.Web.Paginas.Publica.Autoservicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-12 col-md-5 mx-auto my-5 text-center bg-light rounded p-5 shadow">
            <h2>Sistema de Autoservicio</h2>
            <p class="lead text-gris">
                Pida platos directo desde su mesa
            </p>
            <div class="form-group my-4 text-left">
                <asp:Label ID="lblEmailIngreso" runat="server" Text="E-mail de Ingreso"></asp:Label>
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" CssClass="form-control"></asp:TextBox>
            </div>     
            <asp:Button ID="btnAutoservicio" runat="server" Text="Ir al autoservicio" CssClass="btn btn-info" OnClick="btnAutoservicio_Click"/>
        </div>
    </div>
</asp:Content>
