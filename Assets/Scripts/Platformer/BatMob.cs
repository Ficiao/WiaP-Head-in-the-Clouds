using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer {
    public class BatMob : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 0.4f;
        [SerializeField] private int _damage = 1;
        [SerializeField] private int _maxHealth = 2;
        private Transform _player = null;
        private BatSpawner _spawner = null;
        private int _health = 0;

        public Transform Player { get; set; }
        public BatSpawner Spawner { get; set; }
        public int Health { get => _health; set => _health = value; }
        public int MaxHealth { get => _maxHealth; }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(transform.position, _player.position, _moveSpeed);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(_damage);
                _spawner.KillBat(this);
            }
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            if(_health <= 0)
            {
                _spawner.KillBat(this);
            }
        }
    } 
}
