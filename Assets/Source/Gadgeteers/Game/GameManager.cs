using System.Collections.Generic;
using Source.Gadgeteers.Game.Entities;

namespace Source.Gadgeteers.Game
{
    public class GameManager : SingletonBehaviour<GameManager>
    {
        public HashSet<Entity> Entities { get; } = new();
        
        private void Start()
        {
            
        }
    }
}