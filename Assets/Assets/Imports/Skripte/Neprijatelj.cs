using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neprijatelj : MonoBehaviour {

    public int zivoti;
    public float brzina;
    [HideInInspector] public Transform igrac;
    public float vrijemeIzmeduNapada;
    public int steta;
    public GameObject efektSmrt;
    public string enemyName;

    public virtual void Start() {
        igrac = GameObject.FindGameObjectWithTag("Igrac").transform;
    }

    public void PrimiStetu(int steta) {
        zivoti -= steta;
        if (zivoti <= 0) {
            Instantiate(efektSmrt, transform.position, transform.rotation);
            //Spawner.Instance.DestroyEnemy(this.gameObject);
            Destroy(gameObject);

        }
    }
}
