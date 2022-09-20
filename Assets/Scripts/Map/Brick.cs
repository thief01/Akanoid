using Game;
using Patterns;
using UnityEngine;

namespace Map
{
    public class Brick : MonoBehaviour
    {
        private const int POINTS_FOR_HIT = 10;
        private const int POINTS_FOR_DESTROY = 90;
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
            GameManager.Instance.AddPoints(POINTS_FOR_HIT);
            SetHealth(health-1);
            if (health <= 0)
            {
                TrySpawnPowerUp();
                MapFX fx = Pool<MapFX>.Instance.GetObject();
                if (fx == null)
                    return;
                fx.transform.position = transform.position;
                fx.PlayAnimation(AnimationType.explodeBrick);
                GameManager.Instance.AddPoints(POINTS_FOR_DESTROY);
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
            MapGenerator.Instance.BrickDestroyed();
            GameManager.Instance.BrickDestroyed();
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
