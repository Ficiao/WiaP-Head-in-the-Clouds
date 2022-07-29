using UnityEngine;

namespace Platformer
{
    public class RunnerMob : MonoBehaviour, IEnemy, IAggroable
    {
        [SerializeField] private bool _goLeft = false;
        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private float _aggroRunModifier = 1f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _maxHealth = 2;
        private Transform _player = null;
        private int _health = 0;
        private AggroStateType _aggroState = AggroStateType.Idle;
        private Vector3 _moveVector;
        private Vector3 _runVector;

        public int Damage { get => _damage; }
        public int Health { get => _health; set => _health = value; }
        public int MaxHealth { get => _maxHealth; }

        private void Start()
        {
            _moveVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _runVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _health = _maxHealth;
        }

        private void Update()
        {
            switch (_aggroState)
            {
                case AggroStateType.Idle:
                    IdleWalk();
                    break;
                case AggroStateType.Aggored:
                    AggroRun();
                    break;
                default:
                    break;
            }
        }

        private void IdleWalk()
        {
            _moveVector.x = transform.position.x + (_moveSpeed * (_goLeft ? -1 : 1) * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, _moveVector, Vector3.Distance(transform.position, _moveVector) + 1);
        }

        private void AggroRun()
        {
            _runVector.x = _player.position.x;
            _moveVector.x = transform.position.x + (_moveSpeed * (_goLeft ? -1 : 1) * Time.deltaTime * _aggroRunModifier);
            transform.position = Vector3.MoveTowards(transform.position, _runVector, Vector3.Distance(transform.position, _moveVector));
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("MobWall"))
            {
                _goLeft = !_goLeft;
                return;
            }
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
            ObjectPool.Instance.GetDeathEffect().transform.position = transform.position;
            Destroy(gameObject);
        }

        public void Aggro(Transform target)
        {
            _aggroState = AggroStateType.Aggored;
            _player = target;
        }

        public void Disaggro()
        {
            _aggroState = AggroStateType.Idle;
            _player = null;
        }
    }
}
