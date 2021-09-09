using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class AuditService : IAuditService, IService<AuditModel>
    {
        private readonly IAuditRepository _auditRepository;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public AuditService(IAuditRepository auditRepository, IMapper mapper, IServiceScopeFactory serviceScopeFactory)
        {
            _auditRepository = auditRepository;
            _mapper = mapper;
            _serviceScopeFactory = serviceScopeFactory;

        }

        public Task<AuditModel> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AuditModel>> ListAllAsync()
        {
            var result = await _auditRepository.GetAllAudit();
            return _mapper.Map<List<Audit>, List<AuditModel>>(result);
        }

        public async Task<AuditModel> AddAsync(AuditModel model)
        {
            var mapped = _mapper.Map<AuditModel, Audit>(model);
            var result = await _auditRepository.AddAsync(mapped);
            return _mapper.Map<Audit, AuditModel>(result);
        }

        public Task<AuditModel> UpdateAsync(AuditModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(AuditModel model)
        {
            throw new NotImplementedException();
        }

        public void Execute(Func<IAuditRepository, Task> databaseWork)
        {
            // Fire off the task, but don't await the result
            Task.Run(async () =>
            {
                // Exceptions must be caught
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IAuditRepository>();
                    await databaseWork(repository);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        public async Task<IEnumerable<AuditModel>> ListAllSortedFiltered(Guid? filterByserviceuser, string daterange)
        {

            var listAudits = await ListAllAsync();

            listAudits = listAudits.Where(p => p.CreatedOn.Date > DateTime.UtcNow.AddDays(-7)).OrderByDescending(p=>p.CreatedOn).ToList();

            if (filterByserviceuser != null)
            {
                listAudits = listAudits.Where(y => y.CreatedBy == filterByserviceuser).ToList();
            }

            if (daterange != null)
            {
                string[] date = daterange.Split('-');

                if (DateTime.ParseExact(date[0].Trim(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date != DateTime.UtcNow.Date && DateTime.ParseExact(date[1].Trim(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date != DateTime.UtcNow.Date)
                {
                    listAudits = listAudits.Where(r => r.CreatedOn.Date >= DateTime.ParseExact(date[0].Trim(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date && r.CreatedOn.Date <= DateTime.ParseExact(date[1].Trim(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date).ToList();
                }

            }

            return listAudits;

        }
    }
}