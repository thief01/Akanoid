using Game;
using Platform;
using TMPro;
using UnityEngine;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI score;

        [SerializeField] private TextMeshProUGUI level;

        [SerializeField] private TextMeshProUGUI lifes;

        [SerializeField] private TextMeshProUGUI bullets;


        [SerializeField] private PlatformWeapon platformWeapon;

        // Update is called once per frame
        void Update()
        {
            score.text = GameManager.Instance.CurrentPoints.ToString();
            level.text = GameManager.Instance.CurrentLevel.ToString();
            lifes.text = GameManager.Instance.CurrentLifes.ToString();
            bullets.text = platformWeapon.AmmoLeft.ToString();
        }
    }
}
