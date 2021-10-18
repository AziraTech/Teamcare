using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using teamcare.business.Models;
using teamcare.data.Entities;
using teamcare.data.Entities.Users;
using teamcare.data.Entities.Documents;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Entities.SkillAssessments;


namespace teamcare.web.app.Middleware
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<Audit, AuditModel>().ReverseMap();
			CreateMap<User, UserModel>().ReverseMap();
			CreateMap<ServiceUser, ServiceUserModel>().ReverseMap();
			CreateMap<Residence, ResidenceModel>().ReverseMap();
			CreateMap<DocumentUpload, DocumentUploadModel>().ReverseMap();
			CreateMap<FavouriteServiceUser, FavouriteServiceUserModel>().ReverseMap();
			CreateMap<Contact, ContactModel>().ReverseMap();
			CreateMap<ServiceUserLog, ServiceUserLogModel>().ReverseMap();
			CreateMap<SkillGroup, SkillGroupsModel>().ReverseMap();
			CreateMap<LivingSkill, LivingSkillsModel>().ReverseMap();
			CreateMap<Assessment, AssessmentModel>().ReverseMap();
			CreateMap<AssessmentSkill, AssessmentSkillModel>().ReverseMap();
			CreateMap<AssessmentType, AssessmentTypeModel>().ReverseMap();
			CreateMap<HealthMedication, HealthMedicationModel>().ReverseMap();
			CreateMap<ServiceUserDocument, ServiceUsersDocumentsModel>().ReverseMap();
			CreateMap<BloodPressureReading, BloodPressureReadingModel>().ReverseMap();
		}
	}
}
