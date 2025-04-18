﻿using System.Text.RegularExpressions;
using FluentValidation;

namespace Domain.Entity
{
    /// <summary>
    /// Represents a dealer entity.
    /// </summary>
    public class Dealer
    {
        /// <summary>
        /// The unique identifier for the dealer.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the dealer.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The CNPJ of the dealer.
        /// </summary>
        public string CNPJ { get; set; }

        /// <summary>
        /// The Razao Social of the dealer.
        /// </summary>
        public string RazaoSocial { get; set; }

        /// <summary>
        /// The Nome Fantasia of the dealer.
        /// </summary>
        public string NomeFantasia { get; set; }

        /// <summary>
        /// The email address of the dealer.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The list of phone numbers associated with the dealer.
        /// </summary>
        public List<DealerPhone> PhonesDealer { get; set; } = new List<DealerPhone>();

        /// <summary>
        /// The list of contacts associated with the dealer.
        /// </summary>
        public List<DealerContact> ContacstDealer { get; set; } = new List<DealerContact>();

        /// <summary>
        /// The list of delivery addresses associated with the dealer.
        /// </summary>
        public List<DealerDeliveryAddress> DealerDeliveryAddress { get; set; } = new List<DealerDeliveryAddress>();
    }

    public class DealerValidator : AbstractValidator<Dealer>
    {
        public DealerValidator()
        {
            RuleFor(dealer => dealer.CNPJ)
           .NotEmpty().WithMessage("CNPJ is required")
           .Must(BeValidCNPJ).WithMessage("Invalid CNPJ");

            RuleFor(dealer => dealer.RazaoSocial)
                .NotEmpty().WithMessage("Razao Social is required")
                .Must(BeValidRazaoSocialFantasia).WithMessage("Razao Social is inválid");

            RuleFor(dealer => dealer.NomeFantasia)
                .NotEmpty().WithMessage("Nome Fantasia is required")
                .Must(BeValidRazaoSocialFantasia).WithMessage("Nome Fantasia is inválid");

            RuleFor(dealer => dealer.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid Email");

            RuleFor(dealer => dealer.ContacstDealer)
                .NotNull().WithMessage("ContacstDealer is required")
                .Must(contacts => contacts != null && contacts.Count > 0).WithMessage("ContacstDealer must have at least one contact");

            RuleFor(dealer => dealer.DealerDeliveryAddress)
                .NotNull().WithMessage("DealerDeliveryAddress is required")
                .Must(addresses => addresses != null && addresses.Count > 0).WithMessage("DealerDeliveryAddress must have at least one address");

        }

        private bool BeValidRazaoSocialFantasia(string razaoSocial)
        {
            if (string.IsNullOrWhiteSpace(razaoSocial))
                return false;

            if (razaoSocial.Length < 5)
                return false;

            if (!Regex.IsMatch(razaoSocial, @"^[\p{L}0-9\s]+$"))
                return false;

            return true;
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
