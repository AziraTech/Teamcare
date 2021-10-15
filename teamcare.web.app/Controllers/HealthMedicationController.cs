using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.business.Services;
using teamcare.common.Enumerations;
using teamcare.data.Entities;
using teamcare.web.app.ViewModels;

namespace teamcare.web.app.Controllers
{
    public class HealthMedicationController : BaseController
    {
        private readonly IHealthMedicationService _healthmedicationService;
        private readonly IAuditService _auditService;
        private readonly IServiceUserService _serviceUserService;


        public HealthMedicationController(IHealthMedicationService healthMedicationService,
                                          IAuditService auditService,
                                          IServiceUserService serviceUserService
                                     )
        {
            _healthmedicationService = healthMedicationService;
            _auditService = auditService;
            _serviceUserService = serviceUserService;

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> HealthMedicationTabBind(Guid id)
        {
            var healthdata = await _healthmedicationService.ListAllAsync();

            var userhealth = healthdata.Where(x => x.ServiceUserId == id).FirstOrDefault();

            var model = new HealthMedicationViewModel
            {
                HealthMedication = userhealth
            };

            return PartialView("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(HealthMedicationViewModel healthMedicationViewModel)
        {
            try
            {
                if (healthMedicationViewModel?.HealthMedication != null)
                {
                    var serviceuser = await _serviceUserService.GetByIdAsync(healthMedicationViewModel.HealthMedication.ServiceUserId);

                    var healthMedication = new HealthMedicationModel();

                    if (healthMedicationViewModel.HealthMedication.Id.ToString() == "")
                    {
                        healthMedication = await _healthmedicationService.AddAsync(healthMedicationViewModel.HealthMedication);

                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Create, Details = serviceuser.FirstName + " " + serviceuser.LastName + " Medication Profile has been created.", UserReference = "", CreatedBy = base.UserId });
                        });
                    }
                    else
                    {
                        healthMedication = await _healthmedicationService.UpdateAsync(healthMedicationViewModel.HealthMedication);
                        _auditService.Execute(async repository =>
                        {
                            await repository.CreateAuditRecord(new Audit { Action = AuditAction.Update, Details = serviceuser.FirstName + " " + serviceuser.LastName + " Medication Profile has been updated.", UserReference = "", CreatedBy = base.UserId });
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
    }
}
