using Application.Validators;
using FluentValidation;

namespace AuthServer.Application.Features.Users
{
    public class UserGetNameByIdQueryValidator : AbstractValidator<UserGetNameByIdQuery>
    {
        public UserGetNameByIdQueryValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }
    }
}
