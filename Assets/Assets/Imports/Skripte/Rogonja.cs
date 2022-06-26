using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rogonja : Neprijatelj {

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    private Vector2 ciljanaPozicija;
    private Animator anim;
    public float vrijemeIzmeduStvaranja;
    private float vrijemeStvaranja;
    public GameObject neprijateljStvaranje;
    public float brzinaNapada;
    private float vrijemeNapada;
    public float stopUdaljenost;

    public override void Start() {
        base.Start();
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        ciljanaPozicija = new Vector2(randomX, randomY);
        anim = GetComponent<Animator>();
    }

    void Update() {
        if (igrac != null)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                 igrac.position, brzina * Time.deltaTime);
        }
    }

    /*public void KreiranjeNeprijatelja() {
        if(igrac != null) {
            Instantiate(neprijateljStvaranje, transform.position, transform.rotation);
        }
    }*/
}
