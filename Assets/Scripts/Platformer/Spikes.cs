using UnityEngine;

namespace Platformer
{
    public class Spikes : MonoBehaviour
    {
        [SerializeField] private int damage = 0;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(damage);
            }
        }
    }
}