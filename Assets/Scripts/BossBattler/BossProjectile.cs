using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossBattler
{
    public class BossProjectile : Projectile
    {
        private void Awake()
        {
            int kek = 5;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == ParentCollider) return;
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(Damage);
            }

            UnistavanjeProjektila();
        }
    }
}
