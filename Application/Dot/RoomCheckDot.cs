using ChatSrever.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dot
{
    public class RoomCheckDot
    {
        public IList<MessagesDot> messagesDot {  get; set; }

        public RoomDot RoomDot { get; set; }
    }
}
