using AutoMapper;
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

            var user = await _userRepository.GetByIdAsync(id);
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<IEnumerable<UserModel>> ListAllAsync()
        {

            var listUsers = await _userRepository.ListAllAsync();
            var mapperlist = _mapper.Map<IEnumerable<User>, IEnumerable<UserModel>>(listUsers);
            return mapperlist = mapperlist.OrderBy(r => r.FirstName).ThenBy(p => p.LastName).ToList();
        }

        public async Task<UserModel> AddAsync(UserModel model)
        {

            var result = _mapper.Map<UserModel, User>(model);
            var user = await _userRepository.AddAsync(result);
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task<UserModel> UpdateAsync(UserModel model)
        {

            var result = _mapper.Map<UserModel, User>(model);
            var user = await _userRepository.UpdateAsync(result);
            return _mapper.Map<User, UserModel>(user);
        }

        public async Task DeleteAsync(UserModel model)
        {
            var result = _mapper.Map<UserModel, User>(model);
            await _userRepository.DeleteAsync(result);
        }
    }
}