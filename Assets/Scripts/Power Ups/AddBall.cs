using Platform;
using UnityEngine;

namespace Power_Ups
{
    public class AddBall : PowerUp
    {
        public override void Execute(Transform transform)
        {
            transform.GetComponent<PlatformBallController>().SetBall();
        }
    }
}
