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
			CreateMap<Contact, ContactModel>().ReverseMap();

		}
	}
}
