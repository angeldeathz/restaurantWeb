﻿namespace Restaurant.Model.Clases
{
    public class Usuario
    {
        public Usuario()
        {
            Id = 0;
            Contrasena = string.Empty;
            IdPersona = 0;
            IdTipoUsuario = 0;
        }

        public int Id { get; set; }
        public string Contrasena { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoUsuario { get; set; }
        public Persona Persona { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string NombreUsuario { get { return Persona.Nombre + " " + Persona.Apellido; } }
    }
}
