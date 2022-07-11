using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossBattler {
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int _health = 5;
        public int Health { get => _health; set => _health = value; }

        public void TakeDamage(int damage)
        {
            for (int i = 0; i < damage; i++)
            {
                _health --;
                UIManager.Instance.TakeDamage();
                if (_health <= 0)
                {
                    GameManager.Instance.LoseGame();
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}
