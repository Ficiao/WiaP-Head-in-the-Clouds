using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private List<KeyGemGate> _keyGemGates = null;
        [SerializeField] private float _timeTillGameWin = 3f;

        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }
        

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

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(WinGame());
            }
        }

        private IEnumerator WinGame()
        {
            yield return new WaitForSeconds(_timeTillGameWin);
            UIManager.Instance.WinScreen();
        }

        public void LoseGame()
        {
            UIManager.Instance.LoseScreen();
        }

        public void KeyGemPickedUp()
        {
            foreach(KeyGemGate gate in _keyGemGates)
            {
                gate.KeyGemPickedUp();
            }
        }
    }
}
