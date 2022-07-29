using System.Collections;
using UnityEngine;

namespace BossBattler
{
    public class BossSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnLocation;
        [SerializeField] private Sprite _redAlert;
        [SerializeField] private float _alertTime = 4f;
        private Sprite _defaultSprite;
        private SpriteRenderer _spriteRenderer;

        public GateColor GateColor;
        public bool IsActiveGate = true;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _defaultSprite = _spriteRenderer.sprite;
        }

        public void SpawnBoss(Sprite key, GateColor gateColor)
        {
            StartCoroutine(BossSpawning(key, gateColor));
        }

        private IEnumerator BossSpawning(Sprite key, GateColor gateColor)
        {
            _spriteRenderer.sprite = _redAlert;
            yield return new WaitForSeconds(_alertTime);
            BossController boss = ObjectPool.Instance.GetBoss(key, gateColor);
            boss.transform.position = spawnLocation.transform.position;
            boss.transform.localScale = spawnLocation.transform.localScale;
            _spriteRenderer.sprite = _defaultSprite;

        }
    }
}
