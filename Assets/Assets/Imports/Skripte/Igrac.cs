using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igrac : MonoBehaviour {

    public int brzina;
    private Rigidbody2D rb;
    private Animator anim;
    private Vector2 micanjeBrzina;
    public int zivoti;
    [SerializeField]
    private float horizontal;
    [SerializeField]
    private float vertical;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update() 
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector2 micanjeInput = new Vector2(horizontal, vertical);
        micanjeBrzina = micanjeInput.normalized * brzina;
        if (micanjeInput != Vector2.zero) {
            anim.SetBool("aktivacijaTrcanje", true);
        } else {
            anim.SetBool("aktivacijaTrcanje", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Neprijatelj"))
        {
            PrimiStetu(5);
        }
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + micanjeBrzina * Time.fixedDeltaTime);
    }

    public void PrimiStetu(int steta) {
        zivoti -= steta;
        if (zivoti <= 0) {
            Destroy(gameObject);
        }
    }
}
