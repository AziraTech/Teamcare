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

        //public async Task<IEnumerable<ContactModel>> ListByAllServiceUserContactAsync(Guid id)
        //{
        //    await RecordAuditEntry(new AuditModel { Action = "GetAllContact", Details = "service call for get contact", UserReference = "" });

        //    var listContact = await _contactRepository.ListAllAsync();
        //    var finallist = listContact.Where(r => r.ServiceUserId == id).ToList();
        //    return _mapper.Map<IEnumerable<Contact>, IEnumerable<ContactModel>>(finallist);
        //}

        //public async Task<IEnumerable<ContactModel>> ListAllSortedFiltered(int sortBy, string filterBy)
        //{
        //    var listUsers = await _contactRepository.ListAllAsync();
        //    var mappedUsers=_mapper.Map<IEnumerable<Contact>, IEnumerable<ContactModel>>(listUsers);
        //    switch (sortBy)
        //    {
        //        case 0: mappedUsers = mappedUsers.OrderBy(y => y.FirstName + " " + y.LastName).ToList(); break;
        //        case 1: mappedUsers = mappedUsers.OrderByDescending(y => y.FirstName + " " + y.LastName).ToList(); break;
        //        case 2: mappedUsers = mappedUsers.OrderBy(y => y.DateOfAdmission).ToList(); break;
        //        case 3: mappedUsers = mappedUsers.OrderByDescending(y => y.DateOfAdmission).ToList(); break;
        //    }
        //    if (filterBy != null && "" + filterBy.Trim() != "")
        //    {
        //        mappedUsers = mappedUsers.ToArray().Where(x => x.ResidenceId == new Guid(filterBy)).OrderBy(y => y.FirstName + " " + y.LastName).ToList();
        //    }
        //    return mappedUsers;
        //}

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

        public Task DeleteAsync(ContactModel model)
        {
            throw new NotImplementedException();
        }
    }
}