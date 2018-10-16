using PluralsightWinFormsDemoApp.Model;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp
{
    public interface IPodcastLoader
    {
        Task LoadPodcast(Podcast podcast);
    }
}