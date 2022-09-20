using General;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Screens
{
    public class MenuController : MonoBehaviour
    {
        public void StartNewGame()
        {
            GeneralSettings.LoadGame = false;
            Play();
        }

        public void LoadGame()
        {
            GeneralSettings.LoadGame = true;
            Play();
        }

        public void Exit()
        {
            Application.Quit();
        }

        private void Play()
        {
            SceneManager.LoadScene("Game");
        }
        
        
    }
}
