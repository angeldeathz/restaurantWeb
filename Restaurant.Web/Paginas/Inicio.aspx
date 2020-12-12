<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="Restaurant.Web.Paginas._Inicio" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-12 col-md-10 text-center mx-auto">
        <div id="carrusel_home" class="carousel slide" data-ride="carousel">
          <ol class="carousel-indicators">
            <li data-target="#carrusel_home" data-slide-to="0" class="active"></li>
            <li data-target="#carrusel_home" data-slide-to="1"></li>
            <li data-target="#carrusel_home" data-slide-to="2"></li>
          </ol>
          <div class="carousel-inner">
            <div class="carousel-item p-2 p-md-5 active">
                <img src="../Images/logo_sxxi.png" alt="Restaurante Siglo XXI" class="img-fluid"/>
                <h1>Restaurante</h1>
                <p class="lead text-rosado font-weight-bold">Local de comida tradicional, con más de 50 años de historia</p>
                <br />
            </div>
            <div class="carousel-item p-2 p-md-5">
                <img src="../Images/paste_choclo_2.png" alt="Pastel de Choclo" class="img-fluid"/>
                <h1>Nuestros platos</h1>
                <p class="lead text-rosado font-weight-bold">Nos caracterizamos por un menú de comida tradicional Chilena</p>
            </div>
            <div class="carousel-item p-2 p-md-5">
                <img src="../Images/auto_servicio_2.jpg" alt="Auto Servicio" class="img-fluid"/>
                <h1>Nuestro Servicio</h1>
                <p class="lead text-rosado font-weight-bold">Ofrecemos una experiencia de auto servicio altamente tecnologizada</p>
            </div>
          </div>
          <a class="carousel-control-prev" href="#carrusel_home" role="button" data-slide="prev">
            <span class="carousel-control-prev-icon" aria-hidden="true"></span>
            <span class="sr-only">Previous</span>
          </a>
          <a class="carousel-control-next" href="#carrusel_home" role="button" data-slide="next">
            <span class="carousel-control-next-icon" aria-hidden="true"></span>
            <span class="sr-only">Next</span>
          </a>
        </div>
     
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Nosotros</h2>
            <p class="font-weight-bold text-rosado">
                Somos un restaurante familiar, ofreciendo platos tradicionales por más de 50 años.
            </p>
            <p>
                <a class="btn btn-info" runat="server" href="../Paginas/Publica/nosotros">Conocer más &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Local</h2>
            <p class="font-weight-bold text-rosado">
                Nuestro local es una antigua casona colonial, ubicada en el corazón de providencia, 
            </p>
            <p>
                <a class="btn btn-info" runat="server" href="../Paginas/Publica/contacto">Conocer más &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Carta</h2>
            <p class="font-weight-bold text-rosado">
                Ofrecemos una variedad de platos tradicionales
            </p>
            <p>
                <a class="btn btn-info" runat="server" href="../Paginas/Publica/menu">Conocer más &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
