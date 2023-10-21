using FileService.Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthServer.Infrastructure.Persistence
{
    public class MediaFileConfig : BaseEntityConfig<MediaFile> //IEntityTypeConfiguration<MediaFile>
    {
        public override void CustomeConfigure(EntityTypeBuilder<MediaFile> builder)
        {
            builder.ToTable("MediaFiles");

            #region Properties
            builder.Property(e => e.TitleFa)
                    .IsRequired()
                    .HasMaxLength(300);

            builder.Property(e => e.TitleEn)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(e => e.FileName)
                .IsRequired()
                .HasMaxLength(300);

            builder.Property(e => e.Size)
                .IsRequired();

            builder.Property(e => e.Format)
                .IsRequired();

            builder.Property(e => e.Group)
                .IsRequired();
            #endregion
        }

    }
}
