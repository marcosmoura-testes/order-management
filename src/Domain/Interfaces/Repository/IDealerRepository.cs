using Domain.Entity;

namespace Domain.Interfaces.Repository
{
    /// <summary>
    /// Interface for Dealer repository operations.
    /// </summary>
    public interface IDealerRepository : IBaseRepository<Dealer, int>
    {
        /// <summary>
        /// Checks if a dealer with the specified CNPJ exists.
        /// </summary>
        /// <param name="cnpj">The CNPJ of the dealer.</param>
        /// <returns>True if a dealer with the specified CNPJ exists, otherwise false.</returns>
        bool CNPJExists(string cnpj);

        /// <summary>
        /// Retrieves a dealer by its CNPJ.
        /// </summary>
        /// <param name="cnpj">The CNPJ of the dealer.</param>
        /// <returns>The dealer with the specified CNPJ.</returns>
        Dealer GetByCNPJ(string cnpj);
    }
}
