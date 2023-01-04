using System;
using UnityEngine;

namespace Core.Models
{
    public abstract class WeaponConfig : ScriptableObject, IEquatable<WeaponConfig>
    {
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }

        public bool Equals(WeaponConfig other)
        {
            if (other == null) return false;

            return DisplayName.Equals(other.DisplayName);
        }
    }
}
