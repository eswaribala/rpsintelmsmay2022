using MongoDB.Driver;
using MovieConsumer.Models;

namespace MovieConsumer.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IMongoCollection<BsonMovie> _moviesCollection;

        public MovieRepository(IConfiguration configuration)
        {
            _configuration = configuration;           

            var mongoClient = new MongoClient(_configuration["ConnectionString"]);

            var database = mongoClient.GetDatabase(_configuration["DatabaseName"]);

             _moviesCollection = database.GetCollection<BsonMovie>(
              _configuration["MoviesCollectionName"]);

        }
        public void AddMovie(BsonMovie movie)
        {
           _moviesCollection.InsertOneAsync(movie);
        }
    }
}
