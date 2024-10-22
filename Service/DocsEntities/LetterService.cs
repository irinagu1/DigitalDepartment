using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Exceptions.NotFound;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.Documents;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.Recipients;
using Shared.DataTransferObjects.Roles;
using Shared.DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DocsEntities
{
    internal sealed class LetterService : ILetterService
    {
        private readonly IRepositoryManager _repository;
        private readonly IMapper _mapper;
        public LetterService(IRepositoryManager repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<LetterDto> CreateLetterAsync(LetterForCreationDto letterForCreationDto)
        {
            var letterEntity = _mapper.Map<Letter>(letterForCreationDto);
            _repository.Letter.CreateLetter(letterEntity);
            await _repository.SaveAsync();
            var letterToReturn = _mapper.Map<LetterDto>(letterEntity);
            return letterToReturn;
        }


        public async Task<string> StoreRecipients(RecipientsForCreationDto recipientsForCreationDto)
        {
            if (recipientsForCreationDto.UsersIds is not null)
            {
                List<int> usRecIds = await StoreData(recipientsForCreationDto.UsersIds,
                    recipientsForCreationDto.LetterId,
                    recipientsForCreationDto.ToCheck,
                    "user");
            }
            if (recipientsForCreationDto.RolesIds is not null)
            {
                List<int> rolesRecIds = await StoreData(recipientsForCreationDto.RolesIds,
                    recipientsForCreationDto.LetterId,
                    recipientsForCreationDto.ToCheck,
                    "role");
            }

            return "success";
        }

        async Task<List<int>> StoreData(string[] data, int letterId, bool ToCheck, string who)
        {
            List<int> toReturn = new List<int>();
            foreach (var rec in data)
            {
                RecipientDto recipientDtoForAdding = new RecipientDto()
                {
                    LetterId = letterId,
                    Type = who,
                    TypeId = rec,
                    ToCheck = ToCheck,

                };
                var recipientEntity = _mapper.Map<Recipient>(recipientDtoForAdding);
                _repository.Recipient.CreateRecipient(recipientEntity);
                toReturn.Add(recipientEntity.Id);
            }
            await _repository.SaveAsync();
            return toReturn;

        }

        public async Task<AllRecipientsByCategoryDto> GetRecipientsByLetterId(int letterId)
        {
           var letterEntity = await _repository.Letter.GetLetterById(letterId);
            if (letterEntity is null)
                throw new LetterNotFoundException(letterId);

            var dtoToReturn = new AllRecipientsByCategoryDto()
            {
                UserToCheck = await GetUsers(letterId, true),
                UserToShow = await GetUsers(letterId, false),
                RolesToCheck = await GetRoles(letterId, true),
                RolesToShow= await GetRoles(letterId, false),
            };

            return dtoToReturn;
        }

        private async Task<IEnumerable<UserDto>> GetUsers(int letterId, bool isToCheck)
        {
            var allUserEntities = await _repository.User.GetAll();

            var recipientsUsersToShow = await _repository.Recipient.GetRecipientsByTypeAndLetterId("user", letterId, isToCheck);
            var usersIdsToShow = recipientsUsersToShow.Select(u => u.TypeId).ToList();
            var users = allUserEntities.Where(e => usersIdsToShow.Contains(e.Id));
            var usersDto = _mapper.Map<IEnumerable<UserDto>>(users);

            return usersDto;
        }

        private async Task<IEnumerable<RolesDto>> GetRoles(int letterId, bool isToCheck)
        {
            var allRolesEntities = await _repository.Role.GetAllRoles();

            var recipientsRolesToShow = await _repository.Recipient.GetRecipientsByTypeAndLetterId("role", letterId, isToCheck);
            var rolesIdsToShow = recipientsRolesToShow.Select(u => u.TypeId).ToList();
            var roles = allRolesEntities.Where(r=> rolesIdsToShow.Contains(r.Id));
            var rolesDto =  _mapper.Map<IEnumerable<RolesDto>>(roles);

            return rolesDto;
        }

        public async Task<IEnumerable<RecipientsForReportDto>> GetRecipientsForReportByLetterId(int letterId, int documentId)
        {
            var usersDto = await GetUsers(letterId, true);
            var rolesDto = await GetRoles(letterId, true);

            foreach(var role in rolesDto)
            {
                var usersByRole = await _repository.User.GetUsersByRoleId(role.Id);
                var dto = _mapper.Map<IEnumerable<UserDto>>(usersByRole);
                usersDto = usersDto.Concat(dto);
            }

            usersDto = usersDto.DistinctBy(u=> u.Id).ToList();

            var recipientsForReport = new List<RecipientsForReportDto>();
            foreach(var user in usersDto)
            {
                var toCheck = await _repository.ToCheck.GetToCheckByUserAndDocumentIds(user.Id, documentId);
                if (toCheck is not null)
                    recipientsForReport.Add(new RecipientsForReportDto() { User = user, DateChecked = toCheck.DateChecked });
                else
                    recipientsForReport.Add(new RecipientsForReportDto() { User = user, DateChecked = null});
            }
            return recipientsForReport;
           
        }

        public async Task CreateReport(int documentId, int letterId)
        {
            var document = _repository.Document.GetDocument(documentId, false);
            var recipients = await GetRecipientsForReportByLetterId(letterId, documentId);
            ReportManager reportManager = new ReportManager();
            reportManager.CreateGeneralReport(recipients, document.Name ?? "");
        }
    }
}
