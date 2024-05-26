namespace Appointmenting.API.Domain.DTOs
{
    public class TimeslotDTO 
    {
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public TimeslotDTO(string date, string time)
        {
            Date = date; Time = time;
        }
    }
    public class UpdateTimeslotDTO
    {
        public Guid ID { get; set; }
        public string Date { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public UpdateTimeslotDTO(Guid id,string date, string time)
        {
            ID = id; Date = date; Time = time;
        }
    }
}
