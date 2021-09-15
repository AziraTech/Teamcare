using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities.SkillAssessments;
using teamcare.data.Repositories;
using System.Linq;

namespace teamcare.business.Services
{
    public class LivingSkillService : ILivingSkillService, IService<LivingSkillsModel>
    {
        private readonly ILivingSkillRepository _livingSkillRepository;
        private readonly IMapper _mapper;

        public LivingSkillService(ILivingSkillRepository livingSkillRepository, IMapper mapper)
        {
            _livingSkillRepository = livingSkillRepository;
            _mapper = mapper;

        }

        public async Task<LivingSkillsModel> GetByIdAsync(Guid id)
        {
            var result = await _livingSkillRepository.GetByIdAsync(id);
            return _mapper.Map<LivingSkill, LivingSkillsModel>(result);
        }

        public async Task<IEnumerable<LivingSkillsModel>> ListAllAsync()
        {
            var result = await _livingSkillRepository.ListAllAsync();
            return _mapper.Map<List<LivingSkill>, List<LivingSkillsModel>>(result);
        }

        public async Task<IEnumerable<LivingSkillsModel>> ListByGroupId(Guid id)
        {
            var listdata = await _livingSkillRepository.ListAllAsync();
            var mappeddata = _mapper.Map<IEnumerable<LivingSkill>, IEnumerable<LivingSkillsModel>>(listdata);
            var result = mappeddata.Where(r=>r.GroupId==id).ToList();
            return result;
        }

        public async Task<LivingSkillsModel> AddAsync(LivingSkillsModel model)
        {
            var mapped = _mapper.Map<LivingSkillsModel, LivingSkill>(model);
            var result = await _livingSkillRepository.AddAsync(mapped);
            return _mapper.Map<LivingSkill, LivingSkillsModel>(result);
        }

        public async Task<LivingSkillsModel> UpdateAsync(LivingSkillsModel model)
        {
            var mapped = _mapper.Map<LivingSkillsModel, LivingSkill>(model);
            var result = await _livingSkillRepository.UpdateAsync(mapped);
            return _mapper.Map<LivingSkill, LivingSkillsModel>(result);
        }

        public async Task DeleteAsync(LivingSkillsModel model)
        {
            var result = _mapper.Map<LivingSkillsModel, LivingSkill>(model);
            await _livingSkillRepository.DeleteAsync(result);
        }

        public async Task ChangePositoin(IEnumerable<LivingSkillsModel> model)
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