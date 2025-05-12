using MediatR;
using System;

namespace ServidorPublico.Application.Commands
{
    public class InativarServidorCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
