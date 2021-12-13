using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Enums;

namespace UseModels.Entities
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]

        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public ContactType MainContactType { get; set; }
        public IList<ContactType> PreferedContactTypes { get; set; } = new List<ContactType>();
        public IList<ContactType> AlternativeContactTypes { get; set; } = new List<ContactType>();
        public bool Disable { get; set; }

        public User()
        {
            Disable = false;
        }
    }
}
