

namespace ChatSrever.Domain.Entities
{
    public class Messages: BaseAuditableEntity
    {
        public string Content { get; set; }

        public int UserId {  get; set; }

        public User User {  get; set; }

        public RoomMessages RoomMessage { get; set; }

        public RoomUser RoomUser { get; set; }
    }
}
