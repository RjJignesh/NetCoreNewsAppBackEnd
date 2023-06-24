namespace DataAccess.Dto.Validators
{
    using FluentValidation;
    public class NewsDtoValidators: AbstractValidator<NewsDto>
    {
        /// <summary>
        /// News Dto Validators
        /// </summary>
        public NewsDtoValidators()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(p => p.Detail).NotEmpty().WithMessage("Detail is required");
            RuleFor(p => p.NewsImage).NotEmpty().WithMessage("News image is required");            
        }
    }
}
