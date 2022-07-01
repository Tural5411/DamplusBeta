using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Damplus.Entities.Concrete;
using Damplus.Mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.ViewComponents
{
    public class SliderViewComponent : ViewComponent
    {
        private readonly SliderPageInfo _sliderPageInfo;
        public SliderViewComponent(IOptionsSnapshot<SliderPageInfo> sliderPageInfo)
        {
            _sliderPageInfo = sliderPageInfo.Value;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(_sliderPageInfo);
        }
    }
}
