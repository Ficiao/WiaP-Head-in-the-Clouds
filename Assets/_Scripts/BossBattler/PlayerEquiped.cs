using UnityEngine;

namespace BossBattler
{
    public class PlayerEquiped : MonoBehaviour
    {
        [SerializeField] private Sprite _closedGate;
        private PlayerStats _playerStats = null;

        private Key _equipedKey = null;
        public Key EquipedKey { get => _equipedKey; set => _equipedKey = value; }

        private void Start()
        {
            _playerStats = GetComponent<PlayerStats>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.CompareTag("Door") || _equipedKey == null)
            {
                return;
            }

            BossSpawner bossSpawner = collision.GetComponent<BossSpawner>();
            if (!bossSpawner.IsActiveGate) return;

            if(bossSpawner.GateColor == _equipedKey.GateColor)
            {
                collision.GetComponent<SpriteRenderer>().sprite = _closedGate;
                bossSpawner.IsActiveGate = false;
                GameManager.Instance.SpawnBoss(bossSpawner, _equipedKey.GateColor);
                _equipedKey.transform.parent = null;
                _equipedKey.gameObject.SetActive(false);
                _equipedKey = null;
                _playerStats.Heal(1);
            }
        }
    }
}
