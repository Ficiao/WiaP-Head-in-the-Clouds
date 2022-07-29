using UnityEngine;

public class PlayerProjectile : Projectile
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == ParentCollider) return;
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<IEnemy>().TakeDamage(Damage);
            DestroyProjectile();
            return;
        }
        if (collision.CompareTag("Wall") || collision.CompareTag("Ground") || collision.CompareTag("Roof"))
        {
            DestroyProjectile();
            return;
        }
    } 
}

