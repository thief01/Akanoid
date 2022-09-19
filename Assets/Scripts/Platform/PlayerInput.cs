using UnityEngine;

namespace Platform
{
    public class PlayerInput : MonoBehaviour
    {
        private PlatformWeapon platformWeapon;
        private PlatformMotor platformMotor;
        private PlatformBallController platformBallController;

        private void Awake()
        {
            platformWeapon = GetComponent<PlatformWeapon>();
            platformMotor = GetComponent<PlatformMotor>();
            platformBallController = GetComponent<PlatformBallController>();
        }
    
        void Update()
        {
            GetInput();
        }

        private void GetInput()
        {
            if (Input.GetKeyDown(KeyCode.RightControl))
            {
                platformWeapon.Fire();
            }

            float moveDirection = Input.GetAxis("Horizontal");
            platformMotor.Move(moveDirection);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                platformBallController.FreeBall();
            }
        }
    }
}
