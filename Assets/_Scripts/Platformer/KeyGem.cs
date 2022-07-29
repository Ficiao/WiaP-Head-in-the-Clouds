using UnityEngine;

namespace Platformer
{
    public class KeyGem : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                GameManager.Instance.KeyGemPickedUp();
                Destroy(gameObject);
            }
        }
    } 
}
