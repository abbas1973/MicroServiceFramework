using Application.DTOs;
using Application.Exceptions;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserUpdateCommand
    : IRequest<BaseResult>, IBaseEntityDTO, IUsernameDTO, IMobileDTO, IEmailDTO
    {
        #region Constructors
        public UserUpdateCommand()
        {
            Roles = new List<long>();
        }
        #endregion


        #region Properties
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "نام کاربر")]
        public string Name { get; set; }

        [Display(Name = "فعال/غیر فعال")]
        public bool IsEnabled { get; set; }

        [Display(Name = "نقش های کاربر")]
        public List<long> Roles { get; set; }
        #endregion


        #region Mapping
        public void MapToUser(ref User user)
        {
            user.Id = Id;
            user.UserName = Username;
            user.Name = Name;
            user.Email = Email;
            user.PhoneNumber = Mobile;
            user.IsEnabled = IsEnabled;
            user.UserRoles.Clear();
            user.UserRoles = Roles.Select(x => new UserRole()
            {
                RoleId = x
            }).ToList();
        }
        #endregion
    }
    #endregion



    #region Handler
    public class UserUpdateCommandHandler : IRequestHandler<UserUpdateCommand, BaseResult>
    {
        private readonly IUnitOfWork _uow;
        public UserUpdateCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<BaseResult> Handle(UserUpdateCommand request, CancellationToken cancellationToken)
        {
            #region ویرایش کاربر
            var user = await _uow.Users.FirstOrDefaultAsync(
                x => x.Id == request.Id,
                includes: x => x.Include(z => z.UserRoles));
            if (user == null)
                throw new NotFoundException("کاربر مورد نظر یافت نشد!");

            _uow.UserRoles.RemoveRange(user.UserRoles);
            request.MapToUser(ref user);
            _uow.Users.Update(user);
            #endregion

            await _uow.CommitAsync();
            return new BaseResult(true);
        }
    }

    #endregion
}
