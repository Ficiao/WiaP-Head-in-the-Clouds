using UnityEngine;

namespace Platformer
{
    public class BossLargeProjectile : Projectile
    {
        [SerializeField] private int _numberOfSmallBullets = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == ParentCollider) return;
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(Damage);
                DestroyProjectile();
                return;
            }
            if (collision.CompareTag("Wall") || collision.CompareTag("Ground") || collision.CompareTag("Roof"))
            {
                Explosion();
                DestroyProjectile();
                return;
            }
        }

        private void Explosion()
        {
            BatBossController batBoss = ParentCollider.GetComponent<BatBossController>();
            for (int i = 0; i < _numberOfSmallBullets; i++)
            {
                Projectile projectile = batBoss.GetSmallBullet();
                projectile.ParentController = ParentController;
                projectile.ParentCollider = ParentCollider;
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.transform.Rotate(0, 0, (360.0f / _numberOfSmallBullets) * i);
            }
        }
    }
}