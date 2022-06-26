using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace BossBattler {
    public class UIManager : MonoBehaviour
    {
        private static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }

        [SerializeField] private List<GameObject> _lifePoints;
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _winScreen;

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

        public void TakeDamage()
        {
            _lifePoints[_lifePoints.Count - 1].SetActive(false);
            _lifePoints.RemoveAt(_lifePoints.Count - 1);
        }

        public void LoseScreen()
        {
            _loseScreen.gameObject.SetActive(true);
        }

        public void WinScreen()
        {
            _winScreen.gameObject.SetActive(true);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene("BossLevel");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
