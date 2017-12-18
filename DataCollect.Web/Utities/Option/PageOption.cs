using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Utities.Option
{
    public class PageOption
    {

        public PageOption(PageOption option)
        {
            this.Page = option.Page;
            this.Size = option.Size;
            this.Count = option.Count;
        }

        public PageOption() { }

        public int Page { get; set; }
        public int Size { get; set; }
        public int Count { get; set; }
    }
}
