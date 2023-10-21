using Application.Validators;
using FluentValidation;

namespace AuthServer.Application.Features.Users
{
    public class UserDeleteCommandValidator : AbstractValidator<UserDeleteCommand>
    {

        public UserDeleteCommandValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }


    }
}
