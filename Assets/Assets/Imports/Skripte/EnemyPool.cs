using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyPool : MonoBehaviour
{
    private static EnemyPool _instance;
    public static EnemyPool Instance { get { return _instance; } }

    [SerializeField]
    private List<GameObject> enemyList;
    private Dictionary<string, Queue<GameObject>> enemyPoolDict;
    private Transform igrac;
    private Queue<GameObject> projectileQueue;
    [SerializeField]
    private GameObject projectile;
    private List<GameObject> active;

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

        enemyPoolDict = new Dictionary<string, Queue<GameObject>>();
        foreach(GameObject enemy in enemyList)
        {
            enemyPoolDict.Add(enemy.GetComponent<Neprijatelj>().enemyName, new Queue<GameObject>());
        }
        igrac = GameObject.FindGameObjectWithTag("Igrac").transform;
        projectileQueue = new Queue<GameObject>();
        active = new List<GameObject>();

    }

    private void Update()
    {
        if (Input.GetKeyDown("space") == false)
        {
            return;
        }


        Queue<GameObject> removing = new Queue<GameObject>();
        foreach (GameObject enemy in active)
        {
            removing.Enqueue(enemy);
        }
        while (removing.Count > 0)
        {
            DestroyEnemy(removing.Dequeue());
        }

        
    }

    public GameObject GetBullet()
    {
        if (projectileQueue.Count > 0)
        {
            GameObject projectile = projectileQueue.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }
        else
        {
            return Instantiate(projectile);
        }
    }

    public void DestroyBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        projectileQueue.Enqueue(bullet);
    }

    public void Spawn(string enemyName)
    {
        if (enemyPoolDict[enemyName].Count > 0)
        {
            GameObject enemy = enemyPoolDict[enemyName].Dequeue();
            enemy.SetActive(true);
            Vector2 priv;
            priv.x = Random.Range(-75, 75);
            priv.y = Random.Range(-33, 33);
            while (Vector2.Distance(igrac.position, priv) < 40f)
            {
                priv.x = Random.Range(-75, 75);
                priv.y = Random.Range(-33, 33);
            }

            enemy.transform.position = priv;
            active.Add(enemy);
        }
        else
        {
            Vector2 priv;
            priv.x = Random.Range(-75, 75);
            priv.y = Random.Range(-33, 33);
            while (Vector2.Distance(igrac.position, priv) < 40f)
            {
                priv.x = Random.Range(-75, 75);
                priv.y = Random.Range(-33, 33);
            }

            GameObject enemy = Instantiate(enemyList.Where(obj => obj.name == enemyName).SingleOrDefault(), priv, Quaternion.identity);
            active.Add(enemy);
        }
        
    }

    internal void DestroyEnemy(GameObject enemy)
    {
        UIManagerzzzzzz.Instance.UnitKilled();
        enemy.SetActive(false);
        enemyPoolDict[enemy.GetComponent<Neprijatelj>().enemyName].Enqueue(enemy);
        active.Remove(enemy);
    }
}
