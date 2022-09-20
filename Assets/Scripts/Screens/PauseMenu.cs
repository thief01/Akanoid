using System.Collections;
using Game;
using General;
using Patterns;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Screens
{
    public class PauseMenu : MonoBehaviourSingleton<PauseMenu>
    {
        [SerializeField] private GameObject escapeMenu;
        [SerializeField] private TextMeshProUGUI saved;

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
            StartCoroutine(TextDelay());
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

        private IEnumerator TextDelay()
        {
            saved.gameObject.SetActive(true);
            yield return new WaitForSeconds(2);
            saved.gameObject.SetActive(false);
        }
    }
}
