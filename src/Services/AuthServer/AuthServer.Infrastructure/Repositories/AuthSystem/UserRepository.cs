using AuthServer.Application.Interface;
using Microsoft.AspNetCore.Identity;
using AuthServer.Domain.Entities;
using Application.Exceptions;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using AuthServer.Application.Features.Users;

namespace AuthServer.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        protected UserManager<User> _userManager { get; }
        public UserRepository(DbContext context, UserManager<User> userManager) : base(context)
        {
            _userManager = userManager;
        }


        #region ثبت نام کاربر
        /// <summary>
        /// ثبت نام کاربر
        /// </summary>
        public async Task<long> Register(User user, string password)
        {
            var res = await _userManager.CreateAsync(user, password);
            if (!res.Succeeded)
                throw new ValidationException(res.Errors.Select(x => x.Description).ToList());

            return user.Id;
        }
        #endregion



        #region جستجو لیست کاربران
        /// <summary>
        /// جستجو لیست کاربران
        /// </summary>
        public async Task<ItemListDTO<UserListItemDTO>> GetList(UserGetListQuery search)
        {
            var filter = search.GetFilter();
            var totalCount = await CountAsync();
            var filteredCount = await CountAsync(filter);
            var data = await GetDTOAsync(
                UserListItemDTO.UserSelector,
                filter,
                x => x.OrderByDescending(z => z.Id),
                (search.Page.Value - 1) * search.PageLength.Value,
                search.PageLength.Value
                );


            var model = new ItemListDTO<UserListItemDTO>(
                search.Page.Value,
                search.PageLength.Value,
                totalCount,
                filteredCount,
                data
                );

            return model;
        }
        #endregion



        #region گرفتن اطلاعات پایه کاربر با آیدی
        /// <summary>
        /// اطلاعات کاربر با آیدی
        /// </summary>
        public async Task<UserBaseInfoDTO> GetBaseInfo(long id)
        {
            var user = await GetOneDTOAsync(
                UserBaseInfoDTO.UserSelector,
                x => x.Id == id
                );
            return user;
        }
        #endregion



        #region گرفتن اطلاعات کاربر با آیدی
        /// <summary>
        /// اطلاعات کاربر با آیدی
        /// </summary>
        public async Task<UserItemDTO> GetDetails(long id)
        {
            var user = await GetOneDTOAsync(
                UserItemDTO.UserSelector,
                x => x.Id == id
                );
            return user;
        }
        #endregion




        #region گرفتن کاربر با نام کاربری
        /// <summary>
        /// گرفتن کاربر با نام کاربری
        /// </summary>
        /// <param name="username">نام کاربری کاربر</param>
        /// <returns></returns>
        public async Task<User> GetByUsername(string username)
        {
            var user = await FirstOrDefaultAsync(x => x.UserName == username);
            return user;
        }
        #endregion



        #region افزودن کاربر
        /// <summary>
        /// ثبت نام کاربر
        /// </summary>
        public async Task<long> Create(User user, string password)
        {
            var res = await _userManager.CreateAsync(user, password);
            if (!res.Succeeded)
                throw new ValidationException(res.Errors.Select(x => x.Description).ToList());

            return user.Id;
        }
        #endregion


    }
}
