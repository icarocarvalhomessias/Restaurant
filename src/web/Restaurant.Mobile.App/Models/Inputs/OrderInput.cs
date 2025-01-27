using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Mobile.App.Models.Inputs
{
    public class OrderInput
    {
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }
        public string ClientPhone { get; set; }
        public List<Guid> ProductIds { get; init; }
    }
}
