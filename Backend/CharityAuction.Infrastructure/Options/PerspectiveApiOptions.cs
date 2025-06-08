using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CharityAuction.Infrastructure.Options
{
    public class PerspectiveApiOptions
    {
        public const string SectionName = "PerspectiveApi";
        public string ApiKey { get; set; } = string.Empty;
    }
}
