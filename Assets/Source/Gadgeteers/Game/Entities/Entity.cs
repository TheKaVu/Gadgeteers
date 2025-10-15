using UnityEngine;

namespace Source.Gadgeteers.Game.Entities
{
    [RequireComponent(typeof(StatController))]
    [RequireComponent(typeof(EffectController))]
    public abstract class Entity : MonoBehaviour
    {
        private Vector3 _oldPosition;
        
        public StatController StatCtrl { get; private set; }
        public EffectController EffectCtrl { get; private set; }
        public bool IsMoving { get; private set; }
        public Vector2 Direction { get; private set; }

        private void Awake()
        {
            StatCtrl = GetComponent<StatController>();
            EffectCtrl = GetComponent<EffectController>();
            OnAwake();
        }

        private void OnEnable()
        {
            GameManager.Singleton.Entities.Add(this);
        }

        private void OnDisable()
        {
            GameManager.Singleton.Entities.Remove(this);
        }

        private void Update()
        {
            IsMoving = _oldPosition != transform.position;
            _oldPosition = transform.position;
            OnUpdate();
        }

        public void Move(Vector2 direction)
        {
            var translation = new Vector3(direction.x, 0, direction.y) * (StatCtrl["move_speed"] * 0.4f);
            transform.position += translation;
            Direction = direction;
        }
        
        protected virtual void OnAwake() { }
        
        protected virtual void OnUpdate() { }
    }
}