using UnityEngine;

namespace BossBattler
{
    public class ObjectPool : MonoBehaviour
    {
        private static ObjectPool _instance;
        public static ObjectPool Instance { get { return _instance; } }

        [SerializeField] private GameObject keyPrefab;
        private SpriteRenderer keyRenderer;
        [SerializeField] private GameObject bossPrefab;
        private BossController boss;
       

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

            boss = Instantiate(bossPrefab).GetComponent<BossController>();
            boss.gameObject.SetActive(false);
            keyRenderer = Instantiate(keyPrefab).GetComponent<SpriteRenderer>();
            keyRenderer.gameObject.SetActive(false);
        }

        public BossController GetBoss(Sprite key, GateColor gateColor)
        {
            boss.gameObject.SetActive(true);
            keyRenderer.sprite = key;
            keyRenderer.GetComponent<Key>().GateColor = gateColor;
            boss.Key = keyRenderer.gameObject;
            return boss;
        }

        public void DestroyBoss()
        {
            boss.gameObject.SetActive(false);
        }

    }
}
