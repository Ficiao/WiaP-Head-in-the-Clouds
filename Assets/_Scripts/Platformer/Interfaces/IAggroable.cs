using UnityEngine;

namespace Platformer
{
    interface IAggroable
    {
        public void Aggro(Transform target);
        public void Disaggro();
    }
}
