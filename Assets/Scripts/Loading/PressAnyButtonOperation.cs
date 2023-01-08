using System.Threading.Tasks;

namespace Core.Loading
{
    public class PressAnyButtonOperation : ILoadingOperation
    {
        public string Description => "Press any button...";
        public float Progress { get; private set; }
        public bool IsComplete => Progress == 1f;

        public async Task AwaitForLoad()
        {
            while (!UnityEngine.Input.anyKeyDown)
            {
                await Task.Yield();
            }
            Progress = 1f;
        }
    }
}