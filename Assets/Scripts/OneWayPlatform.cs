using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private List<GameObject> _sideTriggers;

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach(GameObject trigger in _sideTriggers)
            {
                trigger.SetActive(false);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            foreach (GameObject trigger in _sideTriggers)
            {
                trigger.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var kek = 5;
    }

    private void Update()
    {
        if (Input.GetKey("s"))
        {
            _collider.isTrigger = true;
        }
        else
        {
            _collider.isTrigger = false;
        }
    }
}
