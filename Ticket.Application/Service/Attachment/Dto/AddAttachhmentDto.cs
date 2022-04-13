using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Attachment.Dto
{
   public class AddAttachhmentDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string UserId { get; set; }
        public long QuestionId { get; set; }
    }
}
