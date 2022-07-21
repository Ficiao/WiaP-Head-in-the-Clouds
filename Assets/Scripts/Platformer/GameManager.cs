using UnityEngine;

namespace Platformer
{
    public enum GateColor
    {
        Blue,
        Green, 
        Pink
    }

    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }
        

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
        }

        private void Start()
        {
           
        }        

        private void WinGame()
        {
            UIManager.Instance.WinScreen();
        }

        public void LoseGame()
        {
            UIManager.Instance.LoseScreen();
        }
    }

}
