using UnityEngine;

namespace Source.Gadgeteers.Game
{
    public class PlayerCameraController : MonoBehaviour
    {
        [SerializeField]
        private Transform _player;
        [SerializeField]
        private Vector3 _offset;

        private void Update()
        {
            transform.position = _player.position + _offset;
            transform.LookAt(_player.position);
        }
    }
}