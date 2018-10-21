using PluralsightWinFormsDemoApp.Model;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp
{
    public interface IPodcastLoader
    {
        Task UpdatePodcast(Podcast podcast);
    }
}