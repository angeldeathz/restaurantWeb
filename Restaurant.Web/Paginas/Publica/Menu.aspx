<%@ Page Title="Menu" Language="C#" MasterPageFile="~/Master/Site.Master" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="Restaurant.Web.Paginas.Publica.Menu" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  <div class="row pt-3 pb-1">
    <div class="col-12 mb-3"><h1 class="text-rosado text-center">Nuestros platos</h1></div>
    <div class="col-12 col-md-4 offset-md-2">
      <h3>Platos tradicionales</h3>
      <p class="lead">Ofrecemos una amplia variedad de platos típicos chilenos, 
        preparados a partir de recetas pasadas de generación en generación.
      </p>
    </div>
    <div class="col-12 col-md-4">
       <img class="img-fluid rounded" src="https://i.guim.co.uk/img/media/c228ece439b3d0ca9de7c39eda6f545a101da123/0_0_4786_3191/master/4786.jpg?width=1300&quality=85&auto=format&fit=max&s=51ab4fa781254f3dc873b7f848254301"/>
    </div>
  </div>
  <div class="row py-5">
    <div class="col-12 col-md-3 text-center bg-light rounded py-2 shadow mx-auto mb-3">
      <h2>Ensaladas</h2>
      <p class="lead text-justify">
         Entradas y ensaladas típicas para acompañar tus platos favoritos
      </p>
      <img class="img-fluid" src="https://www.enmicocinahoy.cl/wp-content/uploads/2014/09/ensalada-chilena-tomate-cebolla-5-scaled.jpg"/>
    </div>
    <div class="col-12 col-md-3 text-center bg-light rounded py-2 shadow mx-auto mb-3">
      <h2>Platos</h2>
      <p class="lead text-justify">
         Deliciosa variedad de platos tradicionales, desde guisos hasta productos del mar
      </p>
      <img class="img-fluid" src="https://img-global.cpcdn.com/recipes/fadaee2fd7c899a6/751x532cq70/cazuela-de-vacuno-foto-principal.jpg"/>
    </div>
    <div class="col-12 col-md-3 text-center bg-light rounded py-2 shadow mx-auto  mb-3">
      <h2>Postres</h2>
      <p class="lead text-justify">
         Para terminar una dulce velada con un postre como hecho en casa
      </p>
      <img class="img-fluid" src="https://postresperuanos.net/wp-content/uploads/2020/07/receta-leche-asada.jpg"/>
    </div>
  </div>
</asp:Content>
