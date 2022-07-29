using System.Collections.Generic;
using UnityEngine;

public class Weapon : ShootingController
{
    [SerializeField] private GameObject playerProjectile; 
    [SerializeField] private Transform shootOrigin;
    [SerializeField] private float timeBetweenShoot = 0.15f;
    //[SerializeField] private GameObject shootParticles;
    private float timeOfLastShoot;
    private Queue<Projectile> playerProjectileQueue = new Queue<Projectile>();

    void Update()
    {
        Vector2 smjer = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float kut = Mathf.Atan2(smjer.y, smjer.x) * Mathf.Rad2Deg;
        Quaternion rotacija = Quaternion.AngleAxis(kut - 90, Vector3.forward);
        transform.rotation = rotacija;
        if (Input.GetMouseButton(0))
        {
            if (Time.time >= timeOfLastShoot)
            {
                timeOfLastShoot = Time.time + timeBetweenShoot;
                Projectile projectile = GetBullet();
                projectile.ParentController = this;
                projectile.ParentCollider = transform.parent.gameObject;
                projectile.transform.position = shootOrigin.position;
                projectile.transform.rotation = transform.rotation;
                //Instantiate(shootParticles, shootOrigin.position, transform.rotation);
            }
        }
    }

    private Projectile GetBullet()
    {
        if (playerProjectileQueue.Count > 0)
        {
            Projectile projectile = playerProjectileQueue.Dequeue();
            projectile.gameObject.SetActive(true);
            return projectile;
        }
        else
        {
            return Instantiate(playerProjectile).GetComponent<Projectile>();
        }
    }

    public override void DestroyBullet(Projectile bullet)
    {
        bullet.gameObject.SetActive(false);
        playerProjectileQueue.Enqueue(bullet);
    }
}

