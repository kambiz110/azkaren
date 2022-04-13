using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.Question.Dto;
using Azmoon.Common.ResultDto;

namespace Azmoon.Application.Interfaces.Question
{
   public interface IAddQuestion
    {
        ResultDto<AddQuestionViewModel> Execute(AddQuestionViewModel dto );
    }
}
