using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.Loading
{
    public class SceneCleanupOperation : ILoadingOperation
    {
        private readonly int _buildIndex;
        public string Description => "Cleaning scene...";
        public float Progress { get; private set; }
        public bool IsCompleted => Progress == 1f;

        public SceneCleanupOperation(int buildIndex)
        {
            _buildIndex = buildIndex;
        }

        public async Task AwaitForLoad()
        {
            var operation = SceneManager.UnloadSceneAsync(_buildIndex);

#if UNITY_EDITOR
            if (operation == null)
            {
                Logger.Debug($"Unable to clean the scene with such index <b><color=yellow>{_buildIndex}</color></b>.", Logger.LogType.Warning);
            }
#endif

            while (!operation.isDone)
            {
                Progress = operation.progress;
                await Task.Yield();
            }
            Progress = 1f;
        }
    }
}