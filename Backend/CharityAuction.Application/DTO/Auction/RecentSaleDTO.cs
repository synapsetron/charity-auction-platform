using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityAuction.Application.DTO.Auction
{
    public class RecentSaleDTO
    {
        public string Title { get; set; } = string.Empty;
        public decimal SoldPrice { get; set; }
        public string FundName { get; set; } = "Фонд ЗСУ";
        public DateTime SoldAt { get; set; }
    }
}
