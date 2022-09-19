using Patterns;
using UnityEngine;

namespace Map
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            rigidbody2D.velocity = Vector2.up * speed;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Pool<Bullet>.Instance.BackToPool(this);
        }
    }
}
