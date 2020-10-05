namespace Restaurant.Model.Clases
{
    public class MetodoPago
    {
        public MetodoPago()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
