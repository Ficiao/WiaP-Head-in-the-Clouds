using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class BatSpawner : MonoBehaviour
    {
        [SerializeField] private List<Transform> _spanwerLocations = null;
        [SerializeField] private GameObject _batPrefab = null;
        [SerializeField] private float _spawnSpeed = 2f;

        private IEnumerator _spawnerCoroutine = null;
        private List<BatMob> _bats = new List<BatMob>();
        private Transform _player = null;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                _spawnerCoroutine = SpawnBats();
                _player = collision.transform;
                StartCoroutine(_spawnerCoroutine);
            }
        }

        private IEnumerator SpawnBats()
        {
            while (true)
            {
                int spawnGate = Random.Range(0, _spanwerLocations.Count);
                if(_bats.Count == 0)
                {
                    GameObject batObject =  Instantiate(_batPrefab, _spanwerLocations[spawnGate].position, _batPrefab.transform.rotation, transform);
                    BatMob bat = batObject.GetComponent<BatMob>();
                    bat.Player = _player;
                    bat.Spawner = this;
                    bat.Health = bat.MaxHealth;
                }
                else
                {
                    BatMob bat = _bats[_bats.Count - 1];
                    bat.Health = bat.MaxHealth;
                    bat.gameObject.SetActive(true);
                    bat.transform.position = _spanwerLocations[spawnGate].position;
                    _bats.RemoveAt(_bats.Count - 1);
                }

                yield return new WaitForSeconds(_spawnSpeed);                
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                StopCoroutine(_spawnerCoroutine);
                _spawnerCoroutine = null;
            }
        }

        public void KillBat(BatMob bat)
        {
            _bats.Add(bat);
            bat.gameObject.SetActive(false);
        }
    }
}