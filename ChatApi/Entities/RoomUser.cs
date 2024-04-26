

namespace ChatSrever.Domain.Entities
{
    public class RoomUser 
    {
        public int UserId { get; set; }
        public int RoomId { get; set; }

        public User User { get; set; }

        public Rooms Rooms { get; set; }
    }
}
