



namespace ChatSrever.Domain.Entities;

public class User : BaseAuditableEntity
{

    public string? Name { get; set; }

    public string? account { get; set; }

    public string? Password { get; set; }

    public IList<RoomUser> RoomUser { get; private set; } = new List<RoomUser>();

}
