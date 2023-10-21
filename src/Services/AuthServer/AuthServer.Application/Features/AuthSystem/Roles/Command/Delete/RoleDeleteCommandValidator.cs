using Application.Validators;
using FluentValidation;

namespace AuthServer.Application.Features.Roles
{
    public class RoleDeleteCommandValidator : AbstractValidator<RoleDeleteCommand>
    {

        public RoleDeleteCommandValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }


    }
}
