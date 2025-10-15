using System;
using System.Collections.Generic;
using System.Linq;
using Source.Gadgeteers.Game.Entities;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [Serializable]
    public class Hitbox : MonoBehaviour
    {
        private readonly HashSet<Entity> _targets = new();
        
        [SerializeField]
        private Collider _collider;
        [SerializeField]
        private SpriteRenderer _marker;
        
        public SpriteRenderer Marker => _marker;
        public Predicate<Entity> Filter { get; set; }
        
        public event Action<Entity> OnHit;

        public List<Entity> Targets {
            get
            {
                var list = _targets.ToList();
                list.Sort((e1, e2) =>
                {
                    var d1 = (transform.position - e1.transform.position).magnitude;
                    var d2 = (transform.position - e2.transform.position).magnitude;
                    return d1.CompareTo(d2);
                });
                return list;
            }
        }

        public void SetMarkerVisible(bool visible)
        {
            Marker.gameObject.SetActive(visible);
        }

        private void Update()
        {
            if (_marker == null) return;
            _marker.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.z, transform.localScale.y);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.TryGetComponent(out Entity target)) return;
            if (Filter != null && !Filter.Invoke(target)) return;
            
            OnHit?.Invoke(target);
            _targets.Add(target);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.TryGetComponent(out Entity target))
            {
                _targets.Remove(target);
            }
        }
    }
}