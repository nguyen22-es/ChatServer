

namespace ChatSrever.Domain.Entities
{
    public class RoomUser : BaseEntity
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public User User { get; set; }

        public Rooms Rooms { get; set; }
    }
}
