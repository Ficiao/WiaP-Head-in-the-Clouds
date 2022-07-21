using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {

    [SerializeField] private float _speed = 0.15f;
    [SerializeField] private int _damage = 1;
    private ShootingController _parentController;
    public ShootingController ParentController { get => _parentController; set => _parentController = value; }
    private GameObject _parentCollider;
    public GameObject ParentCollider { get => _parentCollider; set => _parentCollider = value; }
    public int Damage { get => _damage; }

    void Update() {
        transform.Translate(Vector2.up * _speed * Time.deltaTime);
    }

    public void UnistavanjeProjektila() {
        _parentController.DestroyBullet(this);
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject != _parentCollider) UnistavanjeProjektila();        
    }
}
