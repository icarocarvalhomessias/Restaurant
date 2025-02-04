using System;

namespace Restaurant.API.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public DateTime DataNascimento { get; set; }
        public string Email { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
    }

    public enum TipoUsuario
    {
        Gestor = 1,
        Entregador = 2
    }
}
