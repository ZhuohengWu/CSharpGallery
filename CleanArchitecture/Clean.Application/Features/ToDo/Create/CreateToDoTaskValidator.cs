﻿
using FluentValidation;

namespace Clean.Application.Features.ToDo.Create;

public class CreateToDoTaskValidator : AbstractValidator<CreateToDoTask>
{
    public CreateToDoTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title required.")
            .Length(5, 100).WithMessage("Title must be between 5 and 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description required.");

        RuleFor(x => x.Priority)
            .NotEmpty().WithMessage("Priority is required.")
            .Must(priority => new[] { "Low", "Medium", "High" }.Contains(priority))
            .WithMessage("Priority must be one of the following: Low, Medium, High.");

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.Now).WithMessage("DueDate must be in the future.");

        RuleFor(x => x.IsCompleted)
            .NotNull().WithMessage("IsCompleted must be specified.");
    }
}
