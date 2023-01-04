using UnityEngine;

namespace Core.Models.Units
{
    [CreateAssetMenu(menuName = "Configuration/Units/Enemy Config")]
    public class EnemyConfig : UnitConfig
    {
        [field: Header("Reward")]
        [field: SerializeField] public byte Score { get; private set; }
    }
}