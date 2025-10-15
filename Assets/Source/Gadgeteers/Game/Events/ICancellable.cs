namespace Source.Gadgeteers.Game.Events
{
    public interface ICancellable
    {
        public bool IsCancelled { get; }
        
        public void Cancel();
    }
}