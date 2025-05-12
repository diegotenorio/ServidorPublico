using System;

namespace ServidorPublico.Application.DTOs
{
    public class ServidorDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public int OrgaoId { get; set; }
        public int LotacaoId { get; set; }
        public string Sala { get; set; }
    }
}
