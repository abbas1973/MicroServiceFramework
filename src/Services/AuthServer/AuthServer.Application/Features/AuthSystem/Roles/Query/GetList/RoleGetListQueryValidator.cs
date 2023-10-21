using Application.Validators;
using FluentValidation;
using Resources;

namespace AuthServer.Application.Features.Roles
{
    public class RoleGetListQueryValidator : AbstractValidator<RoleGetListQuery>
    {
        public RoleGetListQueryValidator()
        {
            Include(new IPaginationDTOValidator());
        }


    }
}
