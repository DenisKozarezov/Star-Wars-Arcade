using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.Loading
{
    public class SceneCleanupOperation : ILoadingOperation
    {
        private readonly int _buildIndex;
        public string Description => "Cleaning scene...";
        public float Progress { get; private set; }
        public bool IsComplete => Progress == 1f;

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
                Logger.Debug("<b><color=yellow>Please, initialize project with Bootstrap scene when you transite between scenes in Playmode.</color></b>", Logger.LogType.Warning);
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