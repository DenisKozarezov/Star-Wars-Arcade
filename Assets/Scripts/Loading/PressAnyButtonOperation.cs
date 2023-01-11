using System.Threading.Tasks;

namespace Core.Loading
{
    public class PressAnyButtonOperation : ILoadingOperation
    {
        public string Description => "Press any button...";
        public float Progress { get; private set; }
        public bool IsCompleted => UnityEngine.Input.anyKeyDown;

        public async Task AwaitForLoad()
        {
            while (!IsCompleted)
            {
                await Task.Yield();
            }
            Progress = 1f;
        }
    }
}