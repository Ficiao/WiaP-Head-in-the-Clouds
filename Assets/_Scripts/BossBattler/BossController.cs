using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossBattler
{
    public class BossController : ShootingController, IEnemy
    {
        [SerializeField] float _shootingRate = 1f;
        [SerializeField] int _numberOfBullets = 10;
        [SerializeField] private GameObject _bossProjectile;
        [SerializeField] private int _maxHealth = 10;
        private Queue<Projectile> _bossProjectileQueue = new Queue<Projectile>();
        private List<Projectile> _activeBossProjectileList = new List<Projectile>();
        private Coroutine _shooting;
        private int _sinusCounter;
        private int _currentHealth = 10;
        public GameObject Key;        

        private void Awake()
        {
            _sinusCounter = -1;
        }

        private void OnEnable()
        {
            _shooting = StartCoroutine(Shooting());
            _currentHealth = _maxHealth;

            foreach(Projectile projectile in _activeBossProjectileList)
            {
                projectile.transform.position = transform.position;
            }
        }

        private void OnDisable()
        {
            StopCoroutine(_shooting);
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(_shootingRate);
                _sinusCounter = (_sinusCounter + 1) % 10;
                for(int i = 0; i < _numberOfBullets; i++)
                {
                    Projectile projectile = GetBullet();
                    projectile.ParentController = this;
                    projectile.ParentCollider = gameObject;
                    projectile.transform.position = transform.position;
                    projectile.transform.rotation = transform.rotation;
                    projectile.transform.Rotate(0, 0, (360.0f / _numberOfBullets) * i);
                    if (_sinusCounter >= 5)
                    {
                        projectile.transform.Rotate(0, 0, ((360.0f / _numberOfBullets) / 3) * _sinusCounter);
                    }
                    else
                    {
                        projectile.transform.Rotate(0, 0, -((360.0f / _numberOfBullets) / 3) * _sinusCounter);
                    }
                }
            }
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        public void Die()
        {
            Key.SetActive(true);
            Key.transform.position = transform.position;
            ObjectPool.Instance.DestroyBoss();
        }

        public Projectile GetBullet()
        {
            if (_bossProjectileQueue.Count > 0)
            {
                Projectile projectile = _bossProjectileQueue.Dequeue();
                _activeBossProjectileList.Add(projectile);
                projectile.gameObject.SetActive(true);
                return projectile;
            }
            else
            {
                Projectile projectile = Instantiate(_bossProjectile).GetComponent<Projectile>();
                _activeBossProjectileList.Add(projectile);
                return projectile;
            }
        }

        public override void DestroyBullet(Projectile bullet)
        {
            _activeBossProjectileList.Remove(bullet);
            bullet.gameObject.SetActive(false);
            _bossProjectileQueue.Enqueue(bullet);
        }
    }
}