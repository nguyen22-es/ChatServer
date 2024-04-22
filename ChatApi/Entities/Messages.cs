

namespace ChatSrever.Domain.Entities
{
    public class Messages: BaseAuditableEntity
    {
        public string Content { get; set; }

        public int UserId {  get; set; }

        public int RoomMessageId { get; set; }

        public User User {  get; set; }

        public RoomMessages RoomMessage { get; set; }

    }
}
