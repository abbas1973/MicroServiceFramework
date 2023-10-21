using Application.Validators;
using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class MediaFileGetByIdsQueryValidator : AbstractValidator<MediaFileGetByIdsQuery>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public MediaFileGetByIdsQueryValidator()
        {
            RuleFor(x => x.Ids).NotNull().WithMessage(_errorRequired)
                .Must(ids => ids.Any()).WithMessage(_errorRequired);
        }
    }
}
