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
    public class Communication
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        [BsonId]
        public string Id { get; set; }
        public string UserId { get; set; }
        public ContactType UsedContactType { get; set; }
        public string CommunicationTitle { get; set; }
        public string CommunicationContent { get; set; }


        public List<Communication> VerifyTitleLongerThanFiveChars(List<Communication> communications)
        {
            List<Communication> result = new();

            foreach (Communication item in communications)
            {
                string title = item.CommunicationTitle;
                string[] titleFormated = title.Split(' ');

                foreach (string word in titleFormated)
                {
                    if (word.Length >= 5)
                    {
                        result.Add(item);
                        break;
                    }
                }
            }

            return result;
        }
    }
}
