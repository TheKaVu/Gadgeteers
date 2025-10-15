using System.Collections;
using UnityEngine;

namespace Source.Gadgeteers.Game
{
    public class GameTimer : MonoBehaviour
    {
        [SerializeField]
        private GameSchedule _schedule;

        private int _index;
        private float _startTime;
        private bool _skipCurrentState;
        
        public GameState CurrentState => _schedule.States[_index];

        public void Run()
        {
            StartCoroutine(Task());
        }

        public void Skip()
        {
            _skipCurrentState = true;
        }

        private IEnumerator Task()
        {
            for (; _index < _schedule.States.Count; _index++)
            {
                _startTime = Time.time;
                CurrentState.Enter();
                yield return new WaitUntil(() => !_skipCurrentState || Time.time - _startTime >= _schedule.States[_index].Length);
                CurrentState.Exit();
            }
        }
    }
}