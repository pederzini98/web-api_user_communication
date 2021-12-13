using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Enums;

namespace UseModels.ViewModels
{
    public class CreateCommunicationViewModel
    {
        public string UserId { get; set; }
        public ContactType UsedContactType { get; set; }
        public string CommunicationTitle { get; set; }
        public string CommunicationContent { get; set; }

        public CreateCommunicationViewModel()
        {

        }
        public CreateCommunicationViewModel(CreateCommunicationViewModel createCommunicationViewModel)
        {
            UserId = createCommunicationViewModel.UserId;
            UsedContactType = createCommunicationViewModel.UsedContactType;
            CommunicationTitle = createCommunicationViewModel.CommunicationTitle;
            CommunicationContent = createCommunicationViewModel.CommunicationContent;
        }
    }
}
