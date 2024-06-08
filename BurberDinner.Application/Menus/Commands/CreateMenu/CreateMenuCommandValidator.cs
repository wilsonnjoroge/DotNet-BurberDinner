using BurberDinner.Application.Menus.Commands.CreateMenu;
using FluentValidation;

public class CreateMenuCommandValidator : AbstractValidator<CreateMenuCommand>
{
    public CreateMenuCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required.");
        
        // Validate only when AverageRating is not null
        RuleFor(x => x.AverageRating)
            .InclusiveBetween(0, 5)
            .When(x => x.AverageRating.HasValue);
        
        RuleForEach(x => x.Sections).SetValidator(new MenuSectionCommandValidator());
    }
}

public class MenuSectionCommandValidator : AbstractValidator<MenuSectionCommand>
{
    public MenuSectionCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Section name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Section description is required.");
        RuleForEach(x => x.Items).SetValidator(new MenuItemCommandValidator());
    }
}

public class MenuItemCommandValidator : AbstractValidator<MenuItemCommand>
{
    public MenuItemCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Item name is required.");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Item description is required.");
    }
}
