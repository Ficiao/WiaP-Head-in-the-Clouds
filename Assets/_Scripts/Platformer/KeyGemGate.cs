using System.Collections.Generic;
using UnityEngine;

namespace Platformer {
    public class KeyGemGate : MonoBehaviour
    {
        [SerializeField] private List<SpriteRenderer> _emptyKeyGems = null;
        private int _keyGemToFill = 0;

        public void KeyGemPickedUp()
        {
            _emptyKeyGems[_keyGemToFill].color = Color.white;

            _keyGemToFill++;
            if (_keyGemToFill == _emptyKeyGems.Count)
            {
                transform.Translate(0f, 5f, 0f);
                ScreenShake.Instance.StartShaking(2.5f);
            }
        }
    } 
}
