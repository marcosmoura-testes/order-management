using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Domain.Entity
{
    public class DealerContact
    {
        public int Id { get; set; }
        public int DealerId { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public bool ContactDefault { get; set; }
    }
    public class DealerContactValidator : AbstractValidator<DealerContact>
    {
        public DealerContactValidator()
        {
            RuleFor(dealer => dealer.Name)
                .NotEmpty().WithMessage("Contact Name is required")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Contact Name must contain only letters and spaces");

            RuleFor(dealer => dealer.PhoneNumber)
                .NotEmpty().WithMessage("Phone Number is required")
                .Matches(@"^\(\d{2}\)\s\d{4,5}-\d{4}$").WithMessage("Phone Number is not valid");
        }
    }
}
