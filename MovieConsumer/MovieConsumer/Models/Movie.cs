namespace MovieConsumer.Models
{
    public class Movie
    {
        public long MovieId { get; set; }
        public string? Title { get; set; }
        public DateTime ReleasedDate { get; set; }

        public string? DirectorName { get; set; }
    }
}
