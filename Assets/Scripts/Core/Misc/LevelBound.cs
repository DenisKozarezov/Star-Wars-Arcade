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
            if (collision.TryGetComponent(out Bullet bullet))
            {
                bullet.Dispose();
            }

            if (collision.TryGetComponent(typeof(IUnit), out Component unit))
            {
                Vector2 point = collision.ClosestPoint(unit.transform.position);
                unit.transform.position = GetWrappedPosition(point);
            }
        }

        private Vector2 GetWrappedPosition(Vector2 worldPosition)
        {
            bool xBoundResult = Mathf.Abs(worldPosition.x) > Mathf.Abs(_collider.bounds.min.x);
            bool yBoundResult = Mathf.Abs(worldPosition.y) > Mathf.Abs(_collider.bounds.min.y);

            if (xBoundResult && yBoundResult) 
                return Vector2.Scale(worldPosition, Vector2.one * -1f);
            else if (xBoundResult) 
                return new Vector2(worldPosition.x * -1f, worldPosition.y);
            else if (yBoundResult) 
                return new Vector2(worldPosition.x, worldPosition.y * -1f);
            
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