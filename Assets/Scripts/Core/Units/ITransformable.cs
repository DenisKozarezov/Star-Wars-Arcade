using UnityEngine;

namespace Core.Units
{
    public interface ITransformable
    {
        Vector2 Position { get; }
        Quaternion Rotation { get; }
        void Translate(Vector2 direction);
        void SetPosition(Vector2 position);
        void Rotate(Quaternion rotation);
    }
}
