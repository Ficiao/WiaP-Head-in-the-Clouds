using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class Groundbreaker : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.CompareTag("Ground") && collision.transform.TryGetComponent<Rigidbody2D>(out Rigidbody2D component))
            {
                collision.transform.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            }
        }
    }
}
