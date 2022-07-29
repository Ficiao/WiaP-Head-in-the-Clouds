using System.Collections.Generic;
using UnityEngine;

namespace BossBattler
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

        [SerializeField] private GameObject _lightningVfx = null;
        [Header("Spawners")]
        [SerializeField] private List<BossSpawner> _greenSpawns;
        [SerializeField] private List<BossSpawner> _blueSpawns;
        [SerializeField] private List<BossSpawner> _pinkSpawns;
        [Header("Key Sprites")]
        [SerializeField] private Sprite _greenKey;
        [SerializeField] private Sprite _blueKey;
        [SerializeField] private Sprite _pinkKey;
        private System.Random rand = new System.Random();
        

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
                SpawnBoss();
        }

        public void SpawnBoss(BossSpawner doorClosed = null, GateColor gateColor = GateColor.Green)
        {
            if(doorClosed != null)
            {
                switch (gateColor)
                {
                    case GateColor.Green:
                        _greenSpawns.Remove(doorClosed);
                        break;
                    case GateColor.Blue:
                        _blueSpawns.Remove(doorClosed);
                        break;
                    case GateColor.Pink:
                        _pinkSpawns.Remove(doorClosed);
                        break;
                    default:
                        break;
                }
            }

            if(_greenSpawns.Count == 0 && _blueSpawns.Count == 0 && _pinkSpawns.Count == 0)
            {
                WinGame();
                return;
            }

            while (true)
            {
                int picker = rand.Next(1, 4);
                switch (picker)
                {
                    case 1:
                        if (_greenSpawns.Count > 0)
                        {
                            picker = rand.Next(0, _greenSpawns.Count);
                            Sprite keyColor;
                            if(_blueSpawns.Count > 0 && _pinkSpawns.Count > 0)
                            {
                                keyColor = rand.Next(0, 2) == 0 ? _blueKey : _pinkKey;
                                if (keyColor == _blueKey) gateColor = GateColor.Blue;
                                else gateColor = GateColor.Pink;
                            }
                            else if(_blueSpawns.Count > 0)
                            {
                                keyColor = _blueKey;
                                gateColor = GateColor.Blue;
                            }
                            else if (_pinkSpawns.Count > 0)
                            {
                                keyColor = _pinkKey;
                                gateColor = GateColor.Pink;
                            }
                            else
                            {
                                keyColor = _greenKey;
                                gateColor = GateColor.Green;
                            }

                            _greenSpawns[picker].SpawnBoss(keyColor, gateColor);
                            return;
                        }
                        break;
                    case 2:     
                        if (_blueSpawns.Count > 0)
                        {
                            Sprite keyColor;
                            if (_greenSpawns.Count > 0 && _pinkSpawns.Count > 0)
                            {
                                keyColor = rand.Next(0, 2) == 0 ? _greenKey : _pinkKey;
                                if (keyColor == _greenKey) gateColor = GateColor.Green;
                                else gateColor = GateColor.Pink;
                            }
                            else if (_greenSpawns.Count > 0)
                            {
                                keyColor = _greenKey;
                                gateColor = GateColor.Green;
                            }
                            else if (_pinkSpawns.Count > 0)
                            {
                                keyColor = _pinkKey;
                                gateColor = GateColor.Pink;
                            }
                            else
                            {
                                keyColor = _blueKey;
                                gateColor = GateColor.Blue;
                            }

                            picker = rand.Next(0, _blueSpawns.Count);
                            _blueSpawns[picker].SpawnBoss(keyColor, gateColor);
                            return;
                        }
                        break;
                    case 3:
                        if (_pinkSpawns.Count > 0)
                        {
                            Sprite keyColor;
                            if (_blueSpawns.Count > 0 && _greenSpawns.Count > 0)
                            {
                                keyColor = rand.Next(0, 2) == 0 ? _blueKey : _greenKey;
                                if (keyColor == _blueKey) gateColor = GateColor.Blue;
                                else gateColor = GateColor.Green;
                            }
                            else if (_blueSpawns.Count > 0)
                            {
                                keyColor = _blueKey;
                                gateColor = GateColor.Blue;
                            }
                            else if (_greenSpawns.Count > 0)
                            {
                                keyColor = _greenKey;
                                gateColor = GateColor.Green;
                            }
                            else
                            {
                                keyColor = _pinkKey;
                                gateColor = GateColor.Pink;
                            }

                            picker = rand.Next(0, _pinkSpawns.Count);
                            _pinkSpawns[picker].SpawnBoss(keyColor, gateColor);
                            return;
                        }
                        break;
                    default:
                        break;
                }
                    
            }
        }

        private void WinGame()
        {
            UIManager.Instance.WinScreen();
            _lightningVfx.SetActive(false);
        }

        public void LoseGame()
        {
            UIManager.Instance.LoseScreen();
        }
    }

}
