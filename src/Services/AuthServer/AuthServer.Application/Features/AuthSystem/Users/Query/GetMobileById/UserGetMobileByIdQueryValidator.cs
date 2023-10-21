using Application.Validators;
using FluentValidation;

namespace AuthServer.Application.Features.Users
{
    public class UserGetMobileByIdQueryValidator : AbstractValidator<UserGetMobileByIdQuery>
    {
        public UserGetMobileByIdQueryValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }
    }
}
