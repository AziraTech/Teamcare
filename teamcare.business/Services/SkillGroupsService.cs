using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities.SkillAssessments;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class SkillGroupsService : ISkillGroupsService, IService<SkillGroupsModel>
    {
        private readonly ISkillGroupsRepository _skillGroupsRepository;
        private readonly IMapper _mapper;

        public SkillGroupsService(ISkillGroupsRepository skillGroupsRepository, IMapper mapper)
        {
            _skillGroupsRepository = skillGroupsRepository;
            _mapper = mapper;

        }

        public async Task<SkillGroupsModel> GetByIdAsync(Guid id)
        {
            var result = await _skillGroupsRepository.GetByIdAsync(id);
            return _mapper.Map<SkillGroup, SkillGroupsModel>(result);
        }

        public async Task<IEnumerable<SkillGroupsModel>> ListAllAsync()
        {
            var result = await _skillGroupsRepository.ListAllAsync();
            return _mapper.Map<List<SkillGroup>, List<SkillGroupsModel>>(result);
        }

        public async Task<SkillGroupsModel> AddAsync(SkillGroupsModel model)
        {
            var mapped = _mapper.Map<SkillGroupsModel, SkillGroup>(model);
            var result = await _skillGroupsRepository.AddAsync(mapped);
            return _mapper.Map<SkillGroup, SkillGroupsModel>(result);
        }

        public async Task<SkillGroupsModel> UpdateAsync(SkillGroupsModel model)
        {
            var mapped = _mapper.Map<SkillGroupsModel, SkillGroup>(model);
            var result = await _skillGroupsRepository.UpdateAsync(mapped);
            return _mapper.Map<SkillGroup, SkillGroupsModel>(result);
        }

        public async Task DeleteAsync(SkillGroupsModel model)
        {
            var result = _mapper.Map<SkillGroupsModel, SkillGroup>(model);
            await _skillGroupsRepository.DeleteAsync(result);
        }

        public async Task ChangePositoin(IEnumerable<SkillGroupsModel> model)
        {
            foreach (var item in model)
            {
                var skilldata = await GetByIdAsync((Guid)item.Id);
                if (skilldata != null)
                {
                    skilldata.Position = item.Position;
                }

                await UpdateAsync(skilldata);
            }
        }

    }
}