using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Core.Loading
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField]
        private Image _progressBar;
        [SerializeField]
        private TextMeshProUGUI _description;
        [SerializeField, Range(0f, 1f)]
        private float _progressBarSpeed;

        private Tweener _tweener;

        public async Task LoadAndDestroyAsync(Queue<LazyLoadingOperation> operations)
        {
            foreach (var lazyOperation in operations)
            {
                ILoadingOperation loadingOperation = lazyOperation.Value;

                ResetFill();
                _description.text = loadingOperation.Description;
                while (!loadingOperation.IsComplete)
                {
                    SetFillAmount(loadingOperation.Progress);
                    await loadingOperation.AwaitForLoad();
                }
            }
        }
        private void SetFillAmount(float value)
        {
            _tweener?.Kill();
            _tweener = _progressBar.DOFillAmount(value, _progressBarSpeed);
        }
        private void ResetFill()
        {
            _tweener?.Kill();
            _progressBar.fillAmount = 0f;
        }
    }
}