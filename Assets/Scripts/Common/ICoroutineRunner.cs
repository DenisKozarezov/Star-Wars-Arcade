using System.Collections;
using UnityEngine;

namespace Core
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(string coroutineName);
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(string coroutineName);
        void StopCoroutine(Coroutine coroutine);
    }

    public class AsyncProcessor : MonoBehaviour, ICoroutineRunner { }
}
