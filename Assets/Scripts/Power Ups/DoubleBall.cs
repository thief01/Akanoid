using Game;
using UnityEngine;

namespace Power_Ups
{
    public class DoubleBall : PowerUp
    {
        public override void Execute(Transform transform)
        {
            GameManager.Instance.DuplicateBall();
        }
    }
}
