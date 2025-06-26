using FluentValidation;
using SRInfraInventorySystem.Application.DTOs;

namespace SRInfraInventorySystem.Application.Validators
{
    public class AssignManagerDtoValidator : AbstractValidator<AssignManagerDto>
    {
        public AssignManagerDtoValidator()
        {
            RuleFor(x => x.DepartmentId)
                .NotEmpty()
                .WithMessage("Departman ID'si gereklidir");

            // ManagerPersonnelId nullable olabilir (null = yönetici kaldır)
        }
    }
} 