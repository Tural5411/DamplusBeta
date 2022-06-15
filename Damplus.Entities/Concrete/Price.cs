using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class Price:EntityBase
    {
        public string Header { get; set; }
        public string Icon { get; set; }
        public string PriceValue { get; set; }
        public string Content { get; set; }
        public string Text { get; set; }
        public string Text2 { get; set; }
        public string Text3 { get; set; }
        public string Text4 { get; set; }
        public string Text5 { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
