using System.Text.RegularExpressions;
using FluentValidation;

namespace Domain.Entity
{
    /// <summary>
    /// Represents a client's order.
    /// </summary>
    public class ClientOrder
    {
        /// <summary>
        /// Unique identifier for the client order.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Date and time when the order was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// CNPJ of the client who placed the order.
        /// </summary>
        public string ClientCNPJ { get; set; }

        /// <summary>
        /// Identifier for the dealer associated with the order.
        /// </summary>
        public int DealerId { get; set; }

        /// <summary>
        /// Identifier for the status of the order.
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// Total amount of the order.
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// List of products in the client order.
        /// </summary>
        public List<ClientOrderProduct> CLientOrderProducts { get; set; } = new List<ClientOrderProduct>();
    }

    /// <summary>
    /// Validator for the ClientOrder class.
    /// </summary>
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
