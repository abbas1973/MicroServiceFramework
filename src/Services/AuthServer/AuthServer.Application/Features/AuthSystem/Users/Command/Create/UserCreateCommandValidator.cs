using Application.Validators;
using FluentValidation;
using Resources;

namespace AuthServer.Application.Features.Users
{
    public class UserCreateCommandValidator : AbstractValidator<UserCreateCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public UserCreateCommandValidator()
        {
            Include(new IUsernameDTOValidator());
            Include(new IPasswordDTOValidator());
            Include(new IMobileDTOValidator());
            Include(new IEmailDTOValidator());

            RuleFor(x => x.Name).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired)
                .Must(BaseValidator.BeAValidName).WithMessage(string.Format(Messages.ErrorFormat, "{PropertyName}"));

        }


    }
}
