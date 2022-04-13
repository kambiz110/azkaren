using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces;
using Azmoon.Application.Service.Answer.Dto;

using Azmoon.Application.Service.Question.Dto;
using Azmoon.Application.Service.Role.Command;
using Azmoon.Application.Service.Role.Dto;
using Azmoon.Application.Service.User.Dto;
using Azmoon.Application.Service.Group.Dto;
using Azmoon.Domain.Entities;
using Azmoon.Application.Service.Quiz.Dto;

namespace Azmoon.Application.AutoMapper
{
   public class ViewModelToDomainProfile : Profile
    {
        public ViewModelToDomainProfile()
        {
            CreateMap<AddRoleDto, Role>();
            CreateMap<CreateGroupDto, Group>();



            CreateMap<AddQuestionViewModel, Question>();

            CreateMap<AddAnswerDto, Answer>();

            CreateMap<AddQuizDto, Quiz>().ReverseMap();

            CreateMap<RegisterUserDto, User>()
                .ForMember(ds => ds.Email,
            src => src.MapFrom(src => src.personeli + "@Saas.ir"))
                .ForMember(ds => ds.NormalizedEmail,
            src => src.MapFrom(src => src.personeli + "@Saas.ir"))
                .ForMember(ds => ds.NormalizedUserName,
            src => src.MapFrom(src => src.personeli.ToString()));
                
            CreateMap<RegisterUserDto, persons>()
                .ForMember(ds => ds.name,
            src => src.MapFrom(src => src.FirstName))
                 .ForMember(ds => ds.family,
            src => src.MapFrom(src => src.LastName))
                .ReverseMap();
            //CreateMap<CourseViewModel, CreateCourseCommand>()
            //.ConstructUsing(c => new CreateCourseCommand(c.Name, c.Description, c.ImageUrl));
        }

    }
}
