namespace Restaurant.Model.Clases
{
    public class EstadoPedido
    {
        public EstadoPedido()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
