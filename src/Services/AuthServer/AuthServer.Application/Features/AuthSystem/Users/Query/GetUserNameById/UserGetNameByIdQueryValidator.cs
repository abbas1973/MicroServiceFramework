using Application.Validators;
using FluentValidation;

namespace AuthServer.Application.Features.Users
{
    public class UserGetUserNameByIdQueryValidator : AbstractValidator<UserGetUserNameByIdQuery>
    {
        public UserGetUserNameByIdQueryValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }
    }
}
