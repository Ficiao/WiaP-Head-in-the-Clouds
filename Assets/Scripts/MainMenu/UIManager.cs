using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class UIManager : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] Button _bossBattleButton = null;
        [SerializeField] Button _platformerButton = null;
        [SerializeField] Button _quitButton = null;

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

        private void Init()
        {
            _bossBattleButton.onClick.AddListener(LoadBossBattle);
            _platformerButton.onClick.AddListener(LoadPlatformer);
            _quitButton.onClick.AddListener(Quit);
        }

        public void LoadBossBattle()
        {
            SceneManager.LoadScene("BossLevel");
        }

        public void LoadPlatformer()
        {
            SceneManager.LoadScene("BossLevel");
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}

