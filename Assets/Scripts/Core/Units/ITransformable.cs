using UnityEngine;

namespace Core.Units
{
    public interface ITransformable
    {
        Vector2 Position { get; }
        Quaternion Rotation { get; }
        float Velocity { get; }
        void Translate(Vector2 direction, float maxVelocity, float deacceleration);
        void SetPosition(Vector2 position);
        void Rotate(Quaternion rotation);
    }
}
