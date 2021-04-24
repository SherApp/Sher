using FluentValidation;

namespace Sher.Application.Files.UploadFile
{
    public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Id cannot be empty");
            RuleFor(x => x.UserId).NotEmpty().WithMessage("UploaderId cannot be empty");
            RuleFor(x => x.FileName).NotEmpty().WithMessage("FileName cannot be empty");
            RuleFor(x => x.FileStream).NotNull().WithMessage("FileStream cannot be null");
        }
    }
}