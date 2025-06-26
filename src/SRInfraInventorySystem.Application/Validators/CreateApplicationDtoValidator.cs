using FluentValidation;
using SRInfraInventorySystem.Application.DTOs;

namespace SRInfraInventorySystem.Application.Validators
{
    public class CreateApplicationDtoValidator : AbstractValidator<CreateApplicationDto>
    {
        public CreateApplicationDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Uygulama adı boş olamaz")
                .MaximumLength(100).WithMessage("Uygulama adı en fazla 100 karakter olabilir");

            RuleFor(x => x.DnsName)
                .NotEmpty().WithMessage("DNS adı boş olamaz")
                .MaximumLength(100).WithMessage("DNS adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Port)
                .InclusiveBetween(1, 65535).WithMessage("Port numarası 1-65535 arasında olmalıdır");

            RuleFor(x => x.Protocol)
                .NotEmpty().WithMessage("Protokol boş olamaz")
                .MaximumLength(10).WithMessage("Protokol en fazla 10 karakter olabilir");

            RuleFor(x => x.ResponsiblePerson)
                .NotEmpty().WithMessage("Sorumlu kişi boş olamaz")
                .MaximumLength(100).WithMessage("Sorumlu kişi en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");

            RuleFor(x => x.Version)
                .MaximumLength(20).WithMessage("Versiyon en fazla 20 karakter olabilir");
        }
    }
} 