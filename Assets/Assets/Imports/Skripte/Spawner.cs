using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private IEnumerator spawning;
    private static Spawner _instance;
    public static Spawner Instance { get { return _instance; } }
    [SerializeField]
    private float spawnRate;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

        private void Start()
    {
        spawning = Spawning();
        StartCoroutine(Spawning());
    }
       

    IEnumerator Spawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            EnemyPool.Instance.Spawn("Sismis");
            EnemyPool.Instance.Spawn("Trkac");
            EnemyPool.Instance.Spawn("Rogonja");

        }

    }

    public void DestroyEnemy(GameObject gameObject)
    {
        EnemyPool.Instance.DestroyEnemy(gameObject);
    }


}
