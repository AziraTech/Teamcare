using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using teamcare.business.Models;
using teamcare.common.Configuration;
using teamcare.data.Entities;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class ServiceUserService : BaseService, IServiceUserService
    {
        private readonly IMapper _mapper;
        private readonly IServiceUserRepository _serviceUserRepository;
        private readonly IAuditService _auditService;
        private readonly AzureStorageSettings _azureStorageOptions;

        public ServiceUserService(IAuditService auditService, IMapper mapper,
            IServiceUserRepository serviceUserRepository,
            IOptions<AzureStorageSettings> azureStorageOptions) : base(auditService)
        {
            _mapper = mapper;
            _serviceUserRepository = serviceUserRepository;
            _auditService = auditService;
            _azureStorageOptions = azureStorageOptions.Value;
        }

        public async Task<ServiceUserModel> GetByIdAsync(Guid id)
        {
            var serviceUser = await _serviceUserRepository.GetByIdAsync(id);
            var residence = serviceUser.Residence;
            var documents = serviceUser.DocumentUploads;
            return _mapper.Map<ServiceUser, ServiceUserModel>(serviceUser);
        }

        public async Task<IEnumerable<ServiceUserModel>> ListAllAsync()
        {
            await RecordAuditEntry(new AuditModel { Action = "GetAllUsers", Details = "service call for get user", UserReference = "" });

            var listUsers = await _serviceUserRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<ServiceUser>, IEnumerable<ServiceUserModel>>(listUsers);
        }

        public async Task<ServiceUserModel> AddAsync(ServiceUserModel model)
        {

            var result = _mapper.Map<ServiceUserModel, ServiceUser>(model);
            var user = await _serviceUserRepository.AddAsync(result);
            return _mapper.Map<ServiceUser, ServiceUserModel>(user);
        }



        public async Task<ServiceUserModel> UpdateAsync(ServiceUserModel model)
        {
            var result = _mapper.Map<ServiceUserModel, ServiceUser>(model);
            var serviceUser = await _serviceUserRepository.UpdateAsync(result);
            return _mapper.Map<ServiceUser, ServiceUserModel>(serviceUser);
        }

        public Task DeleteAsync(ServiceUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}