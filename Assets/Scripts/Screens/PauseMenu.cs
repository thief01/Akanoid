using Game;
using General;
using Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Screens
{
    public class PauseMenu : MonoBehaviourSingleton<PauseMenu>
    {
        [SerializeField] private GameObject escapeMenu;

        private bool pasuedGame = false;

        public void SwitchPause()
        {
            pasuedGame = !pasuedGame;
            escapeMenu.gameObject.SetActive(pasuedGame);
            Time.timeScale = pasuedGame ? 0 : 1;
        }

        public void Save()
        {
            GameManager.Instance.SaveGame();
        }

        public void BackToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main Menu");
        }

        public void ExitTheGame()
        {
            Application.Quit();
        }
    }
}
