using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Entities;
using UseModels.Enums;

namespace UseModels.Services
{
    public interface ICommunicationService
    {
        Task<List<Communication>> GetCommunicationByUser(string userId);

        Task<Communication> GetCommunicationById(string id);

        Task<List<Communication>> GetCommunicationByUserAndContactType(string userId, ContactType contacType);

        Task<List<Communication>> GetCommunicationWithTitle(string title);
        Task<List<Communication>> GetBiggerCommunicationTitles();
        

        Task<bool> CreateCommunication(Communication communication);


    }
}
