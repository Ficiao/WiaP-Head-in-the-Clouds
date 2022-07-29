using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayTrigger : MonoBehaviour
{
    [SerializeField] private bool _isUpperTrigger = false;
    [SerializeField] private OneWayPlatform _platform = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _platform.SetColliderActive(_isUpperTrigger);
        }
    }

}
