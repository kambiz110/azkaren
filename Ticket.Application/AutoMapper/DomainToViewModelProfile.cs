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
using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Application.Service.QuizTemp.Dto;
using Azmoon.Application.Service.Result.Dto;

namespace Azmoon.Application.AutoMapper
{
    public class DomainToViewModelProfile : Profile
    {
        public DomainToViewModelProfile()
        {
            //  CreateMap<Course, CourseViewModel>();
            CreateMap<User, Application.Service.Filter.Dto.Result>()
                 .ForMember(ds => ds.id,
            src => src.MapFrom(src => src.UserName))
                  .ForMember(ds => ds.text,
            src => src.MapFrom(src => src.FirstName+" "+ src.LastName));
            CreateMap<Group, GetGroupViewModel>();
            CreateMap<WorkPlace, GetWorkPlaceViewModel>().ReverseMap();
            CreateMap<User, UserShowAdminDto>();
            CreateMap<User, RegisterUserDto>();
            CreateMap<User, UpdateUserDto>().ReverseMap();
            CreateMap<Role, GetRoleDto>();
            CreateMap<Quiz, GetQuizDto>()
                .ForMember(ds => ds.FilterStatus,
            src => src.MapFrom(src => src.QuizFilter!=null?true:false))
                .ForMember(ds => ds.GroupName,
            src => src.MapFrom(src => src.Group != null ? src.Group.Name : ""))
                .ReverseMap();
            CreateMap<Quiz, QuizAssignViewModel>().ReverseMap();
            CreateMap<Quiz, GetQuizDetilesDto>().ReverseMap();
            CreateMap<Question, GetQuestionViewModel>().ReverseMap(); 
            CreateMap<Question, AddQuestionViewModel>().ReverseMap(); 
            CreateMap<Answer, GetAnswerDto>().ReverseMap(); 
            CreateMap<Answer, AddAnswerDto>().ReverseMap(); 
            CreateMap<QuizStartTemp, AddQuizTempDto>().ReverseMap(); 
            CreateMap<Domain.Entities.Result, GetResutQuizDto>()
                .ForMember(ds => ds.UserName,
            src => src.MapFrom(src => src.Student != null ? src.Student.FirstName+" "+ src.Student.LastName : ""))
                .ForMember(ds => ds.PhoneNumber,
            src => src.MapFrom(src => src.Student != null ? src.Student.Phone : ""))
                .ForMember(ds => ds.QuizName,
            src => src.MapFrom(src => src.Quiz != null ? src.Quiz.Name : ""))
                .ReverseMap(); 





        }

    }
}
