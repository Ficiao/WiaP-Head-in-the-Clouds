using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace BossBattler {
    public class UIManager : MonoBehaviour
    {
        [Header("Life Points")]
        [SerializeField] private List<GameObject> _lifePoints;
        [Header("Menu Screens")]
        [SerializeField] private GameObject _loseScreen;
        [SerializeField] private GameObject _winScreen;
        [Header("Buttons")]
        [SerializeField] private Button _winPlayAgainButton = null;
        [SerializeField] private Button _winQuitButton = null;
        [SerializeField] private Button _losePlayAgainButton = null;
        [SerializeField] private Button _loseQuitButton = null;

        private static UIManager _instance;
        public static UIManager Instance { get { return _instance; } }
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

            Init();
        }

        public void Init()
        {
            _winPlayAgainButton.onClick.AddListener(PlayAgain);
            _winQuitButton.onClick.AddListener(Quit);
            _losePlayAgainButton.onClick.AddListener(PlayAgain);
            _loseQuitButton.onClick.AddListener(Quit);
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
