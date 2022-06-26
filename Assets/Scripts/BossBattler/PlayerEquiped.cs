using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossBattler {
    public class PlayerEquiped : MonoBehaviour
    {
        [SerializeField] private Sprite _closedGate;

        private Key _equipedKey = null;
        public Key EquipedKey { get => _equipedKey; set => _equipedKey = value; }

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
                GameController.Instance.SpawnBoss(bossSpawner, _equipedKey.GateColor);
                _equipedKey.transform.parent = null;
                _equipedKey.gameObject.SetActive(false);
                _equipedKey = null;
            }
        }
    }
}
