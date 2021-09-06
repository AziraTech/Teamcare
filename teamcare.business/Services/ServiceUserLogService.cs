using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{

    public class ServiceUserLogService : BaseService, IServiceUserLogService
    {
        private readonly IServiceUserLogRepository _repository;
        private readonly IMapper _mapper;

        public ServiceUserLogService(IAuditService auditService,
                                     IServiceUserLogRepository repository,
                                     IMapper mapper) : base(auditService)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceUserLogModel> AddAsync(ServiceUserLogModel model)
        {
            var mapped = _mapper.Map<ServiceUserLogModel, ServiceUserLog>(model);
            var result = await _repository.AddAsync(mapped);
            return _mapper.Map<ServiceUserLog, ServiceUserLogModel>(result);
        }

        public async Task DeleteAsync(ServiceUserLogModel model)
        {
            var result = _mapper.Map<ServiceUserLogModel, ServiceUserLog>(model);
            await _repository.DeleteAsync(result);
        }

        public async Task<ServiceUserLogModel> GetByIdAsync(Guid id)
        {
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<ServiceUserLog, ServiceUserLogModel>(result);
        }

        public async Task<IEnumerable<ServiceUserLogModel>> ListAllAsync()
        {
            var listresidence = await _repository.ListAllAsync();
            var mapperlist = _mapper.Map<IEnumerable<ServiceUserLog>, IEnumerable<ServiceUserLogModel>>(listresidence);
            mapperlist = mapperlist.OrderByDescending(r => r.CreatedOn).ToList();
            return mapperlist;
        }

        public async Task<ServiceUserLogModel> UpdateAsync(ServiceUserLogModel model)
        {

            var mapped = _mapper.Map<ServiceUserLogModel, ServiceUserLog>(model);
            var result = await _repository.UpdateAsync(mapped);
            return _mapper.Map<ServiceUserLog, ServiceUserLogModel>(result);
        }

        public async Task<ServiceUserLogModel> UpdateLogByParam(int type, Guid id, bool Status, String logtext, Guid UserId)
        {

            var logdata = await GetByIdAsync(id);
            logdata.ActionByAdminId = UserId;
            if (type == 1)
            {
                logdata.IsApproved = !Status;
            }
            else if (type == 2)
            {
                logdata.IsVisible = !Status;
            }
            else if (type == 3)
            {
                logdata.LogMessageUpdated = logtext;
            }

            logdata.Id = id;
            logdata.AdminActionOn = DateTimeOffset.UtcNow;

            var mapped = _mapper.Map<ServiceUserLogModel, ServiceUserLog>(logdata);
            var result = await _repository.UpdateAsync(mapped);
            return _mapper.Map<ServiceUserLog, ServiceUserLogModel>(result);
        }

        public async Task<IEnumerable<ServiceUserLogModel>> ListAllSortedFiltered(Guid? filterByserviceuser, bool IsApprove, string daterange)
        {


            var listLogs = await ListAllAsync();
            if (IsApprove == true)
            {
                listLogs = listLogs.ToList();
            }
            else
            {
                listLogs = listLogs.Where(y => y.IsApproved == false).ToList();
            }
            if (filterByserviceuser != null)
            {
                listLogs = listLogs.Where(y => y.ServiceUser.Id == filterByserviceuser).ToList();
            }
            
            if(daterange != null)
            {
                string[] date = daterange.Split('-');

                listLogs = listLogs.Where(r => r.CreatedOn.Date >= DateTime.ParseExact(date[0].Trim(),"MM/dd/yyyy",System.Globalization.CultureInfo.InvariantCulture).Date && r.CreatedOn.Date <= DateTime.ParseExact(date[1].Trim(), "MM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture).Date || r.IsApproved==false).ToList();

            }

            return listLogs;

        }
    }
}
