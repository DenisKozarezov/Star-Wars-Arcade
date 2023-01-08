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
            _coordinates.text = $"X: {coordinates.x.ToString("F")} Y: {coordinates.y.ToString("F")}";
        }
        private void SetVelocity(float velocity)
        {
            _velocity.text = $"Velocity: {velocity.ToString("F3")}";
        }
    }
}