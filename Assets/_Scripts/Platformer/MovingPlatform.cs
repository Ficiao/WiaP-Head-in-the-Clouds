using UnityEngine;

namespace Platformer {
    public class MovingPlatform : MonoBehaviour
    {
        [SerializeField] private float _lowerPositionY = 0f;
        [SerializeField] private float _higherPositionY = 0f;
        [SerializeField] private bool _goingUp = false;
        [SerializeField] private float _moveSpeed = 0f;
        private Vector3 _starPosition;
        private Vector3 _endPosition;
        private Transform _player = null;

        private void Start()
        {
            if (_goingUp)
            {
                _starPosition = new Vector3(transform.position.x, _lowerPositionY, transform.position.z);
                _endPosition = new Vector3(transform.position.x, _higherPositionY, transform.position.z);
            }
            else
            {
                _starPosition = new Vector3(transform.position.x, _higherPositionY, transform.position.z);
                _endPosition = new Vector3(transform.position.x, _lowerPositionY, transform.position.z);
            }
        }

        private void FixedUpdate()
        {
            if (_player != null && !_goingUp)
            {
                _starPosition = transform.position;
                transform.position = Vector3.MoveTowards(transform.position, _endPosition, _moveSpeed);
                _player.Translate(0, -Vector3.Distance(_starPosition, transform.position) * 1.05f, 0);
            }
            
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, _endPosition, _moveSpeed);
            }

            if (transform.position.y == _higherPositionY && _goingUp)
            {
                transform.Translate(0, -_higherPositionY + _lowerPositionY, 0);
            }
            else if (transform.position.y == _lowerPositionY && !_goingUp)
            {
                transform.Translate(0, _higherPositionY - _lowerPositionY, 0);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(!_goingUp && collision.transform.CompareTag("Player"))
            {
                _player = collision.transform;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (!_goingUp && collision.transform.CompareTag("Player"))
            {
                _player = null;
            }
        }
    } 
}
