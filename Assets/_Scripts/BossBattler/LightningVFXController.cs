using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossBattler
{
    public class LightningVFXController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _lightningParticles = null;
        [SerializeField] private Animator _redWarning = null;
        [SerializeField] private Collider2D _colliderDmg = null;
        [SerializeField] private float _minPauseTime = 0f;
        [SerializeField] private float _maxPauseTime = 0f;
        [SerializeField] private float _timeTillStart = 0f;
        [SerializeField] private float _effectDuration = 0f;
        [SerializeField] private int _damage = 1;
        private IEnumerator _effectCoroutine;

        private void Start()
        {
            _colliderDmg.enabled = false;
            _effectCoroutine = Effect();
            StartCoroutine(_effectCoroutine);
        }

        public void StopEffect()
        {
            StopCoroutine(_effectCoroutine);
        }

        private IEnumerator Effect()
        {
            yield return new WaitForSeconds(_timeTillStart);
            while (true)
            {
                _redWarning.SetTrigger("ShowWarning");
                yield return new WaitForSeconds(1f);
                _redWarning.SetTrigger("ShowWarning");
                yield return new WaitForSeconds(1f);
                _colliderDmg.enabled = true;
                _lightningParticles.ForEach(l => l.SetActive(true));
                yield return new WaitForSeconds(_effectDuration);
                _colliderDmg.enabled = false;
                _lightningParticles.ForEach(l => l.SetActive(false));
                yield return new WaitForSeconds(Random.Range(_minPauseTime, _maxPauseTime + 1));
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<PlayerStats>().TakeDamage(_damage);
            }
        }
    }
}