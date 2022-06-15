using Damplus.Shared.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Damplus.Entities.Concrete
{
    public class Business:EntityBase
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ThumbNail { get; set; }
        public DateTime Date { get; set; }
        public string SeoAuthor { get; set; }
        public string SeoDescription { get; set; }
        public string SeoTags { get; set; }
        public string FileName { get; set; }
    }
}
