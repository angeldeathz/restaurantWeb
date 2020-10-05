namespace Restaurant.Model.Clases
{
    public class TipoPreparacion
    {
        public TipoPreparacion()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
