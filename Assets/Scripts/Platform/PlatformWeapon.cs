using Map;
using Patterns;
using UnityEngine;

namespace Platform
{
    public class PlatformWeapon : MonoBehaviour
    {
        [SerializeField] private float attackSpeed;
        [SerializeField] private Transform[] shootPoints;

        private float timeFromLastattack = 0;
        private int ammoLeft = 0;

        private void Update()
        {
            if (timeFromLastattack >= 0)
                timeFromLastattack -= Time.deltaTime;
        }

        public void Fire()
        {
            if (timeFromLastattack > 0)
                return;
            if (ammoLeft <= 0)
                return;
            timeFromLastattack = 1 / attackSpeed;

            for (int i = 0; i < shootPoints.Length; i++)
            {
                var bullet = Pool<Bullet>.Instance.GetObject();
                if (bullet == null)
                    return;
                bullet.transform.position = shootPoints[i].position;
            }

            ammoLeft--;
            CheckWeapon();
        }

        public void AddWeapon(int ammo)
        {
            ammoLeft += ammo;
            CheckWeapon();
        }

        private void CheckWeapon()
        {
            for (int i = 0; i < shootPoints.Length; i++)
            {
                shootPoints[i].gameObject.SetActive(ammoLeft>0);
            }
        }
    }
}
