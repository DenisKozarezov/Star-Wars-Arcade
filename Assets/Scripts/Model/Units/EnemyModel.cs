using UnityEngine;

namespace Core.Models.Units
{
    [CreateAssetMenu(menuName = "Configuration/Units/Enemy Model")]
    public class EnemyModel : UnitModel
    {
        [Header("Reward")]
        [SerializeField]
        private byte _score;

        public byte Score => _score;
    }
}