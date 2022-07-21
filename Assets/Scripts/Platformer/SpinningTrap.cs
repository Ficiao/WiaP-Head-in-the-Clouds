using UnityEngine;

namespace Platformer
{
    public class SpinningTrap : MonoBehaviour
    {
        [SerializeField] private float _rotationSpeed = 0;
        [SerializeField] private float _moveSpeed = 0;
        [SerializeField] private int damage = 0;
        [SerializeField] private bool _goLeft = false;

        private Vector3 _rotateVector;
        private Vector3 _moveVector;

        private void Start()
        {
            _rotateVector = new Vector3(0, 0, _rotationSpeed);
            _moveVector = new Vector3(_moveSpeed, transform.position.y, transform.position.z);
        }

        private void Update()
        {
            transform.Rotate(_rotateVector * Time.deltaTime);
            _moveVector = new Vector3(transform.position.x + _moveSpeed * (_goLeft ? -1 : 1), transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(transform.position, _moveVector, Vector3.Distance(transform.position, _moveVector) + 1);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(damage);
            }
            else if (collision.CompareTag("Wall"))
            {
                _goLeft = !_goLeft;
            }
        }


    }
}