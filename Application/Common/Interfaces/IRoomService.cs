using Application.Dot;
using ChatSrever.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IRoomService
    {
        Task<List<Rooms>> GetRoomsAsync();

        Task<Rooms> UpdateRoomsAsnc();

        Task DeleteRoomsAsnc();
    }
}
