using Game;
using Patterns;
using Platform;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private float speed;
        private Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            GameManager.Instance.OnDupliacteBall.AddListener(DuplicateBall);
        }

        public void FreeBall()
        {
            int direction = Random.Range(-1, 2);

            rigidbody2D.velocity = new Vector2(direction, 1).normalized * speed;
        }

        public void DuplicateBall()
        {
            var ball =Pool<Ball>.Instance.GetObject();
            if (ball != null)
            {
                ball.transform.position = transform.position;
                ball.SetVelocity(-rigidbody2D.velocity);
            }
        }

        public void SetVelocity(Vector2 velocity)
        {
            rigidbody2D.velocity = velocity * speed;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "DeathZone")
            {
                GameManager.Instance.BallDead();
                Pool<Ball>.Instance.BackToPool(this);
            }

            if (col.gameObject.tag == "Player")
                OnCollisionWithPlatform(col);
        
        }

        private void OnCollisionWithPlatform(Collision2D col)
        {
            if (col.contacts[0].normal.y > 0)
            {
                var platformSize = col.gameObject.GetComponent<PlatformSize>();
                float distance = transform.position.x - col.transform.position.x;
                float right = distance/ (platformSize.SIZES[platformSize.CurrentSize] *0.5f);
                SetVelocity(new Vector2(right, 1).normalized);
            }
        }
    }
}