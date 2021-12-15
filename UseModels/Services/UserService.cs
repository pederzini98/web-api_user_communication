using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Database;
using UseModels.Entities;
using UseModels.Enums;

namespace UseModels.Services
{
    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IDbClient dbClient)
        {
            _users = dbClient.GetUserCollection();
        }

        public async Task<User> DisableUser(string id)
        {

            User user = await GetUserById(id);
            user.Disable = true;

            await _users.FindOneAndReplaceAsync(user => user.Id == id, user);
            return user;

        }

        public async Task<User> EnabledUser(string id)
        {
            User user = await GetUserById(id);
            user.Disable = false;

            await _users.FindOneAndReplaceAsync(user => user.Id == id, user);
            return user;


        }

        public async Task<List<User>> GetDisabledUsers()
        {
            return await _users.FindAsync(user => user.Disable == true && !String.IsNullOrEmpty(user.Name)).GetAwaiter().GetResult().ToListAsync();
        }

        public async Task<List<User>> GetEnabledUsers()
        {
            List<User> users = await _users.FindAsync(user => user.Disable == false && !String.IsNullOrEmpty(user.Name)).GetAwaiter().GetResult().ToListAsync();
            return users;
        }

        public async Task<List<User>> GetEnabledUsersByContactType(ContactType contactType)
        {
            return await _users.FindAsync(user => (user.MainContactType == contactType || user.PreferedContactTypes.Contains(contactType) ||
            user.AlternativeContactTypes.Contains(contactType)) && user.Disable == false).GetAwaiter().GetResult().ToListAsync();
        }

        public async Task<User> GetUserById(string id)
        {
            IAsyncCursor<User> cursor = await _users.FindAsync(user => user.Id == id && !String.IsNullOrEmpty(user.Name));
            User user = await cursor.FirstAsync();
            return user;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _users.Find(user =>  !String.IsNullOrEmpty(user.Name)).ToListAsync();
        }

        public async Task<bool> CreateUser(User user)
        {
            try
            {
                await _users.InsertOneAsync(user);
                return true;

            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
