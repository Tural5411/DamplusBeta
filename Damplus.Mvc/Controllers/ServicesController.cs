using Microsoft.AspNetCore.Mvc;
using Damplus.Mvc.Models;
using Damplus.Services.Abstract;
using Damplus.Shared.Utilities.Results.ComplexTypes;
using System.Threading.Tasks;

namespace Damplus.Mvc.Controllers
{
    public class ServicesController : Controller
    {
        private readonly IBusinessService _businessService;
        public ServicesController(IBusinessService businessService)
        {
            _businessService = businessService;
        }
        [Route("Xidmet")]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _businessService.GetAllByNonDeleteAndActive();
            return View(new BusinessViewModel
            {
                BusinessListDto = result.Data
            });
        }
        public async Task<IActionResult> Detail(int serviceId)
        {
            var serviceResult = await _businessService.Get(serviceId);
            var relationServices = await _businessService.GetAllByNonDeleteAndActive();
            if (serviceResult.ResultStatus == ResultStatus.Succes)
            {
                return View(new ServiceDetailViewModel
                {
                    BusinessDto = serviceResult.Data,
                    BusinessListDto=relationServices.Data,
                });
            }
            return NotFound();
        }
        //[Obsolete]
        //public async Task<IActionResult> DownloadFile(string filePath, int id)
        //{
        //    string path = Path.Combine(this._environment.WebRootPath, "files/") + filePath;
        //    byte[] bytes = System.IO.File.ReadAllBytes(path);
        //    return File(bytes, "application/octet-stream", filePath);
        //}
    }
}
