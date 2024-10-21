using Entities.Models.Auth;
using Shared.DataTransferObjects;
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
        Task<IEnumerable<UserDto>> GetUsersByRole(string roleId);
        Task<List<UserForLettersDto>> GetAllUserForLetters();
        bool UpdateUserStatus(string userId);
        bool UpdateUser(UserForUpdateDto userForUpdateDto);
        bool ChangePassword(PasswordToChangeDto changeDto);
    }
}
