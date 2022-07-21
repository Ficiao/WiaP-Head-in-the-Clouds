using UnityEngine;

namespace BossBattler
{
    public class BossProjectile : Projectile
    {
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
