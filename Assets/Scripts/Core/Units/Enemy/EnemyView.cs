using UnityEngine;

namespace Core.Units
{
    public class EnemyView : MonoBehaviour, ITransformable
    {
        [field: SerializeField] public Transform FirePoint { get; private set; }
        public Vector2 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public void Rotate(Quaternion rotation) => transform.rotation = rotation;
        public void Translate(Vector2 direction) => transform.Translate(direction, Space.World);
        public void SetPosition(Vector2 position) => transform.position = position;
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}