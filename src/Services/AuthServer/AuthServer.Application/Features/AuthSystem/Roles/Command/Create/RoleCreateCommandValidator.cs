using Application.Validators;
using FluentValidation;
using Resources;

namespace AuthServer.Application.Features.Roles
{
    public class RoleCreateCommandValidator : AbstractValidator<RoleCreateCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public RoleCreateCommandValidator()
        {
            //عنوان
            RuleFor(x => x.Name).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired)
                .Must(BaseValidator.BeAValidName).WithMessage(string.Format(Messages.ErrorFormat, "{PropertyName}"));

            // دسترسی ها
            RuleFor(x => x.Claims).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired)
                .Must(claims => claims.Any()).WithMessage("دسترسی های نقش را مشخص کنید!");
        }


    }
}
