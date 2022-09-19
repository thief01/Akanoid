using System.Collections;
using Map;
using Patterns;
using Platform;
using UnityEngine;
using UnityEngine.Events;

namespace Game
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        public int CurrentLevel => currentLevel;
        public int CurrentLifes => currentLifes;
        public int CurrentPoints => currentPoints;

        public UnityEvent OnDupliacteBall = new UnityEvent();

        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private GameObject brickPrefab;
        [SerializeField] private GameObject powerUpPrefab;
        [SerializeField] private PlatformBallController platformBallController;
        [SerializeField] private int currentLifes=3;
        private int currentLevel=1;
        private int currentBalls = 0;
        private int currentPoints;
        
        private void Awake()
        {
            InitPools();
        }

        private void Start()
        {
            platformBallController.SetBall();
            MapGenerator.Instance.GenerateMap(currentLevel);
        }

        public void AddPoints(int value)
        {
            currentPoints += value;
        }

        public void BallDead()
        {
            currentBalls--;
            if (currentBalls > 0)
                return;
            if (currentLifes <= 0)
            {
                EndGame();
                return;
            }
            currentLifes--;
            platformBallController.SetBall();
        }

        public void DuplicateBall()
        {
            OnDupliacteBall.Invoke();
        }

        public void EndGame()
        {
        
        }

        public void SaveGame()
        {
        
        }

        public void BallSpawned()
        {
            currentBalls++;
        }
        
        private void InitPools()
        {
            Pool<Ball>.Instance.InitPool(100, ballPrefab);
            Pool<Brick>.Instance.InitPool(20*30, brickPrefab);
            Pool<PowerUp>.Instance.InitPool(25, powerUpPrefab);
            Pool<Bullet>.Instance.InitPool(25, bulletPrefab);
        }

        private void LoadData()
        {
        
        }
    }
}
