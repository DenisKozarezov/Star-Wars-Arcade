using System;
using UnityEngine;

namespace Core.Models.Units
{
    public abstract class UnitConfig : ScriptableObject, IEquatable<UnitConfig>
    {
        [field: Header("Settings")]
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: Space, SerializeField, Min(0)] public int Health { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float Velocity { get; private set; }
        [field: SerializeField, Range(0f, 10f)] public float RotationSpeed { get; private set; }
        [field: SerializeField, Range(0f, 0.5f)] public float Deacceleration { get; private set; }
        [field: SerializeField, Range(0f, 5f)] public float ReloadTime { get; private set; }
        [field: Space, SerializeField] public GameObject Prefab { get; private set; }

        public bool Equals(UnitConfig other)
        {
            if (other == null) return false;

            return DisplayName.Equals(other.DisplayName);
        }
    }
}