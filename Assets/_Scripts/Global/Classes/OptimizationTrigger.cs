using UnityEngine;

namespace Platformer {
    public class OptimizationTrigger : MonoBehaviour
    {
        [SerializeField] private OptimizationSector _sector = null;
        [SerializeField] private bool _isTriggerOn = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (_isTriggerOn) 
                {
                    _sector.TriggerOn();
                }
                else
                {
                    _sector.TriggerOff();
                }
            }
        }
    }
}
