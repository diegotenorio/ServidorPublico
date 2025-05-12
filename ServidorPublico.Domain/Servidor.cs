using System;

namespace ServidorPublico.Domain
{
    public class Servidor
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int OrgaoId { get; set; }
        public int LotacaoId { get; set; }
        public string Sala { get; set; }
        public bool Ativo { get; set; } = true;
    }
}
