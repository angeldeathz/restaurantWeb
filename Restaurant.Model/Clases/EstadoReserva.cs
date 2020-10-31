namespace Restaurant.Model.Clases
{
    public class EstadoReserva
    {

        public const int creada = 1;
        public const int enCurso = 2;
        public const int cancelada = 3;
        public const int terminada = 4;
        public EstadoReserva()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
