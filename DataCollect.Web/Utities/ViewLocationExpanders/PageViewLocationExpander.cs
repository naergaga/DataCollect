using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataCollect.Web.Utities.ViewLocationExpanders
{
    public class PageViewLocationExpander : IViewLocationExpander
    {
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            
            return viewLocations.Select(f =>
            {
               
                Debug.WriteLine("View-find:"+f);
                return f.Replace("/Views/", "/Pages/").Replace("/Shared/","/");
            });
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
        }

    }
}
