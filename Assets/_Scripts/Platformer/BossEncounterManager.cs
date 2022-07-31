using System.Collections;
using UnityEngine;

namespace Platformer
{
    public class BossEncounterManager : Singleton<BossEncounterManager>
    {
        [SerializeField] private Animator _leftGate = null;
        [SerializeField] private Animator _rightGate = null;
        [SerializeField] private PlayerController _player = null;
        [SerializeField] private CameraFollow _cameraScript;
        [SerializeField] private Camera _camera = null;
        [SerializeField] private Weapon _playerWeapon = null;
        [SerializeField] private float _startWaitTime = 3f;
        [SerializeField] private AudioSource _bossMusic = null;
        [SerializeField] private GameObject _batBoss = null;
        [SerializeField] private GameObject _batSpawner = null;
        [SerializeField] private PlayerStats _playerStats = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(EncounterStart());
                GetComponent<Collider2D>().enabled = false;
            }
        }

        private IEnumerator EncounterStart()
        {
            _leftGate.SetBool("CloseGate", true);
            _player.Deactivate();
            _playerWeapon.enabled = false;
            _cameraScript.Offset = new Vector3(_cameraScript.Offset.x, _cameraScript.Offset.y + 3.5f, _cameraScript.Offset.z);
            ScreenShake.Instance.StartShaking(_startWaitTime);
            yield return new WaitForSeconds(_startWaitTime);
            _camera.orthographicSize += 2f;
            _bossMusic.Play();
            _player.Activate();
            _playerWeapon.enabled = true;
            _batBoss.SetActive(true);
            _batSpawner.SetActive(true);
        }

        public void BossKilled()
        {
            _camera.orthographicSize -= 2f;
            _cameraScript.Offset = new Vector3(_cameraScript.Offset.x, _cameraScript.Offset.y - 3.5f, _cameraScript.Offset.z);
            StartCoroutine(EncounterEnd());
        }

        private IEnumerator EncounterEnd()
        {
            _leftGate.SetBool("CloseGate", false);
            _rightGate.SetBool("CloseGate", false);
            _player.Deactivate();
            _playerWeapon.enabled = false;
            _batSpawner.SetActive(false);
            ScreenShake.Instance.StartShaking(_startWaitTime);
            _playerStats.ShouldTakeDamage = false;
            yield return new WaitForSeconds(_startWaitTime);
            _bossMusic.Stop();
            _player.Activate();
            _playerWeapon.enabled = true;
            _playerStats.ShouldTakeDamage = true;
        }
    }
}