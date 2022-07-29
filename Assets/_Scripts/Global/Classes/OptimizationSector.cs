using System.Collections.Generic;
using UnityEngine;

namespace Platformer {
    public class OptimizationSector : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _responsabilities = null;
        private bool _triggeredState = false;

        private void Awake()
        {
            _triggeredState = true;
            TriggerOff();
        }

        public void TriggerOn()
        {
            if (_triggeredState == false)
            {
                foreach (GameObject responsability in _responsabilities)
                {
                    responsability?.SetActive(true);
                }

                _triggeredState = true;
            }
        }

        public void TriggerOff()
        {
            if(_triggeredState == true)
            {
                foreach(GameObject responsability in _responsabilities)
                {
                    responsability?.SetActive(false);
                }

                _triggeredState = false;
            }
        }
    }
}