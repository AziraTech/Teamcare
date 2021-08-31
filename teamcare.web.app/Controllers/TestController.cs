using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;

namespace teamcare.web.app.Controllers
{
    public class TestController : BaseController
    {
        private readonly IServiceUserService _serviceUserService;
		private readonly IFileUploadService _fileUploadService;

		public TestController(IServiceUserService serviceUserService,IFileUploadService fileUploadService)
        {
            _serviceUserService = serviceUserService;
			_fileUploadService = fileUploadService;
		}
        public async Task<IActionResult> Index()
        {
            var returnDoc = await _serviceUserService.GetByIdAsync(Guid.Parse("2afade13-c93c-4c90-9cdc-fee6f0b95f71"),new Guid());
            returnDoc.KnownAs = DateTime.Now.Ticks.ToString();
            FileUploadModel model = new FileUploadModel();
            model.BlobName = returnDoc.DocumentUploads.FirstOrDefault().BlobName;
            model.FileName = returnDoc.DocumentUploads.FirstOrDefault().FileName;
            var byyes = _fileUploadService.GetBlobAsync(model,new Guid());
            var updated = await _serviceUserService.UpdateAsync(returnDoc,new Guid());

            return Ok();
        }
    }
}
