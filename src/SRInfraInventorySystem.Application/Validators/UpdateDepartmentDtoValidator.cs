using FluentValidation;
using SRInfraInventorySystem.Application.DTOs;

namespace SRInfraInventorySystem.Application.Validators
{
    public class UpdateDepartmentDtoValidator : AbstractValidator<UpdateDepartmentDto>
    {
        public UpdateDepartmentDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Departman adı boş olamaz")
                .MaximumLength(100).WithMessage("Departman adı en fazla 100 karakter olabilir");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Açıklama en fazla 500 karakter olabilir")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.ParentDepartmentId)
                .NotEqual(Guid.Empty).WithMessage("Geçersiz üst departman ID'si")
                .When(x => x.ParentDepartmentId.HasValue);

            RuleFor(x => x.ManagerPersonnelId)
                .NotEqual(Guid.Empty).WithMessage("Geçersiz yönetici personel ID'si")
                .When(x => x.ManagerPersonnelId.HasValue);
        }
    }
} 