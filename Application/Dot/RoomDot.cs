using AutoMapper;
using ChatSrever.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dot
{
    public class RoomDot
    {
        public int id { get; set; }

        public string? Title { get; set; }

        public int QuantityUser { get; set; }

        public bool IsGroup { get; set; }


        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Rooms, RoomDot>();
            }
        }

    }
}
