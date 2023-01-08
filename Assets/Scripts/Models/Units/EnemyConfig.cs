using UnityEngine;

namespace Core.Models.Units
{
    [CreateAssetMenu(menuName = "Configuration/Units/Enemy Config")]
    public class EnemyConfig : UnitConfig
    {
        [field: Header("AI")]
        [field: SerializeField, Min(0f)] public float AggressionRadius { get; private set; }
        [field: SerializeField, Min(0f)] public float PatrolRadius { get; private set; }
    }
}