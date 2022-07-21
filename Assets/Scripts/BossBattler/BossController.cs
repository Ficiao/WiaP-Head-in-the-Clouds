using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossBattler
{
    public class BossController : ShootingController, IEnemy
    {
        [SerializeField] float shootingRate = 1f;
        [SerializeField] int numberOfBullets = 10;
        [SerializeField] private GameObject bossProjectile;
        private Queue<Projectile> bossProjectileQueue = new Queue<Projectile>();
        private List<Projectile> activeBossProjectileList = new List<Projectile>();
        private Coroutine shooting;
        private int sinusCounter;
        [SerializeField] private int _maxHealth = 10;
        private int _currentHealth = 10;
        public GameObject Key;        

        private void Awake()
        {
            sinusCounter = -1;
        }

        private void OnEnable()
        {
            shooting = StartCoroutine(Shooting());
            _currentHealth = _maxHealth;

            foreach(Projectile projectile in activeBossProjectileList)
            {
                projectile.transform.position = transform.position;
            }
        }

        private void OnDisable()
        {
            StopCoroutine(shooting);
        }

        private IEnumerator Shooting()
        {
            while (true)
            {
                yield return new WaitForSeconds(shootingRate);
                sinusCounter = (sinusCounter + 1) % 10;
                for(int i = 0; i < numberOfBullets; i++)
                {
                    Projectile projectile = GetBullet();
                    projectile.ParentController = this;
                    projectile.ParentCollider = gameObject;
                    projectile.transform.position = transform.position;
                    projectile.transform.rotation = transform.rotation;
                    projectile.transform.Rotate(0, 0, (360.0f / numberOfBullets) * i);
                    if (sinusCounter >= 5)
                    {
                        projectile.transform.Rotate(0, 0, ((360.0f / numberOfBullets) / 3) * sinusCounter);
                    }
                    else
                    {
                        projectile.transform.Rotate(0, 0, -((360.0f / numberOfBullets) / 3) * sinusCounter);
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

        private void Die()
        {
            Key.SetActive(true);
            Key.transform.position = transform.position;
            ObjectPool.Instance.DestroyBoss();
        }

        public Projectile GetBullet()
        {
            if (bossProjectileQueue.Count > 0)
            {
                Projectile projectile = bossProjectileQueue.Dequeue();
                activeBossProjectileList.Add(projectile);
                projectile.gameObject.SetActive(true);
                return projectile;
            }
            else
            {
                Projectile projectile = Instantiate(bossProjectile).GetComponent<Projectile>();
                activeBossProjectileList.Add(projectile);
                return projectile;
            }
        }

        public override void DestroyBullet(Projectile bullet)
        {
            activeBossProjectileList.Remove(bullet);
            bullet.gameObject.SetActive(false);
            bossProjectileQueue.Enqueue(bullet);
        }
    }
}