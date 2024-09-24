using AutoMapper;
using Contracts.RepositoryCore;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Storage;
using Service.Contracts.DocsEntities;
using Shared.DataTransferObjects.Letters;
using Shared.DataTransferObjects.Recipients;
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
    }
}
