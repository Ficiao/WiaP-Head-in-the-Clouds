using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class HealingStation : MonoBehaviour
    {
        [SerializeField] private int _heal = 5;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().Heal(_heal);
            }
        }
    }
}
