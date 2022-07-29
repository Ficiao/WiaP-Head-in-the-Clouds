using UnityEngine;

namespace Platformer
{
    public class CinematicCamera : MonoBehaviour
    {
        [SerializeField] private Transform _player = null;
        [SerializeField] private Vector3 _offset;

        private Vector3 _targetPosition;

        private void Update()
        {
            _targetPosition = _player.position + _offset;
            transform.position = _targetPosition;
        }
    }
}