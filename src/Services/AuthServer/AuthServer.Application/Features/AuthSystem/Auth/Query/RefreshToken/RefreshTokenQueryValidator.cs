using FluentValidation;
using Resources;

namespace AuthServer.Application.Features.Auth
{
    public class RefreshTokenQueryValidator : AbstractValidator<RefreshTokenQuery>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");
        public RefreshTokenQueryValidator()
        {
            RuleFor(x => x.RefreshToken).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired);


            //RuleFor(x => x.AccessToken).NotNull().WithMessage(_errorRequired)
            //    .NotEmpty().WithMessage(_errorRequired);
        }


    }
}
