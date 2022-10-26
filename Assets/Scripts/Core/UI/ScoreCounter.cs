using UnityEngine;
using TMPro;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class ScoreCounter : MonoBehaviour
    {
        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();          
        }
        public void SetScore(ushort score) => _text.text = $"Score: {score}";
    }
}