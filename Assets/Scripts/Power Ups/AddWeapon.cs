using Platform;
using UnityEngine;

namespace Power_Ups
{
    public class AddWeapon : PowerUp
    {
        private const int AMMO = 10;
        public override void Execute(Transform transform)
        {
            transform.GetComponent<PlatformWeapon>().AddWeapon(AMMO);
        }
    }
}
