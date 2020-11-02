using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Model.Clases
{
    [Serializable]
    public class ArticuloPedido
    {
        public int Id { get; set; }
        public int Precio { get; set; }
        public int Cantidad { get; set; }
        public int Total { get; set; }
        public int IdPedido { get; set; }
        public int IdArticulo { get; set; }
        public int IdEstadoArticuloPedido { get; set; }
        public Pedido Pedido { get; set; }
        public Articulo Articulo { get; set; }
        public EstadoArticuloPedido EstadoArticuloPedido { get; set; }
        public List<EstadoArticuloPedido> EstadosArticuloPedido { get; set; }
        public string Comentarios { get; set; }
    }
}
