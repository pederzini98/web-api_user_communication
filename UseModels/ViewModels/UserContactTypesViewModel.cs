using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Entities;
using UseModels.Enums;

namespace UseModels.ViewModels
{
    public class UserContactTypesViewModel
    {
        public List<string> ContactTypes { get; set; } = new List<string>();

        public UserContactTypesViewModel()
        {

        }
        public UserContactTypesViewModel(User user)
        {
            List<ContactType> contactTypes = new();
            contactTypes.Add(user.MainContactType);
            contactTypes.AddRange(user.AlternativeContactTypes);
            contactTypes.AddRange(user.PreferedContactTypes);

            ContactTypes.AddRange(contactTypes.Select(x => x.ToString()).ToList());
        }
    }
}
