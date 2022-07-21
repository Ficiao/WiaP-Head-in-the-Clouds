using UnityEngine;

namespace Platformer
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 5;
        public int Health { get => _maxHealth; set => _maxHealth = value; }

        private int _health;

        private void Start()
        {
            _health = _maxHealth;
        }

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

        public void Heal(int heal)
        {
            for (int i = 0; i < heal; i++)
            {
                if (_health >= _maxHealth)
                {
                    return;
                }
                _health++;
                UIManager.Instance.Heal();
            }
        }
    }
}
