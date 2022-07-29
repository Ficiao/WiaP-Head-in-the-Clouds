using UnityEngine;

namespace Platformer
{
    public class HighlightWireActivator : MonoBehaviour
    {
        [SerializeField] private ChandelierController _chandelier = null;
        [SerializeField] private GameObject _destructableWire = null;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Player") && _destructableWire.activeSelf == false)
            {
                _chandelier.WireActivated();
            }
        }
    }
}
