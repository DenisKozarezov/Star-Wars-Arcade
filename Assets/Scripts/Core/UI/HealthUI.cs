using UnityEngine;
using TMPro;
using Zenject;
using Core.Player;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class HealthUI : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private LazyInject<PlayerModel> _player;

        [Inject]
        private void Construct(LazyInject<PlayerModel> player)
        {
            _player = player;
        }

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        private void Start()
        {
            OnHealthChanged(_player.Value.Health);
            _player.Value.HealthChanged += OnHealthChanged;
        }
        private void OnDestroy()
        {
            _player.Value.HealthChanged -= OnHealthChanged;
        }
        private void OnHealthChanged(int currentHealth)
        {
            _text.text = "Health: " + currentHealth.ToString();
        }
    }
}