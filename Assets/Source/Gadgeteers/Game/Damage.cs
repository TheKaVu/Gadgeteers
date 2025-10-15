using Source.Gadgeteers.Game.Events;

namespace Source.Gadgeteers.Game
{
    public struct Damage : ICancellable
    {
        private float _value;
        
        public float Value { get => _value; set => _value = value >= 0 && !IsCancelled ? value : 0; }
        public DamageType Type { get; }
        public bool IsCancelled { get; private set; }
        

        public Damage(float value, DamageType damageType)
        {
            _value = 0;
            Type = damageType;
            IsCancelled = false;
            Value = value;
        }

        public void Cancel()
        {
            IsCancelled = true;
        }
    }
}