using Application.Validators;
using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class MediaFileGetByIdQueryValidator : AbstractValidator<MediaFileGetByIdQuery>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public MediaFileGetByIdQueryValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }
    }
}
