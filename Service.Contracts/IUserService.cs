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
        UserDto GetUserById(string userId);
        Task<UserDto> GetUserByIdAsync(string userId);
        Task<HashSet<string>> GetUserPermissions(string userId);
        Task<IEnumerable<UserDto>> GetAllUsersForShow(string isActive);
        Task<List<UserForLettersDto>> GetAllUserForLetters();
    }
}
