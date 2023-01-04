using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Core.Infrastructure
{
    public class ProjectBootstrap : MonoBehaviour
    {
        [SerializeField]
        private EventSystem _eventSystem;
 
        private void Awake()
        {
            DontDestroyOnLoad(_eventSystem);
        }
        private void Start()
        {
            SceneManager.LoadSceneAsync(Constants.Scenes.GameScene, LoadSceneMode.Additive);
        }
    }
}