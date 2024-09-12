using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Pets.Contracts.Requests;

namespace Pets.Application.Validators;

public class CreatePetRequestValidator : AbstractValidator<CreatePetRequest>
{   
    public CreatePetRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("Name is required")
           .MaximumLength(50)
           .WithMessage("Name must be less than 50 characters");

        RuleFor(x => x.PetTypeId).NotEqual(default(int))
                .WithMessage("Pet Type is required");

        RuleFor(x => x.MissingSince).NotEqual(default(DateTime))
                .WithMessage("Missing Since is required");

        RuleFor(x => x.MissingSince).LessThan(DateTime.UtcNow)
                .WithMessage("Missing Since must be less than current date");

        RuleFor(x => x.Description)
                .MaximumLength(100)
                .WithMessage("Description must be less than 100 characters");

        RuleFor(x => x.MicroChipId)
               .MaximumLength(20)
               .WithMessage("MicroChipId must be less than 20 characters");
    }   
}

public class UpdatePetRequestValidator : AbstractValidator<UpdatePetRequest>
{
    public UpdatePetRequestValidator()
    {
        RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("Name is required")
           .MaximumLength(50)
           .WithMessage("Name must be less than 50 characters");

        RuleFor(x => x.PetTypeId).NotEqual(default(int))
                .WithMessage("Pet Type is required");

        RuleFor(x => x.MissingSince).NotEqual(default(DateTime))
                .WithMessage("Missing Since is required");

        RuleFor(x => x.MissingSince).LessThan(DateTime.UtcNow)
                .WithMessage("Missing Since must be less than current date");

        RuleFor(x => x.Description)
                .MaximumLength(100)
                .WithMessage("Description must be less than 100 characters");

        RuleFor(x => x.MicroChipId)
               .MaximumLength(20)
               .WithMessage("MicroChipId must be less than 20 characters");
    }
}

