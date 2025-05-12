using FluentValidation;
using ServidorPublico.Application.Commands;

namespace ServidorPublico.Application.Validators
{
    public class CreateServidorValidator : AbstractValidator<CreateServidorCommand>
    {
        public CreateServidorValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().WithMessage("Nome é obrigatório.");
            RuleFor(x => x.OrgaoId).GreaterThan(0).WithMessage("OrgaoId deve ser maior que zero.");
            RuleFor(x => x.LotacaoId).GreaterThan(0).WithMessage("LotacaoId deve ser maior que zero.");
        }
    }
}
