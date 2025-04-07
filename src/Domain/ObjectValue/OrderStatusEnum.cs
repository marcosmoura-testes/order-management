using System.ComponentModel.DataAnnotations;

namespace Domain.ObjectValue
{
    /// <summary>
    /// Enum representing the status of an order.
    /// </summary>
    public enum OrderStatusEnum
    {
        /// <summary>
        /// Order is pending.
        /// </summary>
        [Display(Name = "Pendente")]
        Pendente = 1,

        /// <summary>
        /// Order is approved.
        /// </summary>
        [Display(Name = "Aprovado")]
        Aprovado,

        /// <summary>
        /// Order is requested.
        /// </summary>
        [Display(Name = "Solicitado")]
        Solicitado,

        /// <summary>
        /// Order is being separated.
        /// </summary>
        [Display(Name = "Em separação")]
        EmSeparacao,

        /// <summary>
        /// Order is sent.
        /// </summary>
        [Display(Name = "Enviado")]
        Enviado,

        /// <summary>
        /// Order is delivered.
        /// </summary>
        [Display(Name = "Entregue")]
        Entregue,

        /// <summary>
        /// Order is canceled.
        /// </summary>
        [Display(Name = "Cancelado")]
        Cancelado
    }
}
