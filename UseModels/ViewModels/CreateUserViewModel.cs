using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Enums;

namespace UseModels.ViewModels
{
    public class CreateUserViewModel
    {
        public string Name { get; set; }
        public string Nickame { get; set; }
        public bool Disabled { get; set; }
        public ContactType MainContactType { get; set; }
        public IList<ContactType> PreferedContactTypes { get; set; } = new List<ContactType>();
        public IList<ContactType> AlternativeContactType { get; set; } = new List<ContactType>();

        public CreateUserViewModel()
        {

        }
        public CreateUserViewModel(CreateUserViewModel createUserViewModel)
        {
            Name = createUserViewModel.Name;
            Nickame = createUserViewModel.Nickame;
            MainContactType = createUserViewModel.MainContactType;
            PreferedContactTypes = createUserViewModel.PreferedContactTypes;
            AlternativeContactType = createUserViewModel.AlternativeContactType;
            Disabled = createUserViewModel.Disabled;
        }
    }
}
