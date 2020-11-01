using System;
using System.Collections.Generic;

namespace Restaurant.Model.Clases
{
    [Serializable]
    public class ArticuloPedido
    {
        public ArticuloPedido()
        {
            Id = 0;
            Precio = 0;
            Cantidad = 0;
            Total = 0;
            IdPedido = 0;
            IdArticulo = 0;
            IdEstadoArticuloPedido = 0;
        }

        public int Id { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public int IdPedido { get; set; }
        public int IdArticulo { get; set; }
        public int IdEstadoArticuloPedido { get; set; }
        public Pedido Pedido { get; set; }
        public Articulo Articulo { get; set; }
        public List<EstadoArticuloPedido> EstadosArticuloPedido { get; set; }
    }
}
