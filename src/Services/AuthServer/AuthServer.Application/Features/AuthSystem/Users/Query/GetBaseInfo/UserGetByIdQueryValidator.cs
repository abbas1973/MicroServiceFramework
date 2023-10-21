using Application.Validators;
using FluentValidation;

namespace AuthServer.Application.Features.Users
{
    public class UserGetBaseInfoQueryValidator : AbstractValidator<UserGetBaseInfoQuery>
    {
        public UserGetBaseInfoQueryValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }
    }
}
