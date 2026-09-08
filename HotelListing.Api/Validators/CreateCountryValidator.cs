using FluentValidation;
using HotelListing.Api.DTOs.Hotel;


namespace HotelListing.Api.Validators
{
    public class CreateCountryValidator : AbstractValidator<CreateCountryDto>
    {
        public CreateCountryValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Country name is required.")
                .MaximumLength(100).WithMessage("Country name cannot exceed 100 characters.");
            
            RuleFor(c => c.ShortName)
                .NotEmpty().WithMessage("Short name is required.")
                .MaximumLength(3).WithMessage("Short name cannot exceed 3 characters.");
        }
    }
}