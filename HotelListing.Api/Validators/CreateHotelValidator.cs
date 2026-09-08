using FluentValidation;
using HotelListing.Api.DTOs.Hotel;


namespace HotelListing.Api.Validators
{
    public class CreateHotelValidator : AbstractValidator<CreateHotelDto>
    {
        public CreateHotelValidator()
        {
            RuleFor(h => h.Name)
                .NotEmpty().WithMessage("Hotel name is required.")
                .MaximumLength(100).WithMessage("Hotel name cannot exceed 100 characters.");

            RuleFor(h => h.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.");

            RuleFor(h => h.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(h => h.CountryId)
                .GreaterThan(0).WithMessage("A valid Country ID is required.");
        }
    }
}