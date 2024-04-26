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
    public class MessagesConfiguration : IEntityTypeConfiguration<Messages>
    {
        public void Configure(EntityTypeBuilder<Messages> builder)
        {
            // Đặt tên bảng trong cơ sở dữ liệu
            builder.ToTable("Messages");

            // Cấu hình thuộc tính Content
            builder.Property(m => m.Content)
                .IsRequired(); // Yêu cầu giá trị không được null


            builder.HasOne(m => m.User)
              .WithMany()
              .HasForeignKey(m => m.UserId)
              .OnDelete(DeleteBehavior.Restrict); // Không cho phép xóa người dùng khi có tin nhắn liên kết với họ

       
        }
    }
}
