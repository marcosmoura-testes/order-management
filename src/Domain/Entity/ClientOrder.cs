using System.Text.RegularExpressions;
using FluentValidation;

namespace Domain.Entity
{
    public class ClientOrder
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ClientCNPJ { get; set; }
        public int DealerId { get; set; }
        public int StatusId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<ClientOrderProduct> CLientOrderProducts { get; set; } = new List<ClientOrderProduct>();
    }

    public class ClientOrderValidator : AbstractValidator<ClientOrder>
    {
        public ClientOrderValidator()
        {
            RuleFor(order => order.ClientCNPJ)
                .NotEmpty().WithMessage("ClientCNPJ is required")
                .Must(BeValidCNPJ).WithMessage("Invalid ClientCNPJ");

            RuleFor(order => order.DealerId)
                .GreaterThan(0).WithMessage("DealerId must be a positive integer");

            RuleFor(dealer => dealer.CLientOrderProducts)
             .NotNull().WithMessage("Products is required")
             .Must(products => products != null && products.Count > 0).WithMessage("Products must have at least one product");
        }

        private bool BeValidCNPJ(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                return false;

            // Remove máscara
            cnpj = Regex.Replace(cnpj, @"[^\d]", "");

            // Verifica se tem 14 dígitos
            if (cnpj.Length != 14)
                return false;

            // Validação do CNPJ
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCnpj = cnpj.Substring(0, 12);
            int soma = 0;

            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

            int resto = (soma % 11);
            int digito1 = resto < 2 ? 0 : 11 - resto;

            tempCnpj += digito1;
            soma = 0;

            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

            resto = (soma % 11);
            int digito2 = resto < 2 ? 0 : 11 - resto;

            string digitosVerificadores = digito1.ToString() + digito2.ToString();

            return cnpj.EndsWith(digitosVerificadores);
        }
    }
}
