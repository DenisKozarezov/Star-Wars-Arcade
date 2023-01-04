using UnityEngine;
using Core.Units;

namespace Core.Player
{
    public class PlayerView : MonoBehaviour, ITransformable
    {
        [field: SerializeField] public Transform FirePoint { get; private set; }
        public Vector2 Position => transform.position;
        public Quaternion Rotation => transform.rotation;
        public void Rotate(Quaternion rotation) => transform.rotation = rotation;
        public void Translate(Vector2 direction) => transform.Translate(direction, Space.World);
        public void SetPosition(Vector2 position) => transform.position = position;
    }
}