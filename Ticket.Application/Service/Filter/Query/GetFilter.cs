using AutoMapper;
using Azmoon.Application.Interfaces.Contexts;
using Azmoon.Application.Interfaces.Filter;
using Azmoon.Application.Interfaces.WorkPlace;
using Azmoon.Application.Service.Filter.Dto;
using Azmoon.Common.ResultDto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azmoon.Application.Service.Filter.Query
{
    public class GetFilter : IGetFilter
    {
        private readonly IDataBaseContext _context;
        private readonly IGetChildrenWorkPlace _getChildrenWorkPlace;

        public GetFilter(IDataBaseContext context, IGetChildrenWorkPlace getChildrenWorkPlace)
        {
            _context = context;
            _getChildrenWorkPlace = getChildrenWorkPlace;
        }

        public ResultDto<CreatFilterDto> GetByQuizId(long quizid)
        {
            var filter = _context.QuizFilters.AsNoTracking().Where(p => p.QuizId == quizid)
            .FirstOrDefault();
            if (filter == null)
            {
                
                return new ResultDto<CreatFilterDto>
                {
                    IsSuccess = false
                };
            }
            var result = new CreatFilterDto
            {
                Id=filter.Id,
                QuizId=filter.QuizId,
                TypeDarajeh = (filter.TypeDarajeh)!=null ? filter.TypeDarajeh:null,
                UserList = filter.UserNameOption,
                WorkPlaceId = !String.IsNullOrEmpty(filter.WorkpalceOption)? TolongFirst(filter.WorkpalceOption):null,
                WorkPlaceIdFake = !String.IsNullOrEmpty(filter.WorkpalceOption) ? _context.WorkPlaces.AsNoTracking().Where(d => d.Id == TolongFirst(filter.WorkpalceOption)).FirstOrDefault().Name:null,
                WorkPlaceWithChildren = !String.IsNullOrEmpty(filter.WorkpalceOption) ? ToBoolTwoChild(filter.WorkpalceOption):false
            };
            return new ResultDto<CreatFilterDto>
            {
                Data = result,
                IsSuccess = true,
                Message = "موفق"
            };
        }

        private long TolongFirst(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
    var v = s.Split("_").ToArray();
            long number;

            bool isParsable = Int64.TryParse(v.ElementAt(0), out number);

            if (isParsable)
                return number;
            else
                return 0;
            }
            return 0;

        }
        private bool ToBoolTwoChild(string s)
        {
            if (!String.IsNullOrEmpty(s))
            {
                var v = s.Split("_").ToArray();
                int number;
                bool isParsable = Int32.TryParse(v.ElementAt(1), out number);
                if (number > 0)
                {
                    return true;
                }
                return false;

            }
            return false;
        }

        public ResultDto GetAccessQuizById(long quizid, string username)
        {
            bool access = true;
            var filter = _context.QuizFilters.AsNoTracking().Where(p => p.QuizId == quizid)
           .FirstOrDefault();
            var user = _context.Users.AsNoTracking().Where(p => p.UserName == username)
          .FirstOrDefault();
            if (filter != null)
            {
                var result = new CreatFilterDto
                {
                    QuizId = filter.QuizId,
                    TypeDarajeh = (filter.TypeDarajeh) != null ? filter.TypeDarajeh : null,
                    UserList = filter.UserNameOption,
                    WorkPlaceId = !String.IsNullOrEmpty(filter.WorkpalceOption) ? TolongFirst(filter.WorkpalceOption) : null,
                    WorkPlaceIdFake = !String.IsNullOrEmpty(filter.WorkpalceOption) ? _context.WorkPlaces.AsNoTracking().Where(d => d.Id == TolongFirst(filter.WorkpalceOption)).FirstOrDefault().Name : null,
                    WorkPlaceWithChildren = !String.IsNullOrEmpty(filter.WorkpalceOption) ? ToBoolTwoChild(filter.WorkpalceOption) : false
                };

                if (result.WorkPlaceId!=null)
                {
                    if (result.WorkPlaceWithChildren)
                    {
                        access = _getChildrenWorkPlace.ExequteById(result.WorkPlaceId).Data.Where(p=>p==user.WorkPlaceId).Any();
                        if (!access)
                        {
                            return new ResultDto
                            {
                                IsSuccess = access
                            };
                        }
                    }
                    else
                    {
                        access = user.WorkPlaceId == result.WorkPlaceId ? true : false;
                        if (!access)
                        {
                            return new ResultDto
                            {
                                IsSuccess = access
                            };
                        }
                    }
                }
                if (result.TypeDarajeh!=null)
                {
                    var dj = (int)result.TypeDarajeh;
                    if (dj!=user.TypeDarajeh)
                    {
                        access = false;
                        if (!access)
                        {
                            return new ResultDto
                            {
                                IsSuccess = access
                            };
                        }
                    }
                }
                if (result.UserList!="")
                {
                    var ll = result.UserList.Split(",").ToList();
                    access = ll.Where(p => p.Contains(user.UserName)).Any() ? true : false;
                    if (!access)
                    {
                        return new ResultDto
                        {
                            IsSuccess = access
                        };
                    }
                }

            }

            return new ResultDto { 
            IsSuccess=access
            };
        }
    }
}
