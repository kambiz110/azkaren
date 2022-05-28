using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azmoon.Application.Interfaces.User;
using Azmoon.Application.Service.User.Command;

namespace Azmoon.Application.Interfaces.Facad
{
   public interface IUserFacad
    {
        IGetAllUser GetUsers { get; }
        ICreateUser CreateUser { get; }
        IAddRoleToUser AddRoleToUser { get; }
        IFindUser FindUserById { get; }
        IGetUserForAddRole GetUserForAddRole { get; }
        IDeleteRoleInUser DeleteRoleInUser { get; }
        IGetChildrenUser GetChildrenUser { get; }
        IRegisterUser RegisterUser { get; }
        IUpdateProfile updateProfile { get; }
        IForgotPasswordService forgotPassword { get; }
 


    }
}
