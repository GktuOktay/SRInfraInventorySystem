using FluentValidation;
using SRInfraInventorySystem.Application.DTOs;

namespace SRInfraInventorySystem.Application.Validators
{
    public class CreateDatabaseDtoValidator : AbstractValidator<CreateDatabaseDto>
    {
        public CreateDatabaseDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Veritabanı adı boş olamaz")
                .MaximumLength(100).WithMessage("Veritabanı adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Veritabanı türü boş olamaz")
                .MaximumLength(50).WithMessage("Veritabanı türü en fazla 50 karakter olabilir");

            RuleFor(x => x.Version)
                .MaximumLength(20).WithMessage("Versiyon en fazla 20 karakter olabilir");

            RuleFor(x => x.ResponsiblePerson)
                .NotEmpty().WithMessage("Sorumlu kişi boş olamaz")
                .MaximumLength(100).WithMessage("Sorumlu kişi en fazla 100 karakter olabilir");

            RuleFor(x => x.AccessPermissions)
                .MaximumLength(500).WithMessage("Erişim izinleri en fazla 500 karakter olabilir");

            RuleFor(x => x.ConnectionTools)
                .MaximumLength(200).WithMessage("Bağlantı araçları en fazla 200 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir");
        }
    }
} 