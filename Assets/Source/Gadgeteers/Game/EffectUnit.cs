using System;
using System.Collections;
using Source.Gadgeteers.Game.Entities;
using Source.Gadgeteers.UI;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    public abstract class EffectUnit : MonoBehaviour, ITooltipContext
    {
        public const string Path = "Effects/";
        
        private Entity _source;
        private Coroutine _tickTimer;
        private EffectUnitView _view;
        protected EffectScore _score;
        
        [SerializeField]
        private Sprite _icon;
        [SerializeField]
        private bool _visible;
        [SerializeField]
        private bool _visibleStack;
        [SerializeField]
        private float _frequency;
        [SerializeField]
        private string _name;
        [SerializeField]
        [TextArea]
        private string _description;
        
        public Entity Holder { get; private set; }
        public Entity Source => _source;
        public EffectScore Score => _score;
        public string Name => _name;
        public string Description => _description;
        
        
        public Sprite Icon { get => _icon; protected set => _icon = value; }
        public bool IsVisible { get =>  _visible; protected set => _visible = value; }
        
        public bool IsStackVisible { get => _visibleStack; protected set => _visibleStack = value; }
        
        public float Frequency { get => _frequency; protected set => _frequency = value; }
        
        public int Stack { get; set; }

        public abstract void OnStart();
        
        public abstract  void OnTick();

        public abstract  void OnEnd();
        
        
        private void OnDestroy()
        {
            if(_tickTimer != null) StopCoroutine(_tickTimer);
            if(_view != null) Destroy(_view.gameObject);
        }

        private void Awake()
        {
            if(_icon == null) _icon = Resources.Load<Sprite>("Textures/MissingTexture");
        }

        public void Init(Entity source, float duration)
        {
            _source = source;
            Holder = GetComponentInParent<Entity>();
            _score = new EffectScore
            {
                Duration = duration
            };
            
            name += $" [Source: {source.name}]";
            
            _view = Util.CreateFromPrefab<EffectUnitView>();
            _view.AssignTo(this);
            if(Holder.TryGetComponent<PlayerController>(out _)) _view.transform.SetParent(GameObject.Find("EffectUnitPanel").transform);
            _view.name = $"Display of {name}";

            _score.InitTime = Time.time;
            OnStart();
            UpdateDisplay();
            if (_tickTimer != null) StopCoroutine(_tickTimer);
            _tickTimer = StartCoroutine(TickTimer());
        }
        
        public void Reinit(float duration, EffectReinitMode mode)
        {
            switch (mode)
            {
                case EffectReinitMode.Restart:
                {
                    _score.Duration = duration;
                    _score.InitTime = Time.time;
                    OnStart();
                    break;
                }
                case EffectReinitMode.Prolong:
                {
                    _score.Duration += duration;
                    break;
                }
                case EffectReinitMode.Replace:
                default: break;
            }
            StopCoroutine(_tickTimer);
            UpdateDisplay();
            _tickTimer = StartCoroutine(TickTimer());
        }

        private IEnumerator TickTimer()
        {
            while (!_score.IsFinished)
            {
                yield return new WaitUntil(() => Frequency > 0 || _score.IsFinished);
                
                var period = Frequency > 0 ? 1 / Frequency : 0;
                period = Math.Min(period, Math.Abs(_score.Duration - _score.TimePassed));
                
                yield return new WaitForSeconds(period);
                OnTick();
                UpdateDisplay();
            }
            Holder.EffectCtrl.Clear(GetType(), Holder);
        }

        private void UpdateDisplay()
        {
            _view.Redraw(this);
        }

        public bool TryEnd()
        {
            OnEnd();
            return _score.IsFinished;
        }

        public string GetContent()
        {
            var text = $"<style=Name><style=Header>{Name}</style></style>" +
                       $"\n<style=Normal>{Description}</style>";
            return text;
        }
    }
}