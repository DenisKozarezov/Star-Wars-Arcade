using System;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LevelBound : MonoBehaviour
    {
        private BoxCollider2D _collider;
        private Camera _camera;
        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
            _collider.isTrigger = true;
            _camera = Camera.main;
        }
        private void Start()
        {
            UpdateBounds();
        }

#if UNITY_EDITOR
        private void Update()
        {
            UpdateBounds();
        }
#endif
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out IDisposable disposable))
            {
                disposable.Dispose();
            }
        }

        private Vector2 GetWrappedPosition(Vector2 worldPosition)
        {
            bool xBoundResult = Mathf.Abs(worldPosition.x) > Mathf.Abs(_collider.bounds.min.x);
            bool yBoundResult = Mathf.Abs(worldPosition.y) > Mathf.Abs(_collider.bounds.min.y);

            if (xBoundResult) worldPosition.x *= -1f;
            if (yBoundResult) worldPosition.y *= -1f;
            return worldPosition;
        }
        private void UpdateBounds()
        {
            float ySize = _camera.orthographicSize * 2f;
            Vector2 boxCollider = new Vector2(ySize * _camera.aspect, ySize);
            _collider.size = boxCollider;
        }
    }
}