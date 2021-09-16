using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities.SkillAssessments;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class AssessmentService : IAssessmentService, IService<AssessmentModel>
    {
        private readonly IAssessmentRepository _assessmentRepository;
        private readonly IMapper _mapper;

        public AssessmentService(IAssessmentRepository assessmentRepository, IMapper mapper)
        {
            _assessmentRepository = assessmentRepository;
            _mapper = mapper;

        }

        public async Task<AssessmentModel> GetByIdAsync(Guid id)
        {
            var result = await _assessmentRepository.GetByIdAsync(id);
            return _mapper.Map<Assessment, AssessmentModel>(result);
        }

        public async Task<IEnumerable<AssessmentModel>> ListAllAsync()
        {
            var result = await _assessmentRepository.ListAllAsync();
            return _mapper.Map<List<Assessment>, List<AssessmentModel>>(result);
        }

        public async Task<AssessmentModel> AddAsync(AssessmentModel model)
        {
            var mapped = _mapper.Map<AssessmentModel, Assessment>(model);
            var result = await _assessmentRepository.AddAsync(mapped);
            return _mapper.Map<Assessment, AssessmentModel>(result);
        }

        public async Task<AssessmentModel> UpdateAsync(AssessmentModel model)
        {
            var mapped = _mapper.Map<AssessmentModel, Assessment>(model);
            var result = await _assessmentRepository.UpdateAsync(mapped);
            return _mapper.Map<Assessment, AssessmentModel>(result);
        }

        public async Task DeleteAsync(AssessmentModel model)
        {
            var result = _mapper.Map<AssessmentModel, Assessment>(model);
            await _assessmentRepository.DeleteAsync(result);
        }
       

    }
}