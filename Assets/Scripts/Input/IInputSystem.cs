using System;
using UnityEngine;

namespace Core.Input
{
    public interface IInputSystem
    {
        ref Vector2 Direction { get; }
        ref Vector2 MousePosition { get; }
        bool Enabled { get; }
        Action Fire { get; set; }
        void Enable();
        void Disable();
    }
}