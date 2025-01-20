using AVIVA.Application.Interfaces;
using System;

namespace AVIVA.Infrastructure.Shared.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}