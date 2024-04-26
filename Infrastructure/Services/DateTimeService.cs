using System;
using Application.Common.Interfaces;

namespace ChatServer.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}
