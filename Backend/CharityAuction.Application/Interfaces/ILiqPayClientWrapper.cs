using LiqPay.SDK.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityAuction.Application.Interfaces
{
    public interface ILiqPayClientWrapper
    {
        Task<LiqPayResponse> RequestAsync(string path, LiqPayRequest request);
    }
}
