using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Collider2D))]
    public class LevelBound : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Bullet bullet))
            {
                bullet.Dispose();
            }
        }
    }
}