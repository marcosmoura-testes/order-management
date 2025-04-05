using FluentValidation;

namespace Domain.Entity
{
    public class DealerContact
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public string Name { get; set; }
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
