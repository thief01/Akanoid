using UnityEngine;

namespace Platform
{
    public class PlatformMotor : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody2D rigidbody2D;

        private void Awake()
        {
            rigidbody2D = GetComponent<Rigidbody2D>();
        }
    
        public void Move(float direction)
        {
            rigidbody2D.velocity = new Vector2( direction * speed, 0);
        }
    }
}
