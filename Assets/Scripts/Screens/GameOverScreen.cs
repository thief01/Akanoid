using Game;
using General;
using Patterns;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Screens
{
    public class GameOverScreen : MonoBehaviourSingleton<GameOverScreen>
    {
        [SerializeField] private GameObject restartGame;
        [SerializeField] private GameObject saveScore;
        [SerializeField] private TMP_InputField inputNickname;
        public void ShowEndScreen()
        {
            Time.timeScale = 0;
            Highscore.LoadScores();
            if (-1 != Highscore.IsPossibleToAddNewScore(GameManager.Instance.CurrentPoints))
            {
                ShowAddHighScore();
                return;
            }
            ShowRestartGame();
        }

        public void RestartGame()
        {
            restartGame.gameObject.SetActive(false);
            Time.timeScale = 1;
            GameManager.Instance.RestartGame();
        }

        public void BackToMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene("Main Menu");
        }

        public void SaveScore()
        {
            Score score = new Score();
            score.name = inputNickname.text;
            score.score = GameManager.Instance.CurrentPoints;
            Highscore.TryAddNewScore(score);
            Highscore.SaveScores();
            inputNickname.text = "";
            saveScore.gameObject.SetActive(false);
            ShowRestartGame();
        }

        private void ShowAddHighScore()
        {
            saveScore.gameObject.SetActive(true);
        }

        private void ShowRestartGame()
        {
            restartGame.gameObject.SetActive(true);
        }
    }
}
