using System.Collections.Generic;
using System.Threading.Tasks;
using UseModels.Entities;
using UseModels.Enums;

namespace UseModels.Services
{
    public interface IUserService
    {
        Task<List<User>> GetUsers();

        Task<User> DisableUser(string id);

        Task<User> EnabledUser(string id);

        Task<List<User>> GetDisabledUsers();

        Task<List<User>> GetEnabledUsers();

        Task<List<User>> GetEnabledUsersByContactType(ContactType contactType);

        Task<User> GetUserById(string id);

        Task<bool> CreateUser(User user);
        Task<bool> EditUser(string userId, User user);
    }
}
