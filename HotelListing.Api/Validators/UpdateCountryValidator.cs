using FluentValidation;
using HotelListing.Api.DTOs.Hotel;


namespace HotelListing.Api.Validators
{
    public class UpdateCountryValidator : AbstractValidator<UpdateCountryDto>
    {
        public UpdateCountryValidator()
        {
            RuleFor(c => c.Name)
                .MaximumLength(100).WithMessage("Country name cannot exceed 100 characters.")
                .When(c => c.Name != null);
            
            RuleFor(c => c.ShortName)
                .MaximumLength(3).WithMessage("Short name cannot exceed 3 characters.")
                .When(c => c.ShortName != null);
        }
    }
}