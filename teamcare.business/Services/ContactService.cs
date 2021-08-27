using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using teamcare.business.Models;
using teamcare.data.Entities;
using teamcare.data.Entities.ServiceUsers;
using teamcare.data.Repositories;

namespace teamcare.business.Services
{
    public class ContactService : BaseService, IContactService
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;
        private readonly IAuditService _auditService;

        public ContactService(IAuditService auditService, IMapper mapper,
            IContactRepository contactRepository) : base(auditService)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _auditService = auditService;
        }

        public async Task<ContactModel> GetByIdAsync(Guid id)
        {
            var contact = await _contactRepository.GetByIdAsync(id);
            var documents = contact.DocumentUploads;
            return _mapper.Map<Contact, ContactModel>(contact);
        }

        public async Task<IEnumerable<ContactModel>> ListAllAsync()
        {
            await RecordAuditEntry(new AuditModel { Action = "GetAllContact", Details = "service call for get contact", UserReference = "" });

            var listUsers = await _contactRepository.ListAllAsync();
            return _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactModel>>(listUsers);
        }


        public async Task<ContactModel> AddAsync(ContactModel model)
        {

            var result = _mapper.Map<ContactModel, Contact>(model);
            var user = await _contactRepository.AddAsync(result);
            return _mapper.Map<Contact, ContactModel>(user);
        }



        public async Task<ContactModel> UpdateAsync(ContactModel model)
        {
            var result = _mapper.Map<ContactModel, Contact>(model);
            var contact = await _contactRepository.UpdateAsync(result);
            return _mapper.Map<Contact, ContactModel>(contact);
        }

        public async Task DeleteAsync(ContactModel model)
        {
            await RecordAuditEntry(new AuditModel { Action = "DeleteContact for" + model.Id, Details = "service call for get contact", UserReference = "" });
            var result = _mapper.Map<ContactModel, Contact>(model);
             await _contactRepository.DeleteAsync(result);

        }
    }
}