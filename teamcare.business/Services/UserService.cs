﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public class UserService : BaseService, IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;
		public UserService(IAuditService auditService, IUserRepository userRepository, IMapper mapper) : base(auditService)
		{
			_userRepository = userRepository;
			_mapper = mapper;
		}
		
        public async Task<UserModel> GetByIdAsync(Guid id)
        {
			await RecordAuditEntry(new AuditModel { Action = "GetUser for " + id, Details = "service call for get user", UserReference = "" });

            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<User, UserModel>(user);
		}

        public async Task<IEnumerable<UserModel>> ListAllAsync()
        {
			await RecordAuditEntry(new AuditModel { Action = "GetAllUsers", Details = "service call for get user", UserReference = "" });

            var listUsers = await _userRepository.ListAllAsync();
            var mapperlist = _mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(listUsers);
            return mapperlist = mapperlist.OrderBy(r => r.FirstName).ThenBy(p => p.LastName);
        }

        public async Task<UserModel> AddAsync(UserModel model)
        {
			await RecordAuditEntry(new AuditModel { Action = "AddUser", Details = "service call for get user", UserReference = "" });

            var result = _mapper.Map<UserModel, User>(model);
            var user = await _userRepository.AddAsync(result);
            return _mapper.Map<User, UserModel>(user);
		}

        public async Task<UserModel> UpdateAsync(UserModel model)
        {
			await RecordAuditEntry(new AuditModel { Action = "UpdateUser", Details = "service call for get user", UserReference = "" });

            var result = _mapper.Map<UserModel, User>(model);
            var user = await _userRepository.UpdateAsync(result);
            return _mapper.Map<User, UserModel>(user);
		}

        public async Task DeleteAsync(UserModel model)
        {
			await RecordAuditEntry(new AuditModel { Action = "DeleteUser for" + model.Id, Details = "service call for get user", UserReference = "" });
            var result = _mapper.Map<UserModel, User>(model);
			await _userRepository.DeleteAsync(result);
		}

        public async Task<Guid> GetUserGuidAsync(string PreferredUsername)
        {
            var tempList = await ListAllAsync();            
            return new Guid(tempList.Where(x => x.Email == PreferredUsername.ToString().Trim()).FirstOrDefault().Id.ToString());
        }
    }
}