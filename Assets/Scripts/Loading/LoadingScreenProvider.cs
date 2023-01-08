using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.Loading
{
    public class LoadingScreenProvider : ILoadingScreenProvider
    {
        private async Task<LoadingScreen> LoadScreenAsync()
        {
            var load = Resources.LoadAsync<GameObject>("Prefabs/UI/Loading Screen");
            while (!load.isDone)
            {
                await Task.Yield();
            }

            var obj = GameObject.Instantiate(load.asset as GameObject);
            GameObject.DontDestroyOnLoad(obj);
            return obj.GetComponentInChildren<LoadingScreen>();
        }
        public async Task LoadAndDestroyAsync(Queue<LazyLoadingOperation> operations)
        {
            var loadingScreen = await LoadScreenAsync();

            await loadingScreen.LoadAndDestroyAsync(operations);

            GameObject.Destroy(loadingScreen.gameObject);
        }
    }
}