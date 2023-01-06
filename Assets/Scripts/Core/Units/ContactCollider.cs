using UnityEngine;

namespace Core.Units
{
    [RequireComponent(typeof(Collider2D))]
    public class ContactCollider : MonoBehaviour
    {
        public IUnit Owner { get; set; }
    }
}
