using System.Text;
using UnityEngine;
using TMPro;

namespace Core.UI
{
    [RequireComponent(typeof(RectTransform))]
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class GameTimer : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private readonly StringBuilder _builder = new StringBuilder();

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        private void OnDestroy()
        {
            _builder.Clear();
        }
        public void SetTime(float elapsedTime)
        {
            _builder.Clear();

            int elapsed = (int)(elapsedTime);

            int minutes = elapsed / 60;
            if (minutes < 10) _builder.Append("0");
            _builder.AppendFormat("{0}:", minutes);

            int seconds = elapsed - minutes * 60;
            if (seconds < 10) _builder.Append("0");
            _builder.Append(seconds);
            _text.text = _builder.ToString();
        }
    }
}