using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskHub.Core.Entities;

namespace TaskHub.Infrastructure.Data.Configurations
{
    public class RefreshToken : IEntityTypeConfiguration<Core.Entities.RefreshToken>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens");

            builder.HasKey(rt => rt.Id);


            builder.HasIndex(rt => rt.Token)
                .IsUnique();

            builder.Property(rt => rt.Token);

            builder.Property(rt => rt.Expires)
                .IsRequired();

            builder.Property(rt => rt.UserId)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(ev => ev.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
