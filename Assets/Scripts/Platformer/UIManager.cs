using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Platformer
{
    public class UIManager : MonoBehaviour
    {
        [Header("Life Points")]
        [SerializeField] private List<GameObject> _lifePoints = null;
        [Header("Menu Screens")]
        [SerializeField] private GameObject _loseScreen = null;
        [SerializeField] private GameObject _winScreen = null;
        [Header("Buttons")]
        [SerializeField] private Button _winPlayAgainButton = null;
        [SerializeField] private Button _winQuitButton = null;
        [SerializeField] private Button _losePlayAgainButton = null;
        [SerializeField] private Button _loseQuitButton = null;

        private int _health;

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

            _health = _lifePoints.Count;
        }

        public void TakeDamage()
        {
            _health--;
            _lifePoints[_health].SetActive(false);
        }

        public void Heal()
        {
            _lifePoints[_health].SetActive(true);
            _health++;
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
            SceneManager.LoadScene("Platformer");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
