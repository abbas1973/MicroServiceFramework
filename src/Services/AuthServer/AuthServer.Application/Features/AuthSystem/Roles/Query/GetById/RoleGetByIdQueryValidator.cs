using Application.Validators;
using FluentValidation;

namespace AuthServer.Application.Features.Roles
{
    public class RoleGetByIdQueryValidator : AbstractValidator<RoleGetByIdQuery>
    {
        public RoleGetByIdQueryValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }
    }
}
