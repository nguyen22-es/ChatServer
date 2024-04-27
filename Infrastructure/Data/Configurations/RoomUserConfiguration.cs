using ChatSrever.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class RoomUserConfiguration : IEntityTypeConfiguration<RoomUser>
    {
        public void Configure(EntityTypeBuilder<RoomUser> builder)
        {
            // Đặt tên bảng trong cơ sở dữ liệu
            builder.ToTable("RoomUsers");

            // Thiết lập khóa chính kết hợp
            builder.HasKey(ru => new { ru.UserId, ru.RoomId });

            // Cấu hình khóa ngoại đến User
            builder.HasOne(ru => ru.User)
                .WithMany(u => u.RoomUser)
                .HasForeignKey(ru => ru.UserId);
            // Cấu hình khóa ngoại đến Room
            builder.HasOne(rm => rm.Rooms)
                .WithMany(r => r.RoomUser)
                .HasForeignKey(rm => rm.RoomId);

        }
    }
}
