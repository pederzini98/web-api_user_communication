using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UseModels.Entities;

namespace UseModels.ViewModels
{
    public class CommunicationViewModel
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string UsedContactType { get; set; }
        public string CommunicationTitle { get; set; }
        public string CommunicationContent { get; set; }

        public CommunicationViewModel(Communication communication)
        {
            Id = communication.Id;
            UserId = communication.UserId;
            UsedContactType = communication.UsedContactType.ToString();
            CommunicationTitle = communication.CommunicationTitle;
            CommunicationContent = communication.CommunicationContent;
        }

    }
}