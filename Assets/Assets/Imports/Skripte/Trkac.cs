using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trkac : Neprijatelj {

    public float stopUdaljenost;
    private float vrijemeNapada;
    public float brzinaNapada;

    public override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (igrac != null)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                                 igrac.position, brzina * Time.deltaTime);
        }
    }

    /*    IEnumerator Napad() 
        {
            igrac.GetComponent<Igrac>().PrimiStetu(steta);
            Vector2 izvornaPozicija = transform.position;
            Vector2 pozicijaIgraca = igrac.position;
            float postotak = 0;
            while (postotak <= 1) {
                postotak += brzinaNapada * Time.deltaTime;
                float formula = (-Mathf.Pow(postotak, 2) + postotak) * 4;
                Debug.Log(formula);
                transform.position = Vector2.Lerp(izvornaPozicija, pozicijaIgraca, formula);
                yield return null;
            }
        }*/
}
