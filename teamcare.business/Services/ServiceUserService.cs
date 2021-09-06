﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using teamcare.business.Models;
using teamcare.data.Entities;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
	public class ServiceUserService : BaseService, IServiceUserService
	{
		private readonly IMapper _mapper;
		private readonly IServiceUserRepository _serviceUserRepository;
		private readonly IAuditService _auditService;
		private readonly IFavouriteServiceUserService _favouriteServiceUserService;

		public ServiceUserService(IAuditService auditService, IMapper mapper,
			IServiceUserRepository serviceUserRepository,
			IFavouriteServiceUserService favouriteServiceUserService)
             : base(auditService)
		{
			_mapper = mapper;
			_serviceUserRepository = serviceUserRepository;
			_favouriteServiceUserService = favouriteServiceUserService;
			_auditService = auditService;
		}

		public async Task<ServiceUserModel> GetByIdAsync(Guid id)
		{

			var serviceUser = await _serviceUserRepository.GetByIdAsync(id);
			var residence = serviceUser.Residence;
			var documents = serviceUser.DocumentUploads;
			var contacts = serviceUser.Contacts;
			var serviceUserLog = serviceUser.ServiceUserLog;
			return _mapper.Map<ServiceUser, ServiceUserModel>(serviceUser);
		}

		public async Task<IEnumerable<ServiceUserModel>> ListAllAsync()
		{
			var listUsers = await _serviceUserRepository.ListAllAsync();
			return _mapper.Map<IEnumerable<ServiceUser>, IEnumerable<ServiceUserModel>>(listUsers);
		}

		public async Task<IEnumerable<ServiceUserModel>> ListAllSortedFiltered(int sortBy, string filterBy)
		{

			var listUsers = await _serviceUserRepository.ListAllAsync();
			var mappedUsers = _mapper.Map<IEnumerable<ServiceUser>, IEnumerable<ServiceUserModel>>(listUsers);
			switch (sortBy)
			{
				case 0: mappedUsers = mappedUsers.OrderBy(y => y.FirstName + " " + y.LastName).ToList(); break;
				case 1: mappedUsers = mappedUsers.OrderByDescending(y => y.FirstName + " " + y.LastName).ToList(); break;
				case 2: mappedUsers = mappedUsers.OrderBy(y => y.DateOfAdmission).ToList(); break;
				case 3: mappedUsers = mappedUsers.OrderByDescending(y => y.DateOfAdmission).ToList(); break;
			}
			if (filterBy != null && "" + filterBy.Trim() != "")
			{
				mappedUsers = mappedUsers.ToArray().Where(x => x.ResidenceId == new Guid(filterBy)).OrderBy(y => y.FirstName + " " + y.LastName).ToList();
			}
			return mappedUsers;
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

		public async Task DeleteAsync(ServiceUserModel model)
		{
			throw new NotImplementedException();
		}
	}
}