using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MainMenu
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Slider _loadingSlider = null;
        [SerializeField] private TextMeshProUGUI _loadingText = null;
        [SerializeField] private Slider _settingsAudioSlider = null;
        [SerializeField] private SettingsScriptableObject _settingsScriptableObject = null;
        [Header("Buttons")]
        [SerializeField] private Button _bossBattleButton = null;
        [SerializeField] private Button _platformerButton = null;
        [SerializeField] private Button _settingsButton = null;
        [SerializeField] private Button _quitButton = null;
        [SerializeField] private Button _settingsBackButton = null;
        [Header("Screens")]
        [SerializeField] private GameObject _mainScreen = null;
        [SerializeField] private GameObject _settingsScreen = null;
        [SerializeField] private GameObject _loadingScreen = null;

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
            _settingsButton.onClick.AddListener(Settings);
            _quitButton.onClick.AddListener(Quit);
            _settingsBackButton.onClick.AddListener(SettingsBack);
            _settingsAudioSlider.onValueChanged.AddListener(value => AudioLevelChanged(value));

            _settingsAudioSlider.value = _settingsScriptableObject.SoundLevels;
        }

        private void LoadBossBattle()
        {

            _mainScreen.SetActive(false);
            _loadingScreen.SetActive(true);
            StartCoroutine(LoadLevelAsync("BossLevel"));
        }

        private void LoadPlatformer()
        {

            _mainScreen.SetActive(false);
            _loadingScreen.SetActive(true);
            StartCoroutine(LoadLevelAsync("Platformer"));
        }

        private IEnumerator LoadLevelAsync(string levelName)
        {
            _loadingSlider.value = 0f;
            _loadingText.text = "0" + "%";

            AsyncOperation loading = SceneManager.LoadSceneAsync(levelName);

            while (!loading.isDone)
            {
                float progress = Mathf.Clamp01(loading.progress / 0.9f);

                _loadingSlider.value = progress;
                _loadingText.text = (progress * 100f).ToString("0.00") + "%";

                yield return new WaitForSeconds(0.001f);
            }
        }

        private void Settings()
        {
            _mainScreen.SetActive(false);
            _settingsScreen.SetActive(true);
        }

        private void SettingsBack()
        {
            _mainScreen.SetActive(true);
            _settingsScreen.SetActive(false);
        }

        public void Quit()
        {
            Application.Quit();
        }

        private void AudioLevelChanged(float value)
        {
            _settingsScriptableObject.SoundLevels = value;
        }
    }
}

