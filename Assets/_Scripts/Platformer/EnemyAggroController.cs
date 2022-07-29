using System.Collections.Generic;
using UnityEngine;

namespace Platformer {
    public class EnemyAggroController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _childrenObjects  = null;
        private List<IAggroable> _childrenMobs = new List<IAggroable>();

        private void Start()
        {
            foreach(GameObject mob in _childrenObjects)
            {
                _childrenMobs.Add(mob.GetComponent<IAggroable>());
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                foreach(IAggroable mob in _childrenMobs)
                {
                    mob.Aggro(collision.transform);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                foreach (IAggroable mob in _childrenMobs)
                {
                    mob.Disaggro();
                }
            }
        }
    }
}
