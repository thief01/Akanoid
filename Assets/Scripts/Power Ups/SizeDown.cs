using Platform;
using UnityEngine;

namespace Power_Ups
{
    public class SizeDown : PowerUp
    {
        public override void Execute(Transform transform)
        {
            transform.GetComponent<PlatformSize>().SizeDown();
        }
    }
}
