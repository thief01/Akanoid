using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

namespace UI
{
    public class Highscore : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI highscore;

        private void Awake()
        {
            Game.Highscore.LoadScores();

            highscore.text = "Highscores: ";
            Score[] scores = Game.Highscore.Scores;
            for (int i = 0; i < scores.Length; i++)
            {
                highscore.text += $"\n{i}. {scores[i].name} {scores[i].score}";
            }
        }
        
    }
}