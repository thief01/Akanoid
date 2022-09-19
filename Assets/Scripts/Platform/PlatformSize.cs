using UnityEngine;

namespace Platform
{
    public class PlatformSize : MonoBehaviour
    {
        public readonly float[] SIZES = new[] { 0.71f, 1.38f, 2.04f };
        private const int MAX_SIZE = 2;
        private const int MIN_SIZE = 0;

        public int CurrentSize => currentSize;
    
        [SerializeField] private Transform controlingPlatform;
        [SerializeField] private Sprite[] sprites;

        private SpriteRenderer spriteRenderer;
        private BoxCollider2D boxCollider2D;
        private int currentSize;

        private void Awake()
        {
            spriteRenderer = controlingPlatform.GetComponent<SpriteRenderer>();
            boxCollider2D = controlingPlatform.GetComponent<BoxCollider2D>();
        }

        public void SizeUp()
        {
            if (currentSize + 1 <= MAX_SIZE)
            {
                currentSize++;
                OnSizeChanged();
            }
        }

        public void SizeDown()
        {
            if (currentSize - 1 >= MIN_SIZE)
            {
                currentSize--;
                OnSizeChanged();
            }
        }

        public void ResetSize()
        {
            currentSize = MIN_SIZE;
            OnSizeChanged();
        }

        private void OnSizeChanged()
        {
            spriteRenderer.sprite = sprites[currentSize];
            boxCollider2D.size = new Vector2(SIZES[currentSize], boxCollider2D.size.y);
        }
    }
}
