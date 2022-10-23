using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Core.Infrastructure
{
    public class ProjectBootstrap : MonoBehaviour
    {
        [SerializeField]
        private EventSystem _eventSystem;
        [SerializeField]
        private string _nextSceneName;
 
        private void Awake()
        {
            DontDestroyOnLoad(_eventSystem);
        }
        private void Start()
        {
            SceneManager.LoadSceneAsync(_nextSceneName, LoadSceneMode.Additive);
        }
    }
}