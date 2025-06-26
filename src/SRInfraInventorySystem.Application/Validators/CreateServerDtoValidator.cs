using FluentValidation;
using SRInfraInventorySystem.Application.DTOs;

namespace SRInfraInventorySystem.Application.Validators
{
    public class CreateServerDtoValidator : AbstractValidator<CreateServerDto>
    {
        public CreateServerDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Sunucu adı boş olamaz.")
                .MaximumLength(100).WithMessage("Sunucu adı 100 karakterden uzun olamaz.");
                
            RuleFor(x => x.IpAddress)
                .NotEmpty().WithMessage("IP adresi boş olamaz.")
                .Matches(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$")
                .WithMessage("Geçerli bir IP adresi giriniz.");
                
            RuleFor(x => x.OperatingSystem)
                .NotEmpty().WithMessage("İşletim sistemi boş olamaz.")
                .MaximumLength(50).WithMessage("İşletim sistemi 50 karakterden uzun olamaz.");
                
            RuleFor(x => x.CpuInfo)
                .NotEmpty().WithMessage("CPU bilgisi boş olamaz.")
                .MaximumLength(200).WithMessage("CPU bilgisi 200 karakterden uzun olamaz.");
                
            RuleFor(x => x.RamSizeGB)
                .GreaterThan(0).WithMessage("RAM boyutu 0'dan büyük olmalıdır.")
                .LessThanOrEqualTo(10000).WithMessage("RAM boyutu 10000 GB'dan büyük olamaz.");
                
            RuleFor(x => x.DiskSizeGB)
                .GreaterThan(0).WithMessage("Disk boyutu 0'dan büyük olmalıdır.")
                .LessThanOrEqualTo(100000).WithMessage("Disk boyutu 100000 GB'dan büyük olamaz.");
                
            RuleFor(x => x.ResponsiblePerson)
                .NotEmpty().WithMessage("Sorumlu kişi boş olamaz.")
                .MaximumLength(100).WithMessage("Sorumlu kişi 100 karakterden uzun olamaz.");
                
            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz.");
        }
    }
} 