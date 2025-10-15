using System.Collections.Generic;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    [CreateAssetMenu(fileName = "New Game Schedule", menuName = "Game Schedule")]
    public class GameSchedule : ScriptableObject
    {
        [SerializeField]
        private List<GameState> _states = new();
        
        public List<GameState> States => new(_states);
        
        public GameState this[string stateName] => _states.Find(s => s.Name == stateName);
    }
}