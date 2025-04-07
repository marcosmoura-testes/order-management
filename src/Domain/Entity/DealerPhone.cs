using FluentValidation;

namespace Domain.Entity
{
    /// <summary>
    /// Represents a phone number associated with a dealer.
    /// </summary>
    public class DealerPhone
    {
        /// <summary>
        /// The unique identifier for the dealer phone.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The unique identifier for the dealer.
        /// </summary>
        public int DealerId { get; set; }

        /// <summary>
        /// The phone number of the dealer.
        /// </summary>
        public string PhoneNumber { get; set; }
    }

    public class DealerPhoneValidator : AbstractValidator<DealerPhone>
    {
        public DealerPhoneValidator()
        {
            RuleFor(dealer => dealer.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required")
                .Matches(@"^\(\d{2}\)\s\d{4,5}-\d{4}$").WithMessage("Phone Number is not valid");
        }
    }
}
