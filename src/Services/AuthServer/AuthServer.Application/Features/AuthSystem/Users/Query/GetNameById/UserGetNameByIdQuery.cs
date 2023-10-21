﻿using Application.DTOs;
using Application.Exceptions;
using AuthServer.Application.Interface;
using MediatR;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace AuthServer.Application.Features.Users
{
    #region Request
    public class UserGetNameByIdQuery : IRequest<BaseResult<string>>, IBaseEntityDTO
    {
        #region Constructors
        public UserGetNameByIdQuery(long id)
        {
            Id = id;
        }
        #endregion


        #region Properties
        [Display(Name = "شناسه")]
        public long Id { get; set; }
        #endregion
    }
    #endregion


    #region Handler
    public class UserGetNameByIdQueryHandler : IRequestHandler<UserGetNameByIdQuery, BaseResult<string>>
    {
        protected IUnitOfWork _uow { get; }
        protected ILogger<UserGetNameByIdQueryHandler> _logger { get; }
        public UserGetNameByIdQueryHandler(IUnitOfWork uow, ILogger<UserGetNameByIdQueryHandler> logger)
        {
            _uow = uow;
            _logger = logger;
        }

        public async Task<BaseResult<string>> Handle(UserGetNameByIdQuery request, CancellationToken cancellationToken)
        {
            var res = await _uow.Users.GetOneDTOAsync(x => x.Name, x => x.Id == request.Id);
            if (res == null)
                throw new NotFoundException($"نام کاربر با آیدی {request.Id} یافت نشد!");
            return new BaseResult<string>(res);
        }
    }
    #endregion
}
