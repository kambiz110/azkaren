using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Service.Answer.Dto;

using Azmoon.Application.Service.Question.Dto;

using Azmoon.Application.Service.User.Dto;
using Azmoon.Application.Service.Group.Dto;
using Azmoon.Domain.Entities;
using Azmoon.Application.Service.Quiz.Dto;
using Azmoon.Application.Service.WorkPlace.Dto;

namespace Azmoon.Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            //  CreateMap<Course, CourseViewModel>();
            CreateMap<Group, GetGroupViewModel>();
            CreateMap<WorkPlace, GetWorkPlaceViewModel>().ReverseMap();
            CreateMap<User, UserShowAdminDto>();
            CreateMap<Role, GetRoleDto>();
            CreateMap<Quiz, GetQuizDto>().ReverseMap();
            CreateMap<Quiz, GetQuizDetilesDto>().ReverseMap();
            CreateMap<Question, GetQuestionViewModel>().ReverseMap(); 
            CreateMap<Question, AddQuestionViewModel>().ReverseMap(); 
            CreateMap<Answer, GetAnswerDto>().ReverseMap(); 
            CreateMap<Answer, AddAnswerDto>().ReverseMap(); 





        }

    }
}
