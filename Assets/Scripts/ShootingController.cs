using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class ShootingController : MonoBehaviour
{
    abstract public void DestroyBullet(Projectile bullet);
}
