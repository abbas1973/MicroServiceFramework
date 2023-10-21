using Application.Validators;
using FluentValidation;
using Resources;

namespace AuthServer.Application.Features.Users
{
    public class UserUpdateCommandValidator : AbstractValidator<UserUpdateCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public UserUpdateCommandValidator()
        {
            Include(new IBaseEntityDTOValidator());
            Include(new IUsernameDTOValidator());
            Include(new IMobileDTOValidator());
            Include(new IEmailDTOValidator());

            RuleFor(x => x.Name).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired)
                .Must(BaseValidator.BeAValidName).WithMessage(string.Format(Messages.ErrorFormat, "{PropertyName}"));

        }


    }
}
