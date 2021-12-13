using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Entities;

namespace UseModels.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Nickname { get; set; }
        public bool Disabled { get; set; }
        public string MainContactType { get; set; }
        public IList<string> PreferedContactTypes { get; set; } = new List<string>();
        public IList<string> AlternativeContactType { get; set; } = new List<string>();
        public UserViewModel()
        {

        }
        public UserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Nickname = user.Nickname;
            Disabled = user.Disable;
            MainContactType = user.MainContactType.ToString();
            PreferedContactTypes = user.PreferedContactTypes.Select(x => x.ToString()).ToList();
            AlternativeContactType = user.AlternativeContactTypes.Select(x => x.ToString()).ToList();
        }
    }
}
