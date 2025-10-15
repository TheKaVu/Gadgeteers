using System;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public struct GameState
    {
        [SerializeField]
        private string _name;
        [SerializeField]
        private float _length;
        [SerializeField]
        private UnityEvent _onEnter;
        [SerializeField]
        private UnityEvent _onExit;
        [SerializeField]
        private UnityEvent<Ref<bool>> _condition;
        
        public string Name => _name;
        public float Length => _length;

        public bool IsRunning { get; private set; }

        public void Enter()
        {
            if(!IsRunning)
                _onEnter.Invoke();
            IsRunning = true;
        }

        public void Exit()
        {
            if(IsRunning)
                _onExit.Invoke();
            IsRunning = false;
        }

        public bool Condition()
        {
            Ref<bool> flag = true;
            _condition.Invoke(flag);
            return flag;
        }
    }
}