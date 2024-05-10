using AutoMapper;
using ChatSrever.Domain.Entities;
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
        public int UserSend { get; set; }
        public string nameSend { get; set; }

        public DateTimeOffset timeSend { get; set; }
        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<Messages, MessagesDot>()
                     .ForMember(dest => dest.nameSend, opt => opt.MapFrom(src => src.User.Name))
                     .ForMember(dest => dest.UserSend, opt => opt.MapFrom(src => src.UserId));
            }
        }
    }
}
