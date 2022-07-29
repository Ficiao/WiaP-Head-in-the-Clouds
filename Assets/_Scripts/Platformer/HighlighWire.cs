using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer { 
    public class HighlighWire : MonoBehaviour
    {
    [SerializeField] ChandelierController _chandelier = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Projectile"))
            {
                _chandelier.WireSnapped();
            }
        } 
    }
}
