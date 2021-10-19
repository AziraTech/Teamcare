using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
using teamcare.common.Enumerations;
using teamcare.data.Entities;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    public class BloodPressureController : BaseController
    {
        private readonly IBloodPressureReadingService _bloodPressureReadingService;
        private readonly IAuditService _auditService;
        private readonly IServiceUserService _serviceUserService;


        public BloodPressureController(IBloodPressureReadingService bloodPressureReadingService,
                                          IAuditService auditService,
                                          IServiceUserService serviceUserService
                                     )
        {
            _bloodPressureReadingService = bloodPressureReadingService;
            _auditService = auditService;
            _serviceUserService = serviceUserService;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBloodReadingData(Guid id, string daterange)
        {
            try
            {
                var bloodreadingdata = await _bloodPressureReadingService.ListAllSortedFiltered(id, daterange);

                var model = new BloodPressureReadingViewModel
                {
                    BloodPressureReadingList = bloodreadingdata.ToList()
                };

                return PartialView("~/Views/BloodPressureReading/_BloodPressureReadingList.cshtml", model);

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> BloodModalBind(string id)
        {
            try
            {
                var model = new BloodPressureReadingViewModel();

                if (!string.IsNullOrEmpty(id))
                {
                    var bloodData = await _bloodPressureReadingService.GetByIdAsync(new Guid(id));

                    model = new BloodPressureReadingViewModel
                    {
                        BloodPressureReading = bloodData
                    };
                }
                else
                {
                    model = new BloodPressureReadingViewModel
                    {
                        BloodPressureReading = new BloodPressureReadingModel()
                    };
                }
                return PartialView("~/Views/BloodPressureReading/_CreateBloodPressureReading.cshtml", model);
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(BloodPressureReadingViewModel bloodPressureReadingViewModel)
        {
            try
            {
                if (bloodPressureReadingViewModel?.BloodPressureReading != null)
                {
                    var serviceuser = await _serviceUserService.GetByIdAsync(bloodPressureReadingViewModel.BloodPressureReading.ServiceUserId);

                    var bloodPressureReading = new BloodPressureReadingModel();

                    var oldblooddata = await _bloodPressureReadingService.ListAllAsync();

                    var IsGetTodayData = oldblooddata.Where(x => x.CreatedOn.Date == DateTimeOffset.UtcNow.Date).FirstOrDefault();

                    bloodPressureReadingViewModel.BloodPressureReading.TestDate = DateTime.ParseExact(bloodPressureReadingViewModel.BloodPressureReading.BloodTestdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                    if (IsGetTodayData ==null)
                    {
                        bloodPressureReading = await _bloodPressureReadingService.AddAsync(bloodPressureReadingViewModel.BloodPressureReading);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = serviceuser.FirstName + " " + serviceuser.LastName + " Blood Pressure Reading has been created.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        bloodPressureReadingViewModel.BloodPressureReading.Id = IsGetTodayData.Id;

                        bloodPressureReading = await _bloodPressureReadingService.UpdateAsync(bloodPressureReadingViewModel.BloodPressureReading);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = serviceuser.FirstName + " " + serviceuser.LastName + " Blood Pressure Reading has been updated.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                }
                return Json(new { statuscode = 1 });
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var data = await _bloodPressureReadingService.GetByIdAsync(id);

                await _bloodPressureReadingService.DeleteAsync(data);

                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }
    }
}
