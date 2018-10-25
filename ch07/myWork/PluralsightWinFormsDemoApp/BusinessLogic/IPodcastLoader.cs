using PluralsightWinFormsDemoApp.Model;
using System.Threading.Tasks;

namespace PluralsightWinFormsDemoApp.BusinessLogic
{
    public interface IPodcastLoader
    {
        Task UpdatePodcast(Podcast podcast);
    }
}