using System.Threading.Tasks;

namespace Core.Loading
{
    public interface ILoadingOperation
    {
        string Description { get; }
        float Progress { get; }
        bool IsCompleted { get; }
        Task AwaitForLoad();
    }
}