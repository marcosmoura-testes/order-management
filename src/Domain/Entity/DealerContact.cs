using FluentValidation;

namespace Domain.Entity
{
    /// <summary>
    /// Represents a contact associated with a dealer.
    /// </summary>
    public class DealerContact
    {
        /// <summary>
        /// The unique identifier for the dealer contact.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The identifier of the dealer associated with this contact.
        /// </summary>
        public int DealerId { get; set; }

        /// <summary>
        /// The name of the dealer contact.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Indicates whether this contact is the default contact for the dealer.
        /// </summary>
        public bool ContactDefault { get; set; }
    }

    public class DealerContactValidator : AbstractValidator<DealerContact>
    {
        public DealerContactValidator()
        {
            RuleFor(dealer => dealer.Name)
                .NotEmpty().WithMessage("Contact Name is required")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Contact Name must contain only letters and spaces");
        }
    }
}
