using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Damplus.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.ViewComponents
{
    public class AboutViewComponent : ViewComponent
    {
        private readonly AboutUsPageInfo _chooseUsPageInfo;
        public AboutViewComponent(IOptions<AboutUsPageInfo> chooseUsPageInfo)
        {
            _chooseUsPageInfo = chooseUsPageInfo.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_chooseUsPageInfo);
        }
    }
}
