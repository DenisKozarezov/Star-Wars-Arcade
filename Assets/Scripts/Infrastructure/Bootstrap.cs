using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Loading;
using Zenject;

namespace Core.Infrastructure
{
    public class Bootstrap : MonoBehaviour
    {
        private ILoadingScreenProvider _loadingScreenProvider;

        [Inject]
        private void Construct(ILoadingScreenProvider provider)
        {
            _loadingScreenProvider = provider;
        }

        private IEnumerator Start()
        {
            yield return InitExternalServices();
            LoadProcess();
        }

        private IEnumerator InitExternalServices()
        {
            // init Steam API...
            // init Battle.Net API...
            // etc...
            yield return null;
        }
        private void LoadProcess()
        {
            Queue<LazyLoadingOperation> operations = new Queue<LazyLoadingOperation>();
            Func<ILoadingOperation> menuLoadingOperation = () => new SceneLoadingOperation(Constants.Scenes.MainMenu);
            operations.Enqueue(menuLoadingOperation);
            _loadingScreenProvider.LoadAndDestroyAsync(operations);
        }
    }
}