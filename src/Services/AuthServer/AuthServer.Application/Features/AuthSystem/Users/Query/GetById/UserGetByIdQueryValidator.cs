using Application.Validators;
using FluentValidation;

namespace AuthServer.Application.Features.Users
{
    public class UserGetByIdQueryValidator : AbstractValidator<UserGetByIdQuery>
    {
        public UserGetByIdQueryValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }
    }
}
