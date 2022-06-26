using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sismis : Neprijatelj {

    public float stopUdaljenost;
    private float vrijemeNapada;
    private Animator anim;
    public Transform mjestoMetka;
    public GameObject metak;

    public override void Start() {
        base.Start();
        anim = GetComponent<Animator>();
    }

    void Update() {

        if (igrac != null)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                 igrac.position, brzina * Time.deltaTime);
        }

    }

    /*public void NapadSismisa() {
        if (igrac != null) {
            Vector2 smjer = igrac.position - mjestoMetka.position;
            float kut = Mathf.Atan2(smjer.y, smjer.x) * Mathf.Rad2Deg;
            Quaternion rotacija = Quaternion.AngleAxis(kut - 90, Vector3.forward);
            Instantiate(metak, mjestoMetka.position, rotacija);
        }
    }*/
}
