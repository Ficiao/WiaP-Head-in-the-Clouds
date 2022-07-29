using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer {
    public class BatBossController : ShootingController, IEnemy
    {
        private enum AttackPattern
        {
            Shooting,
            PreparingToDash,
            Dashing,
            Raising
        }

        [SerializeField] private List<SpriteRenderer> _bossSprites = null;
        [SerializeField] private bool _goLeft = false;
        [SerializeField] private float _timeBetweenShoots = 0f;
        [SerializeField] private float _sinusSpeed = 0f;
        [SerializeField] private float _sinusAmplitude = 0f;
        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _maxHealth = 2;
        [SerializeField] private Slider _healthBar = null;
        [SerializeField] private Transform _player = null;
        [SerializeField] private GameObject _largeBossProjectile;
        [SerializeField] private GameObject _smallBossProjectile;
        [SerializeField] private float _timeBetweenDashingSequence = 0f;
        [SerializeField] private float _raisingSpeed = 0f;
        [SerializeField] private float _dashingSpeed = 0f;
        [SerializeField] private Animator _batAnimator;
        [SerializeField] private float _alertTime = 0f;
        [SerializeField] private int _numberOfDashes = 0;
        private Queue<Projectile> _largeBossProjectileQueue = new Queue<Projectile>();
        private Queue<Projectile> _smallBossProjectileQueue = new Queue<Projectile>();
        private Coroutine _shooting;
        private Coroutine _dashing;
        private int _health = 0;
        private AttackPattern _currentAttackPattern = AttackPattern.Shooting;
        private Vector3 _moveVector;
        private Vector3 _dashVector;
        private Vector3 _projectileDirection;
        private float _heightAfterdash;
        private int _currentDashNumber = 0;

        public int Health { get => _health; set => _health = value; }
        public int MaxHealth { get => _maxHealth; }

        private void OnEnable()
        {
            _healthBar.gameObject.SetActive(true);
        }

        private void Start()
        {
            _health = _maxHealth;
            _moveVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _dashVector = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            _shooting = StartCoroutine(Shooting());
            _dashing = StartCoroutine(PreparingToDash());
            _heightAfterdash = transform.position.y;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Wall") || collision.CompareTag("MobWall") || collision.CompareTag("Ground"))
            {
                if (_currentAttackPattern == AttackPattern.Shooting)
                {
                    _goLeft = !_goLeft;
                    return;
                }
                else if(_currentAttackPattern == AttackPattern.Dashing)
                {
                    _currentAttackPattern = AttackPattern.Raising;
                    _batAnimator.SetBool("Dashing", false);
                }
            }
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(_damage);
            }
        }

        private void FixedUpdate()
        {
            if(_currentAttackPattern == AttackPattern.Shooting)
            {
                _moveVector.x = transform.position.x + (_moveSpeed * (_goLeft ? -1 : 1) * Time.fixedDeltaTime);
                _moveVector.y = transform.position.y + Mathf.Sin(_sinusSpeed * Time.time) * _sinusAmplitude;
                transform.position = Vector3.MoveTowards(transform.position, _moveVector, Vector3.Distance(transform.position, _moveVector) + 1);
            }
            else if(_currentAttackPattern == AttackPattern.Dashing)
            {
                transform.Translate(_dashVector * _dashingSpeed * Time.fixedDeltaTime);
            }
            else if(_currentAttackPattern == AttackPattern.Raising)
            {
                _moveVector.x = transform.position.x;
                _moveVector.y = _heightAfterdash;
                transform.position = Vector3.MoveTowards(transform.position, _moveVector, _raisingSpeed * Time.fixedDeltaTime);
                if(transform.position.y == _heightAfterdash)
                {
                    _currentDashNumber++;
                    if(_currentDashNumber == _numberOfDashes)
                    {
                        _currentDashNumber = 0;
                        _currentAttackPattern = AttackPattern.Shooting;
                        _shooting = StartCoroutine(Shooting());
                        _dashing = StartCoroutine(PreparingToDash());
                    }
                    else
                    {
                        _dashing = StartCoroutine(Dashing());
                        _currentAttackPattern = AttackPattern.PreparingToDash;
                    }
                }
            }
        }

        private IEnumerator Dashing()
        {
            foreach (SpriteRenderer sprite in _bossSprites)
            {
                sprite.color = Color.red;
            }
            yield return new WaitForSeconds(_alertTime);
            foreach (SpriteRenderer sprite in _bossSprites)
            {
                sprite.color = Color.white;
            }
            _currentAttackPattern = AttackPattern.Dashing;
            _batAnimator.SetBool("Dashing", true);
            _dashVector = (_player.position - transform.position).normalized;
        }

        private IEnumerator PreparingToDash()
        {
            yield return new WaitForSeconds(_timeBetweenDashingSequence);
            _currentAttackPattern = AttackPattern.PreparingToDash;
            _dashing = StartCoroutine(Dashing());
            StopCoroutine(_shooting);
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(_timeBetweenShoots);
                Projectile projectile = GetLargeBullet();
                projectile.ParentController = this;
                projectile.ParentCollider = gameObject;
                projectile.transform.position = transform.position;
                _projectileDirection = (_player.position - projectile.transform.position).normalized;                
                float angle = Mathf.Atan2(_projectileDirection.y, _projectileDirection.x) * Mathf.Rad2Deg;
                if (angle < 0) angle += 360;
                projectile.transform.eulerAngles = new Vector3(0, 0, angle - 90f);
            }
        }

        private Projectile GetLargeBullet()
        {
            if (_largeBossProjectileQueue.Count > 0)
            {
                Projectile projectile = _largeBossProjectileQueue.Dequeue();
                projectile.gameObject.SetActive(true);
                return projectile;
            }
            else
            {
                Projectile projectile = Instantiate(_largeBossProjectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                return projectile;
            }
        }

        public Projectile GetSmallBullet()
        {
            if (_largeBossProjectileQueue.Count > 0)
            {
                Projectile projectile = _smallBossProjectileQueue.Dequeue();
                projectile.gameObject.SetActive(true);
                return projectile;
            }
            else
            {
                Projectile projectile = Instantiate(_smallBossProjectile, transform.position, Quaternion.identity).GetComponent<Projectile>();
                return projectile;
            }
        }

        public override void DestroyBullet(Projectile bullet)
        {
            bullet.gameObject.SetActive(false);
            if (bullet is BossLargeProjectile) _largeBossProjectileQueue.Enqueue(bullet);
            else if (bullet is BossSmallProjectile) _smallBossProjectileQueue.Enqueue(bullet);
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            _healthBar.value = _health / (float)_maxHealth;
            if (_health <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            _healthBar.gameObject.SetActive(false);
            GameObject deathEffect = ObjectPool.Instance.GetDeathEffect();
            deathEffect.transform.position = transform.position + new Vector3(0, 1.5f, 0);
            deathEffect.transform.GetChild(0).localScale = new Vector3(0.25f, 0.25f, 1);
            gameObject.SetActive(false);
            BossEncounterManager.Instance.BossKilled();
            StopCoroutine(_shooting);
            StopCoroutine(_dashing);
        }
    }
}