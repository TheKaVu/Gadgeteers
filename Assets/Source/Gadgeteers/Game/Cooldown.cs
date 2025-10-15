using System;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    public class Cooldown
    {
        private readonly Func<float> _cooldownSupplier;
        public float Start { get; set; } = float.MinValue;
        public float Value => _cooldownSupplier();
        public float Passed => Time.time - Start;
        public bool IsReady => Passed > Value;

        public Cooldown(Func<float> cooldownSupplier)
        {
            _cooldownSupplier = cooldownSupplier;
        }

        public void Begin()
        {
            Start = Time.time;
        }

        public void Cancel()
        {
            Start = float.MinValue;
        }
    }
}