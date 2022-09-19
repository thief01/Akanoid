using Game;
using Map;
using Patterns;
using UnityEngine;

namespace Platform
{
    public class PlatformBallController : MonoBehaviour
    {
        [SerializeField] private Transform spawnBallPosition;
        private Ball holdingBall;

        private void Update()
        {
            if(holdingBall!=null)
                holdingBall.transform.position = spawnBallPosition.position;
        }

        public void SetBall()
        {
            if (holdingBall != null)
                return;
            holdingBall = Pool<Ball>.Instance.GetObject();
            GameManager.Instance.BallSpawned();
        }

        public void FreeBall()
        {
            if (holdingBall == null)
                return;
            holdingBall.FreeBall();
            holdingBall = null;
        }
    }
}
