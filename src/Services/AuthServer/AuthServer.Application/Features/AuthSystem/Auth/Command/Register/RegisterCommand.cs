using Application.DTOs;
using AuthServer.Application.Interface;
using AuthServer.Domain.Entities;
//using AutoMapper;
//using Base.Application.Mapping;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Auth
{
    #region Request
    public class RegisterCommand
    : IRequest<BaseResult>, IUsernameDTO, IPasswordDTO, IMobileDTO, IEmailDTO
    {
        #region Constructors
        public RegisterCommand() { }
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
        #endregion


        #region Mapping
        public User MapToUser()
        {
            var user = new User
            {
                UserName = Username,
                Name = Name,
                Email = Email,
                PhoneNumber = Mobile
            };
            return user;
        }


        //public void Mapping(Profile profile)
        //{
        //    profile.CreateMap<User, RegisterCommand>()
        //        .ForMember(d => d.Mobile, opt => opt.MapFrom(s => s.PhoneNumber))
        //        .ReverseMap()
        //        .ForMember(d => d.PhoneNumber, opt => opt.MapFrom(s => s.Mobile));
        //}
        #endregion
    } 
    #endregion



    #region Handler
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, BaseResult>
    {
        private readonly IUnitOfWork _uow;
        public RegisterCommandHandler(IUnitOfWork uow/*, IMapper mapper*/)
        {
            _uow = uow;
        }


        public async Task<BaseResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var user = request.MapToUser();
            
            var id = await _uow.Users.Register(user, request.Password);

            // TODO: ارسال پیامک

            return new BaseResult(true);
        }
    }

    #endregion
}
