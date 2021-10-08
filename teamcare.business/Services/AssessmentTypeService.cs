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
    public class AssessmentTypeService : IAssessmentTypeService, IService<AssessmentTypeModel>
    {
        private readonly IAssessmentTypeRepository _assessmentTypeRepository;
        private readonly IMapper _mapper;

        public AssessmentTypeService(IAssessmentTypeRepository assessmentTypeRepository, IMapper mapper)
        {
            _assessmentTypeRepository = assessmentTypeRepository;
            _mapper = mapper;

        }

        public async Task<AssessmentTypeModel> GetByIdAsync(Guid id)
        {
            var result = await _assessmentTypeRepository.GetByIdAsync(id);
            return _mapper.Map<AssessmentType, AssessmentTypeModel>(result);
        }

        public async Task<IEnumerable<AssessmentTypeModel>> ListAllAsync()
        {
            var listtype = await _assessmentTypeRepository.ListAllAsync();
            listtype = listtype.OrderBy(p => p.Position).ToList();
            return _mapper.Map<IEnumerable<AssessmentType>, IEnumerable<AssessmentTypeModel>>(listtype);
        }

        public async Task<AssessmentTypeModel> AddAsync(AssessmentTypeModel model)
        {
            var mapped = _mapper.Map<AssessmentTypeModel, AssessmentType>(model);
            var result = await _assessmentTypeRepository.AddAsync(mapped);
            return _mapper.Map<AssessmentType, AssessmentTypeModel>(result);
        }

        public async Task<AssessmentTypeModel> UpdateAsync(AssessmentTypeModel model)
        {
            var mapped = _mapper.Map<AssessmentTypeModel, AssessmentType>(model);
            var result = await _assessmentTypeRepository.UpdateAsync(mapped);
            return _mapper.Map<AssessmentType, AssessmentTypeModel>(result);
        }

        public async Task DeleteAsync(AssessmentTypeModel model)
        {
            var result = _mapper.Map<AssessmentTypeModel, AssessmentType>(model);
            await _assessmentTypeRepository.DeleteAsync(result);
        }
        public async Task ChangePositoin(IEnumerable<AssessmentTypeModel> model)
        {
            foreach (var item in model)
            {
                var assetdata = await GetByIdAsync((Guid)item.Id);
                if (assetdata != null)
                {
                    assetdata.Position = item.Position;
                }

                await UpdateAsync(assetdata);
            }
        }

    }
}