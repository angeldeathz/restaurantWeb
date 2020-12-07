using System;

namespace Restaurant.Model.Clases
{
    public class HorarioReserva
    {
        public HorarioReserva()
        {
            Id = 0;
            DiaSemana = 0;
            HoraInicio = DateTime.Now;
            HoraFin = DateTime.Now;
        }
        public int Id { get; set; }
        public int DiaSemana { get; set; }
        public DateTime HoraInicio { get; set; }
        public DateTime HoraFin { get; set; }
        public string HoraInicioTime { get { return HoraInicio.ToShortTimeString(); } }
        public string HoraFinTime { get { return HoraFin.ToShortTimeString(); } }
        public string NombreDiaSemana { get { return GetNombreDiaSemana(DiaSemana); }}

        public static string GetNombreDiaSemana(int diaSemana)
        {
            string nombre = string.Empty;
            switch (diaSemana)
            {
                case 0:
                    nombre = "Domingo";
                    break;
                case 1:
                    nombre = "Lunes";
                    break;
                case 2:
                    nombre = "Martes";
                    break;
                case 3:
                    nombre = "Miércoles";
                    break;
                case 4:
                    nombre = "Jueves";
                    break;
                case 5:
                    nombre = "Viernes";
                    break;
                case 6:
                    nombre = "Sábado";
                    break;
            }

            return nombre;
        }
    }
}
