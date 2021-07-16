namespace Bookme.Data.Models
{
    public class WeeklySchedule
    {
        public int Id { get; init; }
        public int BookingConfigurationId { get; set; }
        public BookingConfiguration BookingConfiguration { get; set; }
        public bool Monday { get; set; }
        public bool Tuesdey { get; set; }
        public bool Wednesday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }
        public bool Sunday { get; set; }
    }
}
