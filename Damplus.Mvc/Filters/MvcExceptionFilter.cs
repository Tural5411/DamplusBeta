//using Damplus.Shared.Entities.Concrete;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Filters;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.Extensions.Hosting;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlTypes;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Damplus.Mvc.Filters
//{
//    public class MvcExceptionFilter : IExceptionFilter
//    {
//        private readonly IHostEnvironment _hostEnvironment;
//        private readonly IModelMetadataProvider _modelMetadataProvider;
//        public MvcExceptionFilter(IHostEnvironment hostEnvironment, IModelMetadataProvider modelMetadataProvider)
//        {
//            _hostEnvironment = hostEnvironment;
//            _modelMetadataProvider = modelMetadataProvider;
//        }



//        public void OnException(ExceptionContext context)
//        {
//            if (_hostEnvironment.IsDevelopment())
//            {
//                context.ExceptionHandled = true;
//                var mvcErrorModel = new MvcErrorModel();
//                switch (context.Exception)
//                {
//                    case SqlNullValueException:
//                        mvcErrorModel.Message = 
//                            $"Üzürlü sayın, əməliyyat zamanı gözlənilməz bir databaza xətası baş verdi.Xəta ən qısa zamanda həll ediləcəkdir.";
//                        mvcErrorModel.Detail = context.Exception.Message;
//                        break;
//                    case NullReferenceException:
//                        mvcErrorModel.Message =
//                            $"Üzürlü sayın, əməliyyat zamanı gözlənilməz bir null dəyər xətası baş verdi.Xəta ən qısa zamanda həll ediləcəkdir.";
//                        mvcErrorModel.Detail = context.Exception.Message;
//                        break;
//                    default :
//                        mvcErrorModel.Message =
//                            $"Üzürlü sayın, əməliyyat zamanı gözlənilməz xəta baş verdi.Xəta ən qısa zamanda həll ediləcəkdir.";
//                        break;
//                }
//                var result = new ViewResult { ViewName = "Error" };
//                result.StatusCode = 500;
//                result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState);
//                result.ViewData.Add("MvcErrorModel", mvcErrorModel);
//                context.Result = result;
//            }
//        }
//    }
//}
