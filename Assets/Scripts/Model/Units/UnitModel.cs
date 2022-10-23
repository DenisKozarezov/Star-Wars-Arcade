using UnityEngine;

namespace Core.Models.Units
{
    [CreateAssetMenu(menuName = "Configuration/Units/Unit Model")]
    public class UnitModel : ScriptableObject
    {
        [Header("Settings")]
        [SerializeField]
        private string _displayName;
        [SerializeField, TextArea]
        private string _description;
        [Space, SerializeField]
        private byte _health;
        [SerializeField, Range(0f, 10f)]
        private float _velocity;
        [SerializeField, Range(0f, 10f)]
        private float _rotationSpeed;
        [SerializeField, Range(0f, 0.5f)]
        private float _deacceleration;
        [Space, SerializeField]
        private string _prefabPath;

        public string DisplayName => _displayName;
        public string Description => _description;
        public byte Health => _health;
        public float Velocity => _velocity;
        public float RotationSpeed => _rotationSpeed;
        public float Deacceleration => _deacceleration;
        public string PrefabPath => _prefabPath;
    }
}