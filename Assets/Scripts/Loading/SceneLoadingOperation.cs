using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Core.Loading
{
    public class SceneLoadingOperation : ILoadingOperation
    {
        private readonly int _buildIndex;
        public string Description => "Loading scene...";
        public float Progress { get; private set; }
        public bool IsComplete => Progress == 1f;

        public SceneLoadingOperation(int buildIndex)
        {
            _buildIndex = buildIndex;
        }

        public async Task AwaitForLoad()
        {
            var operation = SceneManager.LoadSceneAsync(_buildIndex, LoadSceneMode.Additive);

#if UNITY_EDITOR
            if (operation == null)
            {
                throw new InvalidOperationException("There is no scene with such index: " + _buildIndex);
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