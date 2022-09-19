using Patterns;
using UnityEngine;

namespace Map
{
    public class Brick : MonoBehaviour
    {
        public int Health => health;
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private float chanceToSpawnPowerUp = 50;

        private int health;
        private SpriteRenderer spriteRenderer;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    
        private void OnCollisionEnter2D(Collision2D col)
        {
            SetHealth(health-1);
            if (health <= 0)
            {
                TrySpawnPowerUp();
                MapFX fx = Pool<MapFX>.Instance.GetObject();
                fx.transform.position = transform.position;
                fx.PlayAnimation(AnimationType.explodeBrick);
            }
        }

        public void SetHealth(int health)
        {
            this.health = health;
            
            if (health <= 0)
            {
                DestroyBlock();
                return;
            }
            UpdateSprite();
        }

        public void DestroyBlock()
        {
            Pool<Brick>.Instance.BackToPool(this);
        }

        private void UpdateSprite()
        {
            if (health - 1 < sprites.Length)
                spriteRenderer.sprite = sprites[health - 1];
            else
                spriteRenderer.sprite = sprites[sprites.Length - 1];
        }

        private void TrySpawnPowerUp()
        {
            float rand = Random.Range(0, 100);
            if (rand <= chanceToSpawnPowerUp)
            {
                PowerUp powerUp = Pool<PowerUp>.Instance.GetObject();
                if (powerUp != null)
                {
                    powerUp.RandomPowerUpType();
                    powerUp.transform.position = transform.position;
                }
            }
        }
    }
}
