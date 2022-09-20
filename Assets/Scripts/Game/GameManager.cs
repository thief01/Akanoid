using System;
using System.Collections;
using System.Collections.Generic;
using General;
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
        [SerializeField] private GameObject vfxPrefab;
        [SerializeField] private PlatformBallController platformBallController;
        [SerializeField] private int currentLifes=3;
        private int currentLevel=1;
        private int currentBalls = 0;
        private int currentPoints;

        private PlatformWeapon platformWeapon;
        private PlatformSize platformSize;
        
        private void Awake()
        {
            InitPools();
            platformWeapon = platformBallController.GetComponent<PlatformWeapon>();
            platformSize = platformBallController.GetComponent<PlatformSize>();
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(1);
            if (GeneralSettings.LoadGame)
                LoadGame();
            else
                StartGame();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                LoadGame();
            }
            if (Input.GetKeyDown(KeyCode.O))
            {
                SaveGame();
            }
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
            GameStateData gameStateData = new GameStateData();
            gameStateData.Bullets = platformWeapon.AmmoLeft;
            gameStateData.Level = currentLevel;
            gameStateData.Life = currentLifes;
            gameStateData.Points = currentPoints;
            gameStateData.PlatformSize = platformSize.CurrentSize;
            gameStateData.BallOnThePlatform = platformBallController.HasBall;
            List<BallData> ballDatas = new List<BallData>();
            Ball[] balls = FindObjectsOfType<Ball>();
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i].gameObject.activeInHierarchy)
                {
                    BallData ballData = new BallData();
                    ballData.Position = new System.Numerics.Vector2(balls[i].transform.position.x, balls[i].transform.position.y);
                    ballData.Velocity = new System.Numerics.Vector2( balls[i].Velocity.x,  balls[i].Velocity.y);
                    ballDatas.Add(ballData);
                }
            }

            List<BrickData> brickDatas = new List<BrickData>();
            Brick[] bricks = FindObjectsOfType<Brick>();
            for (int i = 0; i < bricks.Length; i++)
            {
                if (bricks[i].gameObject.activeInHierarchy)
                {
                    BrickData brickData = new BrickData();
                    brickData.Position = new System.Numerics.Vector2(bricks[i].transform.position.x, bricks[i].transform.position.y);
                    brickData.Health = bricks[i].Health;
                    brickDatas.Add(brickData);
                }
            }
            gameStateData.BallsData = ballDatas;
            gameStateData.BrickData = brickDatas;
            GameSaveLoad.Save(gameStateData);
        }

        public void BallSpawned()
        {
            currentBalls++;
        }

        public void BrickDestroyed()
        {
            if(MapGenerator.Instance.GeneratingLevel)
                return;
            if (MapGenerator.Instance.BricksOnMap <= 0)
            {
                currentLevel++;
                MapGenerator.Instance.GenerateMap(currentLevel);
            }
        }
        
        private void InitPools()
        {
            Pool<Ball>.Instance.InitPool(100, ballPrefab);
            Pool<Brick>.Instance.InitPool(20*30, brickPrefab);
            Pool<PowerUp>.Instance.InitPool(25, powerUpPrefab);
            Pool<Bullet>.Instance.InitPool(25, bulletPrefab);
            Pool<MapFX>.Instance.InitPool(50, vfxPrefab);
        }

        private void StartGame()
        {
            platformBallController.SetBall();
            currentLevel = 1;
            MapGenerator.Instance.GenerateMap(currentLevel);
        }

        private void LoadGame()
        {
            GameStateData gameStateData = GameSaveLoad.Load();
            MapGenerator.Instance.LoadLevel(gameStateData.BrickData);
            currentLifes = gameStateData.Life;
            currentLevel = gameStateData.Level;
            currentBalls = gameStateData.BallsData.Count;
            currentPoints = gameStateData.Points;
            if (gameStateData.BallOnThePlatform)
            {
                platformBallController.SetBall();
            }
            platformWeapon.AddWeapon(gameStateData.Bullets);
            for (int i = 0; i < gameStateData.PlatformSize; i++)
            {
                platformSize.SizeUp();
            }

            List<BallData> ballDatas = gameStateData.BallsData;
            for (int i = 0; i < ballDatas.Count; i++)
            {
                Ball ball = Pool<Ball>.Instance.GetObject();
                ball.transform.position = new Vector3(ballDatas[i].Position.X, ballDatas[i].Position.Y,0);
                ball.SetVelocity(new Vector2(ballDatas[i].Velocity.X, ballDatas[i].Velocity.Y));
            }
        }
    }
}
