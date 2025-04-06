using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ObjectValue
{
    public enum OrderStatusEnum
    {
        [Display(Name = "Pendente")]
        Pendente = 1,

        [Display(Name = "Aprovado")]
        Aprovad,

        [Display(Name = "Solicitado")]
        Solicitado,

        [Display(Name = "Em separação")]
        EmSeparacao,

        [Display(Name = "Enviado")]
        Enviado,

        [Display(Name = "Entregue")]
        Entregue,

        [Display(Name = "Cancelado")]
        Cancelado 
    }
}
