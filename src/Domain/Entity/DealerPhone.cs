using FluentValidation;

namespace Domain.Entity
{
    public class DealerPhone
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
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
