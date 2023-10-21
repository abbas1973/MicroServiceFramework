using FileService.Application.Interface;
using FileService.Domain.Entities;
using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;

namespace FileService.Infrastructure.Repositories
{
    public class MediaFileRepository : Repository<MediaFile>, IMediaFileRepository
    {
        public MediaFileRepository(DbContext context) : base(context)
        {
        }

    }
}
