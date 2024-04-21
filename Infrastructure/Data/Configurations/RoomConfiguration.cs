
using ChatSrever.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChatServer.Infrastructure.Data.Configurations;

public class RoomsConfiguration : IEntityTypeConfiguration<Rooms>
{
    public void Configure(EntityTypeBuilder<Rooms> builder)
    {
        // Đặt tên bảng trong cơ sở dữ liệu
        builder.ToTable("Rooms");

        // Cấu hình thuộc tính Title
        builder.Property(r => r.Title)
            .HasMaxLength(200) // Giới hạn độ dài tối đa là 200 ký tự
            .IsRequired(); // Yêu cầu giá trị không được null

        // Cấu hình thuộc tính QuantityUser
        builder.Property(r => r.QuantityUser)
            .IsRequired(); // Yêu cầu giá trị không được null

        // Cấu hình mối quan hệ với RoomMessages
        builder.HasMany(r => r.RoomMessages)
            .WithOne(rm => rm.Rooms)
            .HasForeignKey(rm => rm.RoomId);

        // Cấu hình mối quan hệ với RoomUser
        builder.HasMany(r => r.RoomUser)
            .WithOne(ru => ru.Rooms)
            .HasForeignKey(ru => ru.RoomId);
    }
}
