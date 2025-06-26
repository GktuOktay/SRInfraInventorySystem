using FluentValidation;
using SRInfraInventorySystem.Application.DTOs;

namespace SRInfraInventorySystem.Application.Validators
{
    public class CreateAccessLogDtoValidator : AbstractValidator<CreateAccessLogDto>
    {
        public CreateAccessLogDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("Kullanıcı adı gereklidir")
                .MaximumLength(100)
                .WithMessage("Kullanıcı adı 100 karakterden fazla olamaz");

            RuleFor(x => x.AccessMethod)
                .NotEmpty()
                .WithMessage("Erişim yöntemi gereklidir")
                .MaximumLength(50)
                .WithMessage("Erişim yöntemi 50 karakterden fazla olamaz");

            RuleFor(x => x.Permissions)
                .MaximumLength(200)
                .WithMessage("İzinler 200 karakterden fazla olamaz");

            RuleFor(x => x.AccessTime)
                .NotEmpty()
                .WithMessage("Erişim zamanı gereklidir")
                .LessThanOrEqualTo(DateTime.Now)
                .WithMessage("Erişim zamanı gelecekte olamaz");

            RuleFor(x => x.ExitTime)
                .GreaterThan(x => x.AccessTime)
                .When(x => x.ExitTime.HasValue)
                .WithMessage("Çıkış zamanı erişim zamanından sonra olmalıdır");

            RuleFor(x => x.IpAddress)
                .NotEmpty()
                .WithMessage("IP adresi gereklidir")
                .MaximumLength(15)
                .WithMessage("IP adresi 15 karakterden fazla olamaz")
                .Matches(@"^(?:[0-9]{1,3}\.){3}[0-9]{1,3}$")
                .WithMessage("Geçerli bir IP adresi giriniz");

            RuleFor(x => x.UserAgent)
                .MaximumLength(500)
                .WithMessage("User agent 500 karakterden fazla olamaz");

            RuleFor(x => x.Notes)
                .MaximumLength(1000)
                .WithMessage("Notlar 1000 karakterden fazla olamaz");

            RuleFor(x => x)
                .Must(HaveAtLeastOneResource)
                .WithMessage("En az bir kaynak (Sunucu, Uygulama veya Veritabanı) seçilmelidir");
        }

        private bool HaveAtLeastOneResource(CreateAccessLogDto dto)
        {
            return dto.ServerId.HasValue || dto.ApplicationId.HasValue || dto.DatabaseId.HasValue;
        }
    }
} 