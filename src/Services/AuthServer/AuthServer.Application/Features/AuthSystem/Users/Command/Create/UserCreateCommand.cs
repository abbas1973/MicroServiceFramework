using Application.DTOs;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserCreateCommand
    : IRequest<BaseResult<long>>, IUsernameDTO, IPasswordDTO, IMobileDTO, IEmailDTO
    {
        #region Constructors
        public UserCreateCommand() {
            Roles = new List<long>();
        }
        #endregion


        #region Properties
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }

        [Display(Name = "کلمه عبور")]
        public string Password { get; set; }

        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }

        [Display(Name = "ایمیل")]
        public string Email { get; set; }

        [Display(Name = "نام کاربر")]
        public string Name { get; set; }

        [Display(Name = "نقش های کاربر")]
        public List<long> Roles { get; set; }
        #endregion


        #region Mapping
        public User MapToUser()
        {
            var user = new User
            {
                UserName = Username,
                Name = Name,
                Email = Email,
                PhoneNumber = Mobile,
                IsEnabled = true,
                PhoneNumberConfirmed = true,
                UserRoles = Roles.Select(x => new UserRole()
                {
                    RoleId = x
                }).ToList()
            };
            return user;
        }
        #endregion
    }
    #endregion



    #region Handler
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand, BaseResult<long>>
    {
        private readonly IUnitOfWork _uow;
        public UserCreateCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<BaseResult<long>> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            var user = request.MapToUser();
            var id = await _uow.Users.Create(user, request.Password);
            return new BaseResult<long>(id);
        }
    }

    #endregion
}
