using System;
using System.Collections;
using System.Collections.Generic;
using Patterns;
using Power_Ups;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Map
{
    public enum PowerUpType
    {
        sizeUp,
        sizeDown,
        doubleBall,
        addBall,
        weapon
    }
    public class PowerUp : MonoBehaviour
    {
        private readonly Power_Ups.PowerUp[] POWER_UPS =
            { new SizeUp(), new SizeDown(), new DoubleBall(), new AddBall(), new AddWeapon() };
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private float speed;
        private PowerUpType powerUpType;
        private SpriteRenderer spriteRenderer;
        private Rigidbody2D rigidbody2D;

        private void Awake()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            rigidbody2D.velocity = -Vector2.up * speed;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag == "Player")
            {
                POWER_UPS[(int)powerUpType].Execute(col.transform);
            }
        
            Pool<PowerUp>.Instance.BackToPool(this);
        }

        public void RandomPowerUpType()
        {
            SetPowerUpType((PowerUpType)Random.Range(0, 1+(int)PowerUpType.weapon));
        }

        public void SetPowerUpType(PowerUpType powerUpType)
        {
            spriteRenderer.sprite = sprites[(int)powerUpType];
            this.powerUpType = powerUpType;
        }
    }
}