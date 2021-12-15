using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UseModels.Database;
using UseModels.Entities;
using UseModels.Enums;

namespace UseModels.Services
{
    public class CommunicationService : ICommunicationService
    {
        private readonly IMongoCollection<Communication> _communication;
        public CommunicationService(IDbClient dbClient)
        {
            _communication = dbClient.GetCommunicationCollection();

        }

        public async Task<bool> CreateCommunication(Communication communication)
        {
            try
            {
                await _communication.InsertOneAsync(communication);
                return true;

            }
            catch (Exception)
            {

                return false;
            }

        }

        public async Task<List<Communication>> GetBiggerCommunicationTitles()
        {
            return await _communication.FindAsync(communication => !String.IsNullOrEmpty(communication.CommunicationTitle)).GetAwaiter().GetResult().ToListAsync();
        }

        public async Task<Communication> GetCommunicationById(string id)
        {
            IAsyncCursor<Communication> cursor = await _communication.FindAsync(communication => communication.Id == id);
            Communication communication = await cursor.FirstOrDefaultAsync();
            return communication;
        }

        public async Task<List<Communication>> GetCommunicationByUser(string userId)
        {
            return await _communication.FindAsync(communication => communication.UserId == userId).GetAwaiter().GetResult().ToListAsync();
        }

        public async Task<List<Communication>> GetCommunicationByUserAndContactType(string userId, ContactType contacType)
        {
            return await _communication.FindAsync(communication => communication.UserId == userId && communication.UsedContactType == contacType).GetAwaiter().GetResult().ToListAsync();
        }

        public async Task<List<Communication>> GetCommunicationWithTitle(string title)
        {
            return await _communication.FindAsync(communication => !String.IsNullOrEmpty(communication.CommunicationTitle) && communication.CommunicationTitle.ToLower().Contains(title.ToLower())).GetAwaiter().GetResult().ToListAsync();
        }


    }
}
