using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
using teamcare.common.Enumerations;

namespace teamcare.web.app.Controllers
{
	public class DocumentUploadController : BaseController
	{
		private readonly IFileUploadService _fileUploadService;
		private readonly IUserService _userService;
		public Guid userName;


		public DocumentUploadController(IFileUploadService fileUploadService, IUserService userService)
		{
			_fileUploadService = fileUploadService;
			_userService = userService;

		}

		[HttpPost]
		public async Task<IActionResult> Index()
		{
			var req = HttpContext.Request.Form;
            FileUploadModel documentResult = null;
			if (req.Files != null && req.Files.Count > 0)
            {
				var tempUser = User.FindFirstValue(common.ReferenceData.ClaimTypes.PreferredUsername);
				userName = await _userService.GetUserGuidAsync(tempUser);

				await using var ms = new MemoryStream();
                await req.Files[0].CopyToAsync(ms);
                var fileBytes = ms.ToArray();
                documentResult = await _fileUploadService.AddAsync(new FileUploadModel
                {
                    DocumentBytes = fileBytes,
					FileName = req.Files[0].FileName,
					FileSizeInBytes = req.Files[0].Length,
					ContentType = req.Files[0].ContentType,
					IsTemporary = true
				});
            }
			return Json(documentResult?.Id);
		}
	}
}
