
using ChatSrever.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatServer.Infrastructure.Data.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Đặt tên bảng trong cơ sở dữ liệu
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        // Cấu hình thuộc tính Name
        builder.Property(u => u.Name)
            .HasMaxLength(100) // Giới hạn độ dài tối đa là 100 ký tự
            .IsRequired(); // Yêu cầu giá trị không được null

        // Cấu hình thuộc tính Account
        builder.Property(u => u.account)
            .IsRequired(); 

        // Cấu hình thuộc tính Password
        builder.Property(u => u.Password)
            .HasMaxLength(50) // Giới hạn độ dài tối đa là 50 ký tự
            .IsRequired(); // Yêu cầu giá trị không được null

        builder.Property(u => u.AvatarImageUrl)
            .HasMaxLength(200);

        builder.HasMany(u => u.RoomUser)
                      .WithOne()
                      .HasForeignKey(ru => ru.UserId)
                      .OnDelete(DeleteBehavior.Cascade);


    }
}
