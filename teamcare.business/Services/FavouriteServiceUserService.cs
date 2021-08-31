using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using teamcare.business.Models;
using teamcare.data.Entities.Users;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class FavouriteServiceUserService : BaseService, IFavouriteServiceUserService
    {
        private readonly IMapper _mapper;
        private readonly IFavouriteServiceUserRepository _repository;

        public FavouriteServiceUserService(IMapper mapper, IAuditService auditService, IFavouriteServiceUserRepository repository) : base(auditService)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<FavouriteServiceUserModel> GetByIdAsync(Guid id,FavouriteServiceUserModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "GetFavouriteServiceUser for " + id, Details = "service call for get details for favourite service user", UserReference = "",CreatedBy=model.CreatedBy });
            var result = await _repository.GetByIdAsync(id);
            return _mapper.Map<FavouriteServiceUser, FavouriteServiceUserModel>(result);
        }

        public async Task<IEnumerable<FavouriteServiceUserModel>> ListAllAsync(FavouriteServiceUserModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "GetAllFavouriteServiceUsers", Details = "service call for get all favourite service user", UserReference = "",CreatedBy=model.CreatedBy });
            var listFavouriteServiceUser = await _repository.ListAllAsync();
            var mapperlist = _mapper.Map<IEnumerable<FavouriteServiceUser>, IEnumerable<FavouriteServiceUserModel>>(listFavouriteServiceUser);
            mapperlist = mapperlist.OrderBy(r => r.UserId).ToList();
            return mapperlist;
        }


        public async Task<FavouriteServiceUserModel> AddAsync(FavouriteServiceUserModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "AddFavouriteServiceUser", Details = "service call for add favourite service user", UserReference = "", CreatedBy = model.CreatedBy });
            var mapped = _mapper.Map<FavouriteServiceUserModel, FavouriteServiceUser>(model);
            var result = await _repository.AddAsync(mapped);
            return _mapper.Map<FavouriteServiceUser, FavouriteServiceUserModel>(result);
        }

        public async Task<FavouriteServiceUserModel> UpdateAsync(FavouriteServiceUserModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "UpdateFavouriteServiceUser", Details = "service call for update favourite service user", UserReference = "", CreatedBy = model.CreatedBy });
            var mapped = _mapper.Map<FavouriteServiceUserModel, FavouriteServiceUser>(model);
            var result = await _repository.UpdateAsync(mapped);
            return _mapper.Map<FavouriteServiceUser, FavouriteServiceUserModel>(result);
        }

        public async Task DeleteAsync(FavouriteServiceUserModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "DeleteFavouriteServiceUser for" + model.Id, Details = "service call for remove favourite service user", UserReference = "", CreatedBy = model.CreatedBy });
            var result = _mapper.Map<FavouriteServiceUserModel, FavouriteServiceUser>(model);
            await _repository.DeleteAsync(result);
            
        }
    }
}