using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities.SkillAssessments;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class AssessmentSkillService : IAssessmentSkillService, IService<AssessmentSkillModel>
    {
        private readonly IAssessmentSkillRepository _assessmentSkillRepository;
        private readonly IMapper _mapper;

        public AssessmentSkillService(IAssessmentSkillRepository assessmentSkillRepository, IMapper mapper)
        {
            _assessmentSkillRepository = assessmentSkillRepository;
            _mapper = mapper;

        }

        public async Task<AssessmentSkillModel> GetByIdAsync(Guid id)
        {
            var result = await _assessmentSkillRepository.GetByIdAsync(id);
            return _mapper.Map<AssessmentSkill, AssessmentSkillModel>(result);
        }

        public async Task<IEnumerable<AssessmentSkillModel>> ListAllAsync()
        {
            var result = await _assessmentSkillRepository.ListAllAsync();
            return _mapper.Map<List<AssessmentSkill>, List<AssessmentSkillModel>>(result);
        }

        public async Task<AssessmentSkillModel> AddAsync(AssessmentSkillModel model)
        {
            var mapped = _mapper.Map<AssessmentSkillModel, AssessmentSkill>(model);
            var result = await _assessmentSkillRepository.AddAsync(mapped);
            return _mapper.Map<AssessmentSkill, AssessmentSkillModel>(result);
        }

        public async Task<AssessmentSkillModel> UpdateAsync(AssessmentSkillModel model)
        {
            var mapped = _mapper.Map<AssessmentSkillModel, AssessmentSkill>(model);
            var result = await _assessmentSkillRepository.UpdateAsync(mapped);
            return _mapper.Map<AssessmentSkill, AssessmentSkillModel>(result);
        }

        public async Task DeleteAsync(AssessmentSkillModel model)
        {
            var result = _mapper.Map<AssessmentSkillModel, AssessmentSkill>(model);
            await _assessmentSkillRepository.DeleteAsync(result);
        }

        public async Task SaveAssessmentSkillList(Guid AssessmentId, IEnumerable<AssessmentSkillModel> model)
        {
            foreach (var item in model)
            {
                //if (item.Id.ToString() != "")
                //{
                //    var oldassesstskill = await GetByIdAsync((Guid)item.Id);
                //    if (oldassesstskill != null)
                //    {
                //        oldassesstskill.SkillLevel = item.SkillLevel;
                //        await UpdateAsync(oldassesstskill);
                //    }
                //}
                //else
                //{
                    item.AssessmentId = AssessmentId;
                    await AddAsync(item);
                //}
            }
        }

    }
}