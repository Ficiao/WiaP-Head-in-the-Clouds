using System.Collections.Generic;
using UnityEngine;

public class OneWayPlatform : MonoBehaviour
{
    [SerializeField] private List<GameObject> _sideTriggers = null;
    private bool _shouldBeActive;

    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _shouldBeActive = true;
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

    public void SetColliderActive(bool active)
    {
        _shouldBeActive = active;
    }

    private void Update()
    {
        if (Input.GetKey("s"))
        {
            _collider.enabled = false;
        }
        else
        {
            _collider.enabled = _shouldBeActive;
        }
    }
}
