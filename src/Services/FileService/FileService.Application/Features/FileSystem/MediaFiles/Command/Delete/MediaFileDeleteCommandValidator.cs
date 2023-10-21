using Application.Validators;
using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class MediaFileDeleteCommandValidator : AbstractValidator<MediaFileDeleteCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");
        private string _errorMaxLength = string.Format(Messages.ErrorMaxLength, "{PropertyName}", "{MaxLength}");

        public MediaFileDeleteCommandValidator()
        {
            Include(new IBaseEntityDTOValidator());

        }
    }
}
