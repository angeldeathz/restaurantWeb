<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Restaurant.Web.Paginas.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div id="platos" class="col-md-4">
            <h2>Nosotros</h2>
            <p class="text-rosado">
                Somos un restaurante familiar, ofreciendo delicosos platos tradicionales por más de 50 años.
            </p>
            <p>
                <a class="btn btn-warning" href="https://go.microsoft.com/fwlink/?LinkId=301948">Conocer más &raquo;</a>
            </p>
        </div>
        <div id="postres" class="col-md-4">
            <h2>Local</h2>
            <p>
                Nuestro local es una antigua casona colonial, ubicada en el corazón de providencia, 
            </p>
            <p>
                <a class="btn btn-warning" runat="server" href="../Paginas/contacto">Conocer más &raquo;</a>
            </p>
        </div>
        <div id="bebestibles" class="col-md-4">
            <h2>Menú</h2>
            <p>
                Ofrecemos una variedad de platos tradicionales
            </p>
            <p>
                <a class="btn btn-warning" runat="server" href="../Paginas/menu">Conocer más &raquo;</a>
            </p>
        </div>
    </div>
</asp:Content>
