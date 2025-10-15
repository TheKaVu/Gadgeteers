using Source.Gadgeteers.Game.Entities;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [RequireComponent(typeof(Hitbox))]
    public abstract class Projectile : MonoBehaviour
    {
        public const string Path = "Projectiles/";
        
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _maxDistance;
        
        public float Speed => _speed;
        public Hitbox Hitbox { get; private set; }
        public float DistanceTraveled { get; private set; }
        public float MaxDistance => _maxDistance;
        public Entity Sender { get; set; }

        private void Awake()
        {
            Hitbox = GetComponent<Hitbox>();
            Hitbox.OnHit += OnHit;
            Hitbox.Filter = e => e != Sender;
        }

        public void Send(Entity sender)
        {
            Sender = sender;
        }

        private void Update()
        {
            var d = _speed * Time.deltaTime;
            transform.Translate(transform.forward * d, Space.World);
            
            DistanceTraveled += d;
            if (DistanceTraveled > _maxDistance)
            {
                Break();
            }
        }

        protected abstract void OnHit(Entity target);

        protected virtual void Break()
        {
            Destroy(gameObject);
        }
    }
}