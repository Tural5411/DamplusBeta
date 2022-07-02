using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NToastNotify;
using Damplus.Entities.Concrete;
using Damplus.Mvc.Areas.Admin.Models;
using Damplus.Shared.Utilities.Helpers.Abstract;
using Damplus.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Damplus.Mvc.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OptionController : Controller
    {
        private readonly SmmPacket _smmPacket;
        private readonly IWritableOptions<SmmPacket> _smmPacketWriter;
        private readonly ChooseUsPageInfo _chooseUsPageInfo;
        private readonly IWritableOptions<ChooseUsPageInfo> _chooseUsPageInfoWriter;
        private readonly SliderPageInfo _sliderPageInfo;
        private readonly IWritableOptions<SliderPageInfo> _sliderPageInfoWriter;
        private readonly AboutUsPageInfo _aboutUsPageInfo;
        private readonly IWritableOptions<AboutUsPageInfo> _aboutUsPageInfoWriter;
        private readonly IToastNotification _toastNotification;
        private readonly WebsiteInfo _websiteInfo;
        private readonly IWritableOptions<WebsiteInfo> _websiteInfoWriter;
        private readonly SmtpSettings _smtpSettings;
        private readonly IWritableOptions<SmtpSettings> _smtpSettingsWriter;
        private readonly ArticleRightSideBarWidgetOptions _articleRightSideBarWidgetOptions;
        private readonly IWritableOptions<ArticleRightSideBarWidgetOptions> _articleRightSideBarWidgetOptionsWriter;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public OptionController(IOptionsSnapshot<SmmPacket> smmPacket,
            IWritableOptions<SmmPacket> smmPacketWriter,
            IOptionsSnapshot<AboutUsPageInfo> aboutUsPageInfo,
            IWritableOptions<AboutUsPageInfo> aboutUsPageInfoWriter,
            IOptionsSnapshot<SliderPageInfo> sliderPageInfo,
            IWritableOptions<SliderPageInfo> sliderPageInfoWriter,
            IOptionsSnapshot<ChooseUsPageInfo> chooseUsPageInfo,
            IWritableOptions<ChooseUsPageInfo> chooseUsPageInfoWriter,
            IToastNotification toastNotification, IOptionsSnapshot<WebsiteInfo> websiteInfo,
            IWritableOptions<WebsiteInfo> websiteInfoWriter, IOptionsSnapshot<SmtpSettings> smtpSettings,
            IWritableOptions<SmtpSettings> smtpSettingsWriter,
            IOptionsSnapshot<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptions,
            IWritableOptions<ArticleRightSideBarWidgetOptions> articleRightSideBarWidgetOptionsWriter,
            ICategoryService categoryService, IMapper mapper)
        {
            _smmPacket = smmPacket.Value;
            _smmPacketWriter = smmPacketWriter;
            _chooseUsPageInfo = chooseUsPageInfo.Value;
            _chooseUsPageInfoWriter = chooseUsPageInfoWriter;
            _sliderPageInfo = sliderPageInfo.Value;
            _sliderPageInfoWriter = sliderPageInfoWriter;
            _aboutUsPageInfo = aboutUsPageInfo.Value;
            _aboutUsPageInfoWriter = aboutUsPageInfoWriter;
            _toastNotification = toastNotification;
            _websiteInfo = websiteInfo.Value;
            _websiteInfoWriter = websiteInfoWriter;
            _smtpSettings = smtpSettings.Value;
            _smtpSettingsWriter = smtpSettingsWriter;
            _articleRightSideBarWidgetOptions = articleRightSideBarWidgetOptions.Value;
            _articleRightSideBarWidgetOptionsWriter = articleRightSideBarWidgetOptionsWriter;
            _categoryService = categoryService;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Slider()
        {
            return View(_sliderPageInfo);
        }
        [HttpPost]
        public IActionResult Slider(SliderPageInfo sliderPageInfo)
        {
            if (ModelState.IsValid)
            {
                _sliderPageInfoWriter.Update(x =>
                {
                    x.Header = sliderPageInfo.Header;
                    x.Content = sliderPageInfo.Content;
                });
                _toastNotification.AddSuccessToastMessage("Slider bölməsi uğurla editləndi.", new ToastrOptions
                {
                    Title = "Uğurlu Əməliyyat"
                });
                return View(sliderPageInfo);
            }
            return View(sliderPageInfo);
        }
        public IActionResult Choose()
        {
            return View(_chooseUsPageInfo);
        }
        [HttpPost]
        public IActionResult Choose(ChooseUsPageInfo chooseUsPageInfo)
        {
            if (ModelState.IsValid)
            {
                _chooseUsPageInfoWriter.Update(x =>
                {
                    x.Header = chooseUsPageInfo.Header;
                    x.ContentFirst = chooseUsPageInfo.ContentFirst;
                    x.ContentSecond = chooseUsPageInfo.ContentSecond;
                    x.ContentThird = chooseUsPageInfo.ContentThird;
                    x.ContentFourth = chooseUsPageInfo.ContentFourth;
                });
                _toastNotification.AddSuccessToastMessage("ChooseUs bölməsi uğurla editləndi.", new ToastrOptions
                {
                    Title = "Uğurlu Əməliyyat"
                });
                return View(chooseUsPageInfo);
            }
            return View(chooseUsPageInfo);
        }
        [HttpGet]
        public IActionResult About()
        {
            return View(_aboutUsPageInfo);
        }
        [HttpPost]
        public IActionResult About(AboutUsPageInfo aboutUsPageInfo)
        {
            if (ModelState.IsValid)
            {
                _aboutUsPageInfoWriter.Update(x =>
                {
                    x.Header = aboutUsPageInfo.Header;
                    x.Content = aboutUsPageInfo.Content;
                    x.SeoAuthor = aboutUsPageInfo.SeoAuthor;
                    x.SeoDescription = aboutUsPageInfo.SeoDescription;
                    x.SeoTags = aboutUsPageInfo.SeoTags;
                });
                _toastNotification.AddSuccessToastMessage("Haqqında bölməsi uğurla editləndi.", new ToastrOptions
                {
                    Title = "Uğurlu Əməliyyat"
                });
                return View(aboutUsPageInfo);
            }
            return View(aboutUsPageInfo);
        }
        [HttpGet]
        public IActionResult GeneralSettings()
        {
            return View(_websiteInfo);
        }
        [HttpPost]
        public IActionResult GeneralSettings(WebsiteInfo websiteInfo)
        {
            if (ModelState.IsValid)
            {
                _websiteInfoWriter.Update(x =>
                {
                    x.Title = websiteInfo.Title;
                    x.MenuTitle = websiteInfo.MenuTitle;
                    x.SeoAuthor = websiteInfo.SeoAuthor;
                    x.SeoDescription = websiteInfo.SeoDescription;
                    x.SeoTags = websiteInfo.SeoTags;
                    x.Facebook = websiteInfo.Facebook;
                    x.Instagram = websiteInfo.Instagram;
                    x.Youtube = websiteInfo.Youtube;
                    x.Whatsapp = websiteInfo.Whatsapp;
                    x.Phone = websiteInfo.Phone;
                });
                _toastNotification.AddSuccessToastMessage("Esas emeliyyatlar bölməsi uğurla editləndi.", new ToastrOptions
                {
                    Title = "Uğurlu Əməliyyat"
                });
                return View(websiteInfo);
            }
            return View(websiteInfo);
        }
        [HttpGet]
        public IActionResult EmailSettings()
        {
            return View(_smtpSettings);
        }
        [HttpPost]
        public IActionResult EmailSettings(SmtpSettings smtpSettings)
        {
            if (ModelState.IsValid)
            {
                _smtpSettingsWriter.Update(x =>
                {
                    x.Server = smtpSettings.Server;
                    x.Port = smtpSettings.Port;
                    x.SenderEmail = smtpSettings.SenderEmail;
                    x.SenderName = smtpSettings.SenderName;
                    x.Username = smtpSettings.Username;
                    x.Password = smtpSettings.Password;
                });
                _toastNotification.AddSuccessToastMessage("Saytin Email(Smtp) emeliyyatlar bölməsi uğurla editləndi.", new ToastrOptions
                {
                    Title = "Uğurlu Əməliyyat"
                });
                return View(smtpSettings);
            }
            return View(smtpSettings);
        }

        [HttpGet]
        public async Task<IActionResult> ArticleRightSideBarWidgetSettings(int categoryId)
        {
            var categoriesResult = await _categoryService.GetAllByNonDeleteAndActive();
            var articleRightSiderBarWidgetOptionsViewModel = _mapper.Map<ArticleRightSideBarWidgetOptionsViewModel>(_articleRightSideBarWidgetOptions);
            articleRightSiderBarWidgetOptionsViewModel.Categories = categoriesResult.Data.Categories;
            return View(articleRightSiderBarWidgetOptionsViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> ArticleRightSideBarWidgetSettings(ArticleRightSideBarWidgetOptionsViewModel articleRightSideBarWidgetOptions)
        {
            if (ModelState.IsValid)
            {
                var categoriesResult = await _categoryService.GetAllByNonDeleteAndActive();
                articleRightSideBarWidgetOptions.Categories = categoriesResult.Data.Categories;
                _articleRightSideBarWidgetOptionsWriter.Update(x =>
                {
                    x.Header = articleRightSideBarWidgetOptions.Header;
                    x.CategoryId = articleRightSideBarWidgetOptions.CategoryId;
                    x.TakeSize = articleRightSideBarWidgetOptions.TakeSize;
                    x.FilterBy = articleRightSideBarWidgetOptions.FilterBy;
                    x.OrderBy = articleRightSideBarWidgetOptions.OrderBy;
                    x.IsAscending = articleRightSideBarWidgetOptions.IsAscending;
                    x.StartAt = articleRightSideBarWidgetOptions.StartAt;
                    x.EndAt = articleRightSideBarWidgetOptions.EndAt;
                    x.MaxViewCount = articleRightSideBarWidgetOptions.MaxViewCount;
                    x.MinViewCount = articleRightSideBarWidgetOptions.MinViewCount;
                    x.MaxCommentCount = articleRightSideBarWidgetOptions.MaxCommentCount;
                    x.MinCommentCount = articleRightSideBarWidgetOptions.MinCommentCount;
                });
                _toastNotification.AddSuccessToastMessage("Meqale sehifesinin widget emeliyyatlar bölməsi uğurla editləndi.", new ToastrOptions
                {
                    Title = "Uğurlu Əməliyyat"
                });
                return View(articleRightSideBarWidgetOptions);
            }
            return View(articleRightSideBarWidgetOptions);
        }
        public IActionResult PriceSmm()
        {
            return View(_smmPacket);
        }
        [HttpPost]
        public IActionResult PriceSmm(SmmPacket smmPacket)
        {
            if (ModelState.IsValid)
            {
                _smmPacketWriter.Update(x =>
                {
                    x.PacketFirstName = smmPacket.PacketFirstName;
                    x.PacketSecondName = smmPacket.PacketSecondName;
                    x.PacketThirdName = smmPacket.PacketThirdName;
                    x.PacketFourName = smmPacket.PacketFourName;
                    x.PacketFiveName = smmPacket.PacketFiveName;
                    x.PacketSixName = smmPacket.PacketSixName;
                    x.Text1 = smmPacket.Text1;
                    x.Text2 = smmPacket.Text2;
                    x.Text3 = smmPacket.Text3;
                    x.Text4 = smmPacket.Text4;
                    x.Text5 = smmPacket.Text5;
                    x.Text6 = smmPacket.Text6;
                    x.Text7 = smmPacket.Text7;
                    x.Text8 = smmPacket.Text8;
                    //1
                    x.Text9 = smmPacket.Text9;
                    x.Text10 = smmPacket.Text10;
                    x.Text11 = smmPacket.Text11;
                    x.Text12 = smmPacket.Text12;
                    x.Text13 = smmPacket.Text13;
                    x.Text14 = smmPacket.Text14;
                    x.Text15 = smmPacket.Text15;
                    x.Text16 = smmPacket.Text16;
                    //2
                    x.Text17 = smmPacket.Text17;
                    x.Text18 = smmPacket.Text18;
                    x.Text19 = smmPacket.Text19;
                    x.Text20 = smmPacket.Text20;
                    x.Text21 = smmPacket.Text21;
                    x.Text22 = smmPacket.Text22;
                    x.Text23 = smmPacket.Text23;
                    x.Text24 = smmPacket.Text24;
                    //3
                    x.Text25 = smmPacket.Text25;
                    x.Text26 = smmPacket.Text26;
                    x.Text27 = smmPacket.Text27;
                    x.Text28 = smmPacket.Text28;
                    x.Text29 = smmPacket.Text29;
                    x.Text30 = smmPacket.Text30;
                    x.Text31 = smmPacket.Text31;
                    x.Text32 = smmPacket.Text32;
                    //4
                    x.Text33 = smmPacket.Text33;
                    x.Text34 = smmPacket.Text34;
                    x.Text36 = smmPacket.Text35;
                    x.Text35 = smmPacket.Text36;
                    x.Text37 = smmPacket.Text37;
                    x.Text38 = smmPacket.Text38;
                    x.Text39 = smmPacket.Text39;
                    //5
                    x.Text40 = smmPacket.Text40;
                    x.Text41 = smmPacket.Text41;
                    x.Text42 = smmPacket.Text42;
                    x.Text43 = smmPacket.Text43;
                    x.Text44 = smmPacket.Text44;
                    x.Text45 = smmPacket.Text45;
                    x.Text46 = smmPacket.Text46;
                    x.Text47 = smmPacket.Text47;
                    x.Text48 = smmPacket.Text48;
                    //Price
                    x.PacketFirstPrice = smmPacket.PacketFirstPrice;
                    x.PacketSecondPrice = smmPacket.PacketSecondPrice;
                    x.PacketThirdPrice = smmPacket.PacketThirdPrice;
                    x.PacketFourthPrice = smmPacket.PacketFourthPrice;
                    x.PacketFivePrice = smmPacket.PacketFivePrice;
                    x.PacketSixPrice = smmPacket.PacketSixPrice;
                });
                _toastNotification.AddSuccessToastMessage("Qiymetler SMM bölməsi uğurla editləndi.", new ToastrOptions
                {
                    Title = "Uğurlu Əməliyyat"
                });
                return View(smmPacket);
            }
            return View(smmPacket);
        }
    }
}
