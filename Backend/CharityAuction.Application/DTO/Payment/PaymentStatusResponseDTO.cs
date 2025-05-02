using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityAuction.Application.DTO.Payment
{
    public class PaymentStatusResponseDTO
    {
        public bool IsSuccessful { get; set; }
        public string Status { get; set; } // e.g., "pending", "paid", "failed"
        public string Provider { get; set; }
    }
}
