using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Entities;

namespace UseModels.Database
{
    public interface IDbClient
    {
        IMongoCollection<User> GetUserCollection();
        IMongoCollection<Communication> GetCommunicationCollection();

    }
}
