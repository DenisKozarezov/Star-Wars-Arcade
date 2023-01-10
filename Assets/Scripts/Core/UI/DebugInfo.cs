using UnityEngine;
using Zenject;
using Core.Player;
using Core.Units;
using TMPro;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DebugInfo : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _coordinates;
        [SerializeField]
        private TextMeshProUGUI _velocity;

        private ITransformable _player;

        [Inject]
        private void Construct(LazyInject<PlayerController> player)
        {
            _player = player.Value.Transformable;
        }

        private void Update()
        {
            SetCoordinates(_player.Position);
            SetVelocity(_player.Velocity);
        }

        private void SetCoordinates(Vector2 coordinates)
        {
            _coordinates.text = string.Format("X: {0:F} Y: {1:F}", coordinates.x, coordinates.y);
        }
        private void SetVelocity(float velocity)
        {
            _velocity.text = string.Format("Velocity: {0:F3}", velocity);
        }
    }
}