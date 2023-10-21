using FluentValidation;
using Resources;

namespace FileService.Application.Features.MediaFiles
{
    public class PictureUploadCommandValidator : AbstractValidator<FileUploadCommand>
    {
        private string _errorRequired = string.Format(Messages.ErrorRequired, "{PropertyName}");

        public PictureUploadCommandValidator()
        {
            RuleFor(x => x.Files).NotNull().WithMessage(_errorRequired)
                .NotEmpty().WithMessage(_errorRequired);


            RuleFor(x => x.Group).NotNull().WithMessage(_errorRequired);


            //RuleFor(x => x.RelativePath).NotNull().WithMessage(_errorRequired)
            //    .NotEmpty().WithMessage(_errorRequired)
            //    .MaximumLength(100).WithMessage(string.Format(Messages.ErrorMaxLength, "{PropertyName}", "{MaxLength}"))
            //    .Must((model, relativePath, token) =>
            //    {
            //        if (string.IsNullOrEmpty(relativePath))
            //            return true;
            //        if (relativePath.Contains("\\") || relativePath.Contains("/") || relativePath == "..")
            //            return false;
            //        return true;
            //    }).WithMessage(string.Format(Messages.ErrorFormat, "{PropertyName}"));
        }
    }
}
