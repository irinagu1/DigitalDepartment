using Entities.Models.Auth;
using Shared.DataTransferObjects.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts
{
    public interface IUserService
    {
        Task<HashSet<string>> GetUserPermissions(string userId);
        Task<List<UserForLettersDto>> GetAllUserForLetters();
    }
}
