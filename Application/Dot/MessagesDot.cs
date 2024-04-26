using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dot
{
    public class MessagesDot
    {
        public int id {  get; set; }
        public string content { get; set; }

        public string nameSend { get; set; }

        public DateTimeOffset timeSend { get; set; }
    }
}
