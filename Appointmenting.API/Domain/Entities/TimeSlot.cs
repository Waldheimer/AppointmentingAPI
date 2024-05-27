﻿using Appointmenting.API.Domain.DTOs;

namespace Appointmenting.API.Domain.Entities
{
    public record TimeSlot(DateOnly day, TimeOnly time)
    {
        public Guid Id { get; set; } = new Guid();

        public static TimeSlot FromDTO(TimeslotDTO dto)
        {
            return new TimeSlot(DateOnly.Parse(dto.Date), TimeOnly.Parse(dto.Time));
        }
        public static TimeSlot FromDTO(UpdateTimeslotDTO dto)
        {
            TimeSlot ts = new TimeSlot(DateOnly.Parse(dto.Date), TimeOnly.Parse(dto.Time));
            ts.Id = dto.ID;

            return ts;
        }
    }
}
