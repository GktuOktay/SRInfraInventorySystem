using FluentValidation;
using SRInfraInventorySystem.Application.DTOs;

namespace SRInfraInventorySystem.Application.Validators
{
    public class CreatePersonnelDtoValidator : AbstractValidator<CreatePersonnelDto>
    {
        public CreatePersonnelDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("Ad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyad alanı boş olamaz")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-posta alanı boş olamaz")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir");

            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Telefon en fazla 20 karakter olabilir");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Ünvan alanı boş olamaz")
                .MaximumLength(100).WithMessage("Ünvan en fazla 100 karakter olabilir");

            RuleFor(x => x.IdentityNumber)
                .NotEmpty().WithMessage("TC Kimlik No alanı boş olamaz")
                .Length(11).WithMessage("TC Kimlik No 11 haneli olmalıdır")
                .Matches(@"^\d{11}$").WithMessage("TC Kimlik No sadece rakam içermelidir");

            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz")
                .MaximumLength(50).WithMessage("Kullanıcı adı en fazla 50 karakter olabilir")
                .Matches(@"^[a-zA-Z0-9._-]+$").WithMessage("Kullanıcı adı sadece harf, rakam, nokta, alt çizgi ve tire içerebilir");
        }
    }
} 