using UnityEngine;

namespace Platformer
{
    public class PlayerStats : MonoBehaviour
    {
        [SerializeField] private int _maxHealth = 5;
        private int _health;
        private bool _shoudlTakeDamage = false;

        public int Health { get => _maxHealth; set => _maxHealth = value; }
        public bool ShouldTakeDamage { get => _shoudlTakeDamage; set => _shoudlTakeDamage = value; }


        private void Start()
        {
            _health = _maxHealth;
            _shoudlTakeDamage = true;
        }

        public void TakeDamage(int damage)
        {
            if (_shoudlTakeDamage == false) return;

            for (int i = 0; i < damage; i++)
            {
                _health--;
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
