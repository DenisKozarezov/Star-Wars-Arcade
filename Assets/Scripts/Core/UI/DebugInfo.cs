using TMPro;
using UnityEngine;
using Zenject;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class DebugInfo : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _coordinates;
        [SerializeField]
        private TextMeshProUGUI _velocity;

        private void Construct()
        {

        }

        private void Awake()
        {
            SetCoordinates(Vector2.zero);
            SetVelocity(0f);
        }

        private void SetCoordinates(Vector2 coordinates)
        {
            _coordinates.text = $"X: {coordinates.x} Y: {coordinates.y}";
        }
        private void SetVelocity(float velocity)
        {
            _velocity.text = velocity.ToString();
        }
    }
}