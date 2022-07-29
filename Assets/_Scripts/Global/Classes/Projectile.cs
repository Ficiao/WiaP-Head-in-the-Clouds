using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    [SerializeField] private float _speed = 0.15f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _lifeDuration = 5f;
    private ShootingController _parentController;
    private GameObject _parentCollider;
    private IEnumerator _lifeTime;

    public ShootingController ParentController { get => _parentController; set => _parentController = value; }
    public GameObject ParentCollider { get => _parentCollider; set => _parentCollider = value; }
    public int Damage { get => _damage; }

    private void OnEnable()
    {
        _lifeTime = LifeTime();
        StartCoroutine(_lifeTime);
    }

    private void OnDisable()
    {
        StopCoroutine(_lifeTime);
    }

    void FixedUpdate() {
        transform.Translate(Vector2.up * _speed * Time.fixedDeltaTime);
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(_lifeDuration);
        DestroyProjectile();
    }

    public void DestroyProjectile() {
        StopCoroutine(_lifeTime);
        _parentController.DestroyBullet(this);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject != _parentCollider) DestroyProjectile();        
    }
}
