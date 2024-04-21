


namespace ChatSrever.Domain.Entities;

public class Rooms : BaseAuditableEntity
{
    public string? Title { get; set; }

    public int QuantityUser { get; set; }

    public IList<RoomMessages> RoomMessages { get; private set; } = new List<RoomMessages>();
    public IList<RoomUser> RoomUser { get; private set; } = new List<RoomUser>();
}
