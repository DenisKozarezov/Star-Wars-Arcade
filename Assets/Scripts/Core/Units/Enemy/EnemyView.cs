using UnityEngine;

namespace Core.Units
{
    public class EnemyView : MonoBehaviour, ITransformable
    {
        private Vector2 _currentVelocity;

        [field: SerializeField] public Transform FirePoint { get; private set; }
        public Vector2 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public float Velocity => _currentVelocity.magnitude;

        public void Rotate(Quaternion rotation) => transform.rotation = rotation;
        public void Translate(Vector2 direction, float maxVelocity, float deacceleration)
        {
            transform.position = Vector2.SmoothDamp(Position, Position + direction, ref _currentVelocity, deacceleration, maxVelocity);
        }
        public void SetPosition(Vector2 position) => transform.position = position;
        public void SetActive(bool isActive) => gameObject.SetActive(isActive);
    }
}