using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTrigger : MonoBehaviour
{
    [SerializeField] private bool _isUpperTrigger = false;
    [SerializeField] private Collider2D _platform;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _platform.enabled = _isUpperTrigger;
        }
    }

}
