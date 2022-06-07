using MongoDB.Bson.Serialization.Attributes;

namespace MovieConsumer.Models
{
    public class BsonMovie
    {
        [BsonId]
        public long MovieId { get; set; }
        public string? Title { get; set; }
        public DateTime ReleasedDate { get; set; }

        public string? DirectorName { get; set; }
    }
}
