using Application.DTOs;
using Application.Interface;
using AuthServer.Application.Features.Users;
using AuthServer.Domain.Entities;

namespace AuthServer.Application.Interface
{
    public interface IUserRepository : IBaseRepository<User>
    {
        /// <summary>
        /// ثبت نام کاربر
        /// </summary>
        Task<long> Register(User user, string password);


        /// <summary>
        /// جستجو لیست کاربران
        /// </summary>
        Task<ItemListDTO<UserListItemDTO>> GetList(UserGetListQuery search);


        /// <summary>
        /// اطلاعات کاربر با آیدی
        /// </summary>
        Task<UserItemDTO> GetDetails(long id);


        /// <summary>
        /// اطلاعات کاربر با آیدی
        /// </summary>
        Task<UserBaseInfoDTO> GetBaseInfo(long id);


        /// <summary>
        /// گرفتن کاربر با نام کاربری
        /// </summary>
        /// <param name="username">نام کاربری کاربر</param>
        /// <returns></returns>
        Task<User> GetByUsername(string username);


        /// <summary>
        /// افزودن کاربر
        /// </summary>
        Task<long> Create(User user, string password);

    }
}
