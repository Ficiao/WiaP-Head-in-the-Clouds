using UnityEngine;

namespace Platformer
{
    public class BatMob : MonoBehaviour, IEnemy
    {
        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _maxHealth = 2;
        private Transform _player = null;
        private BatSpawner _spawner = null;
        private int _health = 0;

        public Transform Player { get => _player; set => _player = value; }
        public BatSpawner Spawner { get => _spawner; set => _spawner = value; }
        public int Health { get => _health; set => _health = value; }
        public int MaxHealth { get => _maxHealth; }

        private void Start()
        {
            _health = _maxHealth;
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.position, _moveSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(_damage);
                Die();
            }
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if (_health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            _spawner.KillBat(this);
            ObjectPool.Instance.GetDeathEffect().transform.position = transform.position;
        }
    }
}
