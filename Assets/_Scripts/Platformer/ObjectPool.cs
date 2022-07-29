using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _deathEffectPrefab = null;
        private List<GameObject> _deathEffectPool = new List<GameObject>();

        private static ObjectPool _instance;
        public static ObjectPool Instance { get { return _instance; } }
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public GameObject GetDeathEffect()
        {
            return Instantiate(_deathEffectPrefab);
        }
    }
}