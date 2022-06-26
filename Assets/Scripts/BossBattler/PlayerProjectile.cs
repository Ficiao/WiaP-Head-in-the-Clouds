using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossBattler {
    public class PlayerProjectile : Projectile
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == ParentCollider) return;
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<BossController>().TakeDamage(Damage);
            }

            UnistavanjeProjektila();
        } 
    }
}
