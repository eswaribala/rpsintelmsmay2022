namespace MoviePublisher.Services
{
    public interface IMessageService
    {
        Task<string> PublishMovie(string topicName, string message, IConfiguration configuration);
    }
}
