namespace Restaurant.Model.Clases
{
    public class UnidadMedida
    {
        public UnidadMedida()
        {
            Id = 0;
            Nombre = string.Empty;
            Abreviacion = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Abreviacion { get; set; }
    }
}
