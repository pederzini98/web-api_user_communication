using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Entities;

namespace UseModels.Database
{
    public class DbClient :  IDbClient
    {
      
     private readonly IMongoCollection<User> _userCollection;
    private readonly IMongoCollection<Communication> _communicationCollection;

    public DbClient(IOptions<DbConfig> options)
    {
        var client = new MongoClient(options.Value.Connection_String);
        var database = client.GetDatabase(options.Value.Database_Name);
        _userCollection = database.GetCollection<User>(options.Value.Database_Collection_Name);
        _communicationCollection = database.GetCollection<Communication>(options.Value.Database_Collection_Name);

    }

    public IMongoCollection<Communication> GetCommunicationCollection()
    {
        return _communicationCollection;
    }

    public IMongoCollection<User> GetUserCollection()
    {
        return _userCollection;
    }

}
    }
