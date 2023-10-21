using Application.Validators;
using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class DownloadByIdQueryValidator : AbstractValidator<DownloadByIdQuery>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public DownloadByIdQueryValidator()
        {
            Include(new IBaseEntityDTOValidator());
        }
    }
}
