using MovieConsumer.Models;

namespace MovieConsumer.Repositories
{
    public interface IMovieRepository
    {
        void AddMovie(BsonMovie movie);

    }
}
