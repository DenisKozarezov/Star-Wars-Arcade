using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Core.Loading;

namespace Core.UI
{
    public class MenuController : MonoBehaviour
    {
        [SerializeField]
        private Button _startGame;
        [SerializeField]
        private Button _quit;

        private ILoadingScreenProvider _loadingScreenProvider;

        [Inject]
        private void Construct(ILoadingScreenProvider loadingScreenProvider)
        {
            _loadingScreenProvider = loadingScreenProvider;
        }

        private void Awake()
        {
            _startGame.onClick.AddListener(OnStartGame);
            _quit.onClick.AddListener(OnQuit);
        }

        private void OnStartGame()
        {
            Queue<LazyLoadingOperation> operations = new Queue<LazyLoadingOperation>();
            Func<ILoadingOperation> cleanupSceneOperation = () => new SceneCleanupOperation(Constants.Scenes.MainMenu);
            Func<ILoadingOperation> gameLoadingOperation = () => new SceneLoadingOperation(Constants.Scenes.GameScene);
            Func<ILoadingOperation> pressAnyButtonOperation = () => new PressAnyButtonOperation();
            operations.Enqueue(cleanupSceneOperation);
            operations.Enqueue(gameLoadingOperation);
            operations.Enqueue(pressAnyButtonOperation);
            _loadingScreenProvider.LoadAndDestroyAsync(operations);
        }

        private void OnQuit()
        {
            if (!Application.isEditor)
            {
                Application.Quit();
            }
#if UNITY_EDITOR
            else
            {
                UnityEditor.EditorApplication.isPlaying = false;
            }
#endif
        }
    }
}
