using Platform;
using UnityEngine;

namespace Power_Ups
{
    public class SizeUp : PowerUp
    {
        public override void Execute(Transform transform)
        {
            transform.GetComponent<PlatformSize>().SizeUp();
        }
    }
}
