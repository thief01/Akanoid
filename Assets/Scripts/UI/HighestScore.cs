using System;
using System.Collections;
using System.Collections.Generic;
using Game;
using TMPro;
using UnityEngine;

public class HighestScore : MonoBehaviour
{
    private TextMeshProUGUI textMeshProUGUI;
    private void Awake()
    {
        Highscore.LoadScores();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.text = Highscore.Scores[0].score.ToString();
    }
}
