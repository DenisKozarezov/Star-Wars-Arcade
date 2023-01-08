using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Loading
{
    public interface ILoadingScreenProvider
    {
        Task LoadAndDestroyAsync(Queue<LazyLoadingOperation> operations);
    }
}