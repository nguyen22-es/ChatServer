

namespace ChatSrever.Domain.Entities
{
    public class RoomMessages
    {
        public int RoomId { get; set; }
        public int MessagesId { get; set; }


        public Rooms Rooms { get; set; }
        public Messages Messages { get; set; }
    }
}
