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
    public class RoomMessagesConfiguration : IEntityTypeConfiguration<RoomMessages>
    {
        public void Configure(EntityTypeBuilder<RoomMessages> builder)
        {
            // Đặt tên bảng trong cơ sở dữ liệu
            builder.ToTable("RoomMessages");

            // Thiết lập khóa chính kết hợp
            builder.HasKey(rm => new { rm.RoomId, rm.MessagesId });

            // Cấu hình khóa ngoại đến Room
            builder.HasOne(rm => rm.Rooms)
                .WithMany(r => r.RoomMessages)
                .HasForeignKey(rm => rm.RoomId);

            // Cấu hình khóa ngoại đến Messages
            builder.HasOne(rm => rm.Messages)
                .WithOne(m => m.RoomMessage)
                .HasForeignKey<RoomMessages>(rm => rm.RoomId);


        }
    }
}
