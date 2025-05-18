using FluentValidation;
using MyProject.DTOs;

namespace MyProject.Validators
{
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Name can't exceed 100 characters.");
        }
    }

    public class CategoryUpdateDTOValidator : AbstractValidator<CategoryUpdateDTO>
    {
        public CategoryUpdateDTOValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Name can't exceed 100 characters.");
        }
    }

}