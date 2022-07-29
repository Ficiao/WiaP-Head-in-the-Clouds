using UnityEngine;

namespace BossBattler
{
    public class Key : MonoBehaviour
    {
        public GateColor GateColor;
        private bool _isPickedUp = false;

        private void OnDisable()
        {
            _isPickedUp = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_isPickedUp && collision.CompareTag("Player")) 
            {
                transform.parent = collision.transform;
                transform.position = collision.transform.position;
                collision.GetComponent<PlayerEquiped>().EquipedKey = this;
                _isPickedUp = true;
            }
        }


    }
}
