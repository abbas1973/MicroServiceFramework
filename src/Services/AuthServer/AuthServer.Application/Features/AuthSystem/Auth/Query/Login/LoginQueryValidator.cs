using FluentValidation;
using Resources;

namespace AuthServer.Application.Features.Auth
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");
        public LoginQueryValidator()
        {

            RuleFor(x => x.Username).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired);

            RuleFor(x => x.Password).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired);
        }


    }
}
