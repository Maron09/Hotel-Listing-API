using FluentValidation;
using HotelListing.Api.DTOs.Hotel;



namespace HotelListing.Api.Validators
{
    public class UpdateHotelValidator : AbstractValidator<UpdateHotelDto>
    {
        public UpdateHotelValidator()
        {
            RuleFor(h => h.Name)
                .MaximumLength(100).WithMessage("Hotel name cannot exceed 100 characters.")
                .When(h => h.Name != null); // only validate if provided

            RuleFor(h => h.Address)
                .MaximumLength(500).WithMessage("Address cannot exceed 500 characters.")
                .When(h => h.Address != null);

            RuleFor(h => h.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.")
                .When(h => h.Rating.HasValue);
        }
    }
}