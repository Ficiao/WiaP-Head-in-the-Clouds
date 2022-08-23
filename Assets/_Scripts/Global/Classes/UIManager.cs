using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    [SerializeField] private string _levelname = null;
    [SerializeField] private List<GameObject> _lifePoints = null;
    [SerializeField] private SettingsScriptableObject _settingsScriptableObject = null;
    [SerializeField] private AudioSource _backgroundMusic = null;
    [SerializeField] private PlayerController _playerController = null;
    [SerializeField] private Weapon _playerWeapon = null;
    [SerializeField] private TextMeshProUGUI _winTimeText = null;
    [SerializeField] private Image _dashCooldownShadow = null;
    [Header("Menu Screens")]
    [SerializeField] private GameObject _loseScreen = null;
    [SerializeField] private GameObject _winScreen = null;
    [SerializeField] private GameObject _settingsScreen = null;
    [SerializeField] private GameObject _pauseScreen = null;
    [SerializeField] private GameObject _pauseButtonObject = null;
    [Header("Buttons")]
    [SerializeField] private Button _winPlayAgainButton = null;
    [SerializeField] private Button _winMainMenuButton = null;
    [SerializeField] private Button _winSettingsButton = null;
    [SerializeField] private Button _winQuitButton = null;
    [SerializeField] private Button _losePlayAgainButton = null;
    [SerializeField] private Button _loseMainMenuButton = null;
    [SerializeField] private Button _loseSettingsButton = null;
    [SerializeField] private Button _loseQuitButton = null;
    [SerializeField] private Button _pauseResumeButton = null;
    [SerializeField] private Button _pauseMainMenuButton = null;
    [SerializeField] private Button _pauseRestartButton = null;
    [SerializeField] private Button _pauseSettingsButton = null;
    [SerializeField] private Button _pauseQuitButton = null;
    [SerializeField] private Button _pauseButton = null;
    [SerializeField] private Button _settingsBackButton = null;
    [SerializeField] private Slider _settingsAudioSlider = null;
    private GameObject _currentScreen = null;
    private Coroutine _cooldownCoroutine = null;
    private int _health = 0;
    private float _startTime = 0f;

    public bool DashReady { get => _dashCooldownShadow.fillAmount <= 0; }

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
        _winPlayAgainButton.onClick.AddListener(PlayAgain);
        _winMainMenuButton.onClick.AddListener(MainMenu);
        _winSettingsButton.onClick.AddListener(Settings);
        _winQuitButton.onClick.AddListener(Quit);
        _losePlayAgainButton.onClick.AddListener(PlayAgain);
        _loseMainMenuButton.onClick.AddListener(MainMenu);
        _loseSettingsButton.onClick.AddListener(Settings);
        _loseQuitButton.onClick.AddListener(Quit);
        _pauseResumeButton.onClick.AddListener(Resume);
        _pauseMainMenuButton.onClick.AddListener(MainMenu);
        _pauseRestartButton.onClick.AddListener(PlayAgain);
        _pauseSettingsButton.onClick.AddListener(Settings);
        _pauseQuitButton.onClick.AddListener(Quit);
        _settingsBackButton.onClick.AddListener(SettingsBack);
        _pauseButton.onClick.AddListener(Pause);
        _settingsAudioSlider.onValueChanged.AddListener(value => AudioLevelChanged(value));

        _backgroundMusic.volume = _settingsScriptableObject.SoundLevels;
        _settingsAudioSlider.value = _settingsScriptableObject.SoundLevels;
        _health = _lifePoints.Count;
        _startTime = Time.time;
    }

    public void TakeDamage()
    {
        if (_health <= 0) return;
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
        _settingsScreen.SetActive(false);
        _loseScreen.gameObject.SetActive(true);
        _currentScreen = _loseScreen;
        _playerController.Deactivate();
        _playerWeapon.enabled = false;
        _pauseButtonObject.SetActive(false);
    }

    public void WinScreen()
    {
        _settingsScreen.SetActive(false);
        _winScreen.gameObject.SetActive(true);
        _currentScreen = _winScreen;
        _playerController.Deactivate();
        _playerWeapon.enabled = false;
        _pauseButtonObject.SetActive(false);
        TimeSpan ts = TimeSpan.FromSeconds(Time.time - _startTime);
        _winTimeText.text = string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds);
        _playerController.GetComponent<Animator>().SetBool("Dance", true);
    }

    private void Pause()
    {
        _pauseScreen.SetActive(true);
        _currentScreen = _pauseScreen;
        _playerController.Deactivate();
        _playerWeapon.enabled = false;
        _pauseButtonObject.SetActive(false);
    }

    private void Resume()
    {
        _currentScreen = null;
        _pauseScreen.SetActive(false);
        _playerController.Activate();
        _playerWeapon.enabled = true;
        _pauseButtonObject.SetActive(true);
    }

    private void PlayAgain()
    {
        SceneManager.LoadScene(_levelname);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Settings()
    {
        _settingsScreen.gameObject.SetActive(true);
        _currentScreen.SetActive(false);
    }

    private void SettingsBack()
    {
        _settingsScreen.gameObject.SetActive(false);
        _currentScreen.SetActive(true);
    }

    private void AudioLevelChanged(float value)
    {
        _backgroundMusic.volume = value;
        _settingsScriptableObject.SoundLevels = value;
    }

    private void Quit()
    {
        Application.Quit();
    }

    public void DashUsed(float cooldown)
    {
        if (_cooldownCoroutine != null) return;
        _cooldownCoroutine = StartCoroutine(AbilityCooldown(cooldown, _dashCooldownShadow));
    }

    private IEnumerator AbilityCooldown(float cooldown, Image cooldownShadow)
    {
        float timePassed = - Time.deltaTime;
        while (timePassed <= cooldown)
        {
            timePassed += Time.deltaTime;
            cooldownShadow.fillAmount = 1f - Math.Clamp(timePassed / cooldown, 0f, 1f);
            yield return null;
        }
        _cooldownCoroutine = null;
    }
}



