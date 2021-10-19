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
    public class WeightReadingController : BaseController
    {
        private readonly IWeightReadingService _weightReadingService;
        private readonly IAuditService _auditService;
        private readonly IServiceUserService _serviceUserService;


        public WeightReadingController(IWeightReadingService weightReadingService,
                                          IAuditService auditService,
                                          IServiceUserService serviceUserService
                                     )
        {
            _weightReadingService = weightReadingService;
            _auditService = auditService;
            _serviceUserService = serviceUserService;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetWeightReadingData(Guid id, string daterange)
        {
            try
            {
                var weightreadingdata = await _weightReadingService.ListFiltered(id, daterange);


                var model = new WeightReadingViewModel
                {
                    WeightReadingList = weightreadingdata.ToList()
                };

                return PartialView("~/Views/WeightReading/_WeightReadingList.cshtml", model);

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> WeightModalBind(string id)
        {
            try
            {
                var model = new WeightReadingViewModel();

                if (!string.IsNullOrEmpty(id))
                {
                    var bloodData = await _weightReadingService.GetByIdAsync(new Guid(id));

                    model = new WeightReadingViewModel
                    {
                        WeightReading = bloodData
                    };
                }
                else
                {
                    model = new WeightReadingViewModel
                    {
                        WeightReading = new WeightReadingModel()
                    };
                }
                return PartialView("~/Views/WeightReading/_CreateWeightReading.cshtml", model);
            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Save(WeightReadingViewModel weightReadingViewModel)
        {
            try
            {
                if (weightReadingViewModel?.WeightReading != null)
                {
                    var serviceuser = await _serviceUserService.GetByIdAsync(weightReadingViewModel.WeightReading.ServiceUserId);

                    var bloodPressureReading = new WeightReadingModel();

                    var oldblooddata = await _weightReadingService.ListAllAsync();

                    var IsGetTodayData = oldblooddata.Where(x => x.CreatedOn.Date == DateTimeOffset.UtcNow.Date).FirstOrDefault();

                    weightReadingViewModel.WeightReading.TestDate = DateTime.ParseExact(weightReadingViewModel.WeightReading.WeightTestdate, "dd/MM/yyyy", CultureInfo.InvariantCulture);


                    if (IsGetTodayData ==null)
                    {
                        bloodPressureReading = await _weightReadingService.AddAsync(weightReadingViewModel.WeightReading);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = serviceuser.FirstName + " " + serviceuser.LastName + " Weight Reading has been created.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        weightReadingViewModel.WeightReading.Id = IsGetTodayData.Id;

                        bloodPressureReading = await _weightReadingService.UpdateAsync(weightReadingViewModel.WeightReading);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = serviceuser.FirstName + " " + serviceuser.LastName + " Weight Reading has been updated.", UserReference = "", CreatedBy = base.UserId });
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
                var data = await _weightReadingService.GetByIdAsync(id);

                await _weightReadingService.DeleteAsync(data);

                return Json(new { statuscode = 1 });

            }
            catch (Exception ex)
            {
                return Json(new { statuscode = 3, message = ex.Message });
            }
        }
    }
}
