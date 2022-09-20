using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

namespace Game
{
    [Serializable]
    public class Score
    {
        public string name;
        public int score;
    }
    public static class Highscore
    {
        public static Score[] Scores => scores.ToArray();
        private const string SCORE_PATH = "SCORE.SC";
        private static List<Score> scores = new List<Score>();
        
        public static void LoadScores()
        {
            if (!File.Exists(SCORE_PATH))
                return;

            scores.Clear();
            string json = File.ReadAllText(SCORE_PATH);
            scores = JsonConvert.DeserializeObject<List<Score>>(json);

            if (scores == null)
            {
                scores = new List<Score>();
            }
        }

        public static void SaveScores()
        {
            string scoreString = JsonConvert.SerializeObject(scores);
            if (File.Exists(SCORE_PATH))
            {
                File.Delete(SCORE_PATH);
            }
            File.WriteAllText(SCORE_PATH, scoreString);

        }
        
        public static void TryAddNewScore(Score score)
        {
            int id = IsPossibleToAddNewScore(score.score);
            if (id == 20)
            {
                scores.Add(score);
                return;
            }
            if (id == -1)
                return;
            scores.Insert(id, score);
            scores.RemoveAt(scores.Count-1);
        }
        
        public static void TryAddNewScore(string name, int value)
        {
            Score score = new Score();
            score.name = name;
            score.score = value;
            TryAddNewScore(score);
        }

        public static int IsPossibleToAddNewScore(int score)
        {
            for (int i = 0; i < scores.Count; i++)
            {
                if (score > scores[i].score)
                {
                    return i;
                }
            }

            if (scores.Count < 10)
            {
                return 20;
            }
            
            return -1;
        }
        
    }
}
