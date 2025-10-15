using System;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public struct EffectScore
    {
        public static readonly EffectScore Empty = new(){};
        
        private float _duration;
        private float _initTime;
        
        public float Duration
        {
            get => _duration;
            set => _duration = value;
        }

        public float TimePassed => Time.time - _initTime;

        public float InitTime
        {
            get => _initTime;
            set => _initTime = value;
        }

        public float Progress => Math.Min(TimePassed / _duration, 1f);

        public bool IsFinished => TimePassed >= Duration && Duration >= 0;
    }
}