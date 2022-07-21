using UnityEngine;

namespace Platformer
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform _player = null;
        [SerializeField] private Vector3 _offset;
        [SerializeField] private float _smoothingFactor = 0;

        private Vector3 _targetPosition;

        private void Update()
        {
            _targetPosition = _player.position + _offset;
            if (_targetPosition.y < 0) _targetPosition.y = 0;
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _smoothingFactor * Time.deltaTime);
        }
    }
}
