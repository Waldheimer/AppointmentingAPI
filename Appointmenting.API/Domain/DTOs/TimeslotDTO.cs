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
}
