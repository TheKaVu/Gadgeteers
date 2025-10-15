using System;
using UnityEngine;

namespace Assets.Scripts.Game.Statistics
{
    [Serializable]
    public class StatBinding
    {
        [SerializeField]
        private StatType _target;
        [SerializeField]
        private float _factor;
        [SerializeField]
        private StatController _controller;
        [SerializeField]
        private bool _isDynamic;

        public StatType Target => _target;
        public float Factor { 
            get => _factor;
            set
            {
                if (_isDynamic) _factor = value;
            }
        }
        public StatController Controller { get => _controller; set => _controller = value; }

        public bool IsDynamic => _isDynamic;

        public StatBinding(StatType target, float factor, bool isDynamic = false, StatController controller = null)
        {
            _target = target;
            _factor = factor;
            _isDynamic = isDynamic;
            _controller = controller;
        }

        public float Get() => Get(_controller);

        public float Get(StatController controller) => controller == null ? 0 : controller[_target] * _factor;
    }
}
