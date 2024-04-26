using ChatSrever.Domain.Common;
using System.Threading.Tasks;


namespace ChatServer.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(BaseEntity domainEvent);
    }
}
